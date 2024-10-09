using System.ComponentModel.DataAnnotations;

namespace BwssbRestfulAPI.Models.AxisKiosk
{
    public class ReceiptRequest
    {
       
        [Required(ErrorMessage = "RR Number is required")]
        [StringLength(50, ErrorMessage = "RR Number cannot exceed 50 characters")]
        public string RrNumber { get; set; }

        public string BWSSBKey { get; set; }
    }
}
