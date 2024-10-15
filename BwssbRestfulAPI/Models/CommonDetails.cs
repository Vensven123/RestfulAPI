namespace BwssbRestfulAPI.Models
{
    public class CommonDetails
    {

        public class CashCounter
        {
            public int CCID { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }
            public string Fax { get; set; }
            public string SDID { get; set; }
            public string Email { get; set; }
            public byte? AreaID { get; set; }
            public string Type { get; set; }
        }
    

    }
}
