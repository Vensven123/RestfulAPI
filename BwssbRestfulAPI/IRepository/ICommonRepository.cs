using static BwssbRestfulAPI.Models.CommonDetails;

namespace BwssbRestfulAPI.IRepository
{
    public interface ICommonRepository
    {
        Task<(List<CashCounter> Data, string Message)> GetCashCounterByCCIDAsync(int ccid);
    }
}
