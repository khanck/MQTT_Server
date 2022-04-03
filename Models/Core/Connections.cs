
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models.Core
{
    [Table("Connections")]
    public class Connections
    {
        public Guid ID { get; set; }
        [StringLength(100)]
        [Required]
        [RegularExpression(@"^[\u0600-\u06FFa-zA-Z0-9\s_.\/-^(\p{L}\p{M}*)+$]+$", ErrorMessage = "Invalid Client Id")]
        public string ClientId { get; set; }
        [StringLength(100)]
        [RegularExpression(@"^[\u0600-\u06FFa-zA-Z0-9\s_.\/-^(\p{L}\p{M}*)+$]+$", ErrorMessage = "Invalid Endpoint")]
        public string Endpoint { get; set; }
        [StringLength(100)]
        [RegularExpression(@"^[\u0600-\u06FFa-zA-Z0-9\s_.\/-^(\p{L}\p{M}*)+$]+$", ErrorMessage = "Invalid Reason Code")]
        public string ReasonCode { get; set; }
        [StringLength(100)]
        [RegularExpression(@"^[\u0600-\u06FFa-zA-Z0-9\s_.\/-^(\p{L}\p{M}*)+$]+$", ErrorMessage = "Invalid Return Code")]
        public string ReturnCode { get; set; }
        public DateTime TimeStamp { get; set; }

    }
}
