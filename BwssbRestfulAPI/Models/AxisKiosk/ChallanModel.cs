using System.ComponentModel.DataAnnotations;

namespace BwssbRestfulAPI.Models.AxisKiosk
{
    public class ChallanModel
    {

        [Required(ErrorMessage = "Challan Number is required")]
        [StringLength(50, ErrorMessage = "Challan Number cannot exceed 50 characters")]
        public string ChallanNumber { get; set; }

        [Required(ErrorMessage = "Challan Amount is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Challan Amount must be greater than zero")]
        public int ChallanAmount { get; set; }

        [Required(ErrorMessage = "Cashier name is required")]
        [StringLength(50, ErrorMessage = "Cashier Name cannot exceed 50 characters")]
        public string CashierC { get; set; }

        [Required(ErrorMessage = "Pay Date is required")]
        [StringLength(50, ErrorMessage = "Pay Date cannot exceed 50 characters")]
        public string PayDateC { get; set; }

        [Required(ErrorMessage = "CCID is required")]
        public int Ccid { get; set; }

        [StringLength(50, ErrorMessage = "K Receipt Number cannot exceed 50 characters")]
        public string KReceiptNumberC { get; set; }

        [StringLength(50, ErrorMessage = "Receipt Number cannot exceed 50 characters")]
        public string ReceiptNumberC { get; set; }

        [Required(ErrorMessage = "Amount Collected is required")]
        public int AmountCollectedC { get; set; }

        [Required(ErrorMessage = "SDID is required")]
        [StringLength(3, ErrorMessage = "SDID cannot exceed 3 characters")]
        public string SdidC { get; set; }

        [Required(ErrorMessage = "Realised status is required")]
        public bool RealisedC { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public int UserIdC { get; set; }

        [StringLength(20, ErrorMessage = "Batch Number cannot exceed 20 characters")]
        public string BatchNoC { get; set; }

        [StringLength(50, ErrorMessage = "Collection ID CP cannot exceed 50 characters")]
        public string CollectionIdCP { get; set; }

        [StringLength(50, ErrorMessage = "Receipt Number CP cannot exceed 50 characters")]
        public string ReceiptNumberCP { get; set; }

        [StringLength(50, ErrorMessage = "Collection ID CC cannot exceed 50 characters")]
        public string CollectionIdCC { get; set; }

        [StringLength(50, ErrorMessage = "Mode cannot exceed 50 characters")]
        public string ModeC { get; set; }

        public int? CnumberC { get; set; }
        public byte BankIdC { get; set; }

        [StringLength(50, ErrorMessage = "Bank Branch cannot exceed 50 characters")]
        public string BankBranchC { get; set; }

        [Required(ErrorMessage = "Amount Collected CC is required")]
        public int AmountCollectedCC { get; set; }

        [StringLength(50, ErrorMessage = "Cheque Date cannot exceed 50 characters")]
        public string ChdateC { get; set; }
    }
}
