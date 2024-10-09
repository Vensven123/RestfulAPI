namespace BwssbRestfulAPI.Models.AxisKiosk
{
    public class ReceiptDetails
    {
        public string RRNumber { get; set; }
        public int ConsumerID { get; set; }
        public string ConsumerName { get; set; }
        public string Address { get; set; }
        public string BillNumber { get; set; }
        public decimal BillAmount { get; set; }
        public DateTime BillDate { get; set; }
        public DateTime BillDueDate { get; set; }
        public string SDID { get; set; }
    }
}
