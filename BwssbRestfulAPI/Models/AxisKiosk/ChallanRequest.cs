using System.ComponentModel.DataAnnotations;

namespace BwssbRestfulAPI.Models.AxisKiosk
{
    public class ChallanRequest
    {

        [Required(ErrorMessage = "RR Number is required")]
        [StringLength(50, ErrorMessage = "RR Number cannot exceed 50 characters")]
        public string ChallanNumber { get; set; }

        public string BWSSBKey { get; set; }


    }
}
