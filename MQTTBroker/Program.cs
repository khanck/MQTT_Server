using MQTTnet;
using MQTTnet.Server;
using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Models.Core;
using DAL.Core;

namespace MQTTBroker
{
    class Program
    {
        static int MessageCounter;
        private static IServiceProvider _serviceProvider;
        private static Boolean isAuthenticationRequired;
        private static string username;
        private static string password;
        //private static CommonRepository<Connections> commonRepository;

        static void Main(string[] args)
        {    
            IConfiguration config = new ConfigurationBuilder()
                 .AddJsonFile("appSettings.json")
                 .Build();
          
            _serviceProvider = new ServiceCollection()
               .AddLogging()
               .AddDbContext<CoreDbContext>(options =>
               options.UseSqlServer(config.GetConnectionString("DBConnecet"), options => options.EnableRetryOnFailure()
               // ,optionsBuilder => optionsBuilder.MigrationsAssembly("MQTTBroker")
               )).BuildServiceProvider();


            int port = Convert.ToInt32(config.GetSection("Settings")["EndpointPort"]);
            isAuthenticationRequired = Convert.ToBoolean(config.GetSection("Settings")["IsAuthenticationRequired"]);
            username = Convert.ToString(config.GetSection("Settings")["Username"]);
            password = Convert.ToString(config.GetSection("Settings")["Password"]);

            // Create the options for our MQTT Broker
            MqttServerOptionsBuilder options = new MqttServerOptionsBuilder()
                                                     // set endpoint to localhost
                                                     .WithDefaultEndpoint()
                                                     // port used 
                                                     .WithDefaultEndpointPort(port)
                                                     // handler for new connections
                                                     .WithConnectionValidator(OnNewConnection)
                                                     // handler for new messages
                                                     .WithApplicationMessageInterceptor(OnNewMessage);

            // creates a new mqtt server     
            IMqttServer mqttServer = new MqttFactory().CreateMqttServer();

            // start the server with options  
            mqttServer.StartAsync(options.Build()).GetAwaiter().GetResult();

            // keep application running until user press a key
            Console.ReadLine();
        }
        
        public static void OnNewConnection(MqttConnectionValidatorContext context)
        {
            bool IsAuthorized = false;
            if (isAuthenticationRequired)
            {
                if ((context.Username == username) && (context.Password == password))
                    IsAuthorized = true;
                else
                    IsAuthorized = false;
            }
            else
                IsAuthorized = true;

            if (IsAuthorized)
            {
                using (CommonRepository<Connections> ConnectionRepo = new CommonRepository<Connections>(_serviceProvider))
                {
                    try
                    {
                        Connections connection = new Connections();

                        connection.ID = new Guid();
                        connection.ClientId = context.ClientId;
                        connection.Endpoint = context.Endpoint;
                        connection.ReasonCode = context.ReasonCode.ToString();
                        connection.ReturnCode = (string)context.ReturnCode.ToString();
                        connection.TimeStamp = DateTime.Now;

                        ConnectionRepo.Add(connection);
                        ConnectionRepo.SaveChanges();

                    }
                    catch (Exception ex)
                    {
                        var error = ex.Message;
                    }
                }
            }
            //Log.Logger.Information(
            //        "New connection: ClientId = {clientId}, Endpoint = {endpoint}",
            //        context.ClientId,
            //        context.Endpoint);
        }

        public static void OnNewMessage(MqttApplicationMessageInterceptorContext context)
        {
            var payload = context.ApplicationMessage?.Payload == null ? null : Encoding.UTF8.GetString(context.ApplicationMessage?.Payload);

            using (CommonRepository<Messages> MessageRepo = new CommonRepository<Messages>(_serviceProvider))
            {
                try
                {
                    Messages message = new Messages();

                    message.ID = new Guid();
                    message.MessageId = MessageCounter;
                    message.ClientId = context.ClientId;
                    message.Topic = context.ApplicationMessage?.Topic;
                    message.Payload = payload;
                    message.QualityOfServiceLevel = context.ApplicationMessage?.QualityOfServiceLevel.ToString();
                    message.Retain = (bool)context.ApplicationMessage?.Retain;

                    message.TimeStamp = DateTime.Now;

                    MessageRepo.Add(message);
                    MessageRepo.SaveChanges();

                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                }
            }

            MessageCounter++;

            //Log.Logger.Information(
            //    "MessageId: {MessageCounter} - TimeStamp: {TimeStamp} -- Message: ClientId = {clientId}, Topic = {topic}, Payload = {payload}, QoS = {qos}, Retain-Flag = {retainFlag}",
            //    MessageCounter,
            //    DateTime.Now,
            //    context.ClientId,
            //    context.ApplicationMessage?.Topic,
            //    payload,
            //    context.ApplicationMessage?.QualityOfServiceLevel,
            //    context.ApplicationMessage?.Retain);
           
        }

    }
}
