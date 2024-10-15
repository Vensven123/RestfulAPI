using System.ComponentModel.DataAnnotations;

namespace BwssbRestfulAPI.Models.JWToken
{
    public class RequestToken
    {
        [Required]
        public string CCID { get; set; }

        [Required]
        public string BwssBKey { get; set; }

    }
}
