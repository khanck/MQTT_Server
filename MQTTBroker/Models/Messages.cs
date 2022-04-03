using System;
using System.Collections.Generic;
using System.Text;

namespace MQTTBroker.Models
{
   public class Messages
    {
        public Guid ID { get; set; }
        public string MessageId { get; set; }
        public string ClientId { get; set; }
        public string Topic { get; set; }
        public string TopicAlias { get; set; }
        public string Payload { get; set; }
        public string QualityOfServiceLevel { get; set; }
        public bool Retain { get; set; }  

        public DateTime TimeStamp { get; set; }

    }
}
