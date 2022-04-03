using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models.Core
{
    [Table("Messages")]
    public class Messages
    {
        public Guid ID { get; set; }        
        public long? MessageId { get; set; }
        [StringLength(100)]
        [Required]
        [RegularExpression(@"^[\u0600-\u06FFa-zA-Z0-9\s_.\/-^(\p{L}\p{M}*)+$]+$", ErrorMessage = "Invalid Client Id")]
        public string ClientId { get; set; }
        [StringLength(100)]
        [Required]
        [RegularExpression(@"^[\u0600-\u06FFa-zA-Z0-9\s_.\/-^(\p{L}\p{M}*)+$]+$", ErrorMessage = "Invalid Topic")]
        public string Topic { get; set; }
        [StringLength(100)]
        [RegularExpression(@"^[\u0600-\u06FFa-zA-Z0-9\s_.\/-^(\p{L}\p{M}*)+$]+$", ErrorMessage = "Invalid Topic Alias")]
        public string TopicAlias { get; set; }
        [StringLength(100)]
        [RegularExpression(@"^[\u0600-\u06FFa-zA-Z0-9\s_.\/-^(\p{L}\p{M}*)+$]+$", ErrorMessage = "Invalid Payload")]
        public string Payload { get; set; }
        [StringLength(100)]
        [RegularExpression(@"^[\u0600-\u06FFa-zA-Z0-9\s_.\/-^(\p{L}\p{M}*)+$]+$", ErrorMessage = "Invalid Quality Of ServiceLevel")]
        public string QualityOfServiceLevel { get; set; }
        public bool Retain { get; set; }

        public DateTime TimeStamp { get; set; }

    }
}
