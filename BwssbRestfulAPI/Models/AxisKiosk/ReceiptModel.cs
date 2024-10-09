using System.ComponentModel.DataAnnotations;

namespace BwssbRestfulAPI.Models.AxisKiosk
{
    public class ReceiptModel
    {
        [Required(ErrorMessage = "RR Number is required")]
        [StringLength(50, ErrorMessage = "RR Number cannot exceed 50 characters")]
        public string RrNumber { get; set; }

        [Required(ErrorMessage = "Bill Number is required")]
        [StringLength(50, ErrorMessage = "Bill Number cannot exceed 50 characters")]
        public string BillNumber { get; set; }

        [Required(ErrorMessage = "Bill Amount is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Bill Amount must be greater than zero")]
        public int BillAmount { get; set; }

        [Required(ErrorMessage = "Cashier name is required")]
        [StringLength(50, ErrorMessage = "Cashier Name cannot exceed 50 characters")]
        public string Cashier { get; set; }

        [Required(ErrorMessage = "Pay Date is required")]
        [StringLength(50, ErrorMessage = "Pay Date cannot exceed 50 characters")]
        public string PayDate { get; set; }

        [Required(ErrorMessage = "CCID is required")]
        public int Ccid { get; set; }

        [StringLength(50, ErrorMessage = "K Receipt Number cannot exceed 50 characters")]
        public string KReceiptNumber { get; set; }

        [StringLength(50, ErrorMessage = "Receipt Number cannot exceed 50 characters")]
        public string ReceiptNumber { get; set; }

        [Required(ErrorMessage = "Amount Collected is required")]
        public int AmountCollected { get; set; }

        [Required(ErrorMessage = "SDID is required")]
        [StringLength(10, ErrorMessage = "SDID cannot exceed 10 characters")]
        public string Sdid { get; set; }

        [StringLength(3, ErrorMessage = "Org SDID cannot exceed 3 characters")]
        public string OrgSdid { get; set; }

        [Required(ErrorMessage = "Realised status is required")]
        public bool Realised { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public int UserId { get; set; }

        [StringLength(20, ErrorMessage = "Batch Number cannot exceed 20 characters")]
        public string BatchNo { get; set; }

        [StringLength(50, ErrorMessage = "Collection ID RP cannot exceed 50 characters")]
        public string CollectionIdRP { get; set; }

        [StringLength(50, ErrorMessage = "Receipt Number RP cannot exceed 50 characters")]
        public string ReceiptNumberRP { get; set; }

        [StringLength(50, ErrorMessage = "Collection ID RC cannot exceed 50 characters")]
        public string CollectionIdRC { get; set; }

        [StringLength(50, ErrorMessage = "Mode cannot exceed 50 characters")]
        public string Mode { get; set; }

        public int? CNumber { get; set; }

        
        public byte BankId { get; set; }

        [StringLength(50, ErrorMessage = "Bank Branch cannot exceed 50 characters")]
        public string BankBranch { get; set; }

        [Required(ErrorMessage = "Amount Collected RC is required")]
        public int AmountCollectedRC { get; set; }

        [StringLength(50, ErrorMessage = "Cheque Date cannot exceed 50 characters")]
        public string Chdate { get; set; }
    }
}
