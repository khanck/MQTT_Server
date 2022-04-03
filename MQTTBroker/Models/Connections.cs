using System;
using System.Collections.Generic;
using System.Text;

namespace MQTTBroker.Models
{
   public class Connections
    {
        public Guid ID { get; set; }
        public string ClientId { get; set; }
        public string Endpoint { get; set; }
        public string ReasonCode { get; set; }
        public string ReturnCode { get; set; }
        public DateTime TimeStamp { get; set; }

    }
}
