using BwssbRestfulAPI.DBServices;
using static BwssbRestfulAPI.Models.CommonDetails;
using System.Data.SqlClient;
using System.Data;
using BwssbRestfulAPI.IRepository;
using BwssbRestfulAPI.Models.AxisKiosk;

namespace BwssbRestfulAPI.Repository
{

    public class CommonRepository : ICommonRepository
    {

        private readonly IDBServices _dBServices;
        private readonly IConfiguration _configuration;


        public CommonRepository(IDBServices dBServices, IConfiguration configuration)
        {

            _dBServices = dBServices;
            _configuration = configuration;

        }


        //public async Task<CashCounter> GetCashCounterByCCIDAsync(int ccid)
        //{
        //    CashCounter cashCounter = null;

        //    using (var connection = new SqlConnection(_dBServices.GetBGSConn()))
        //    {
        //        using (var command = new SqlCommand("spBGS_GetCashCounterByCCID", connection))
        //        {

        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.AddWithValue("@CCID", ccid);

        //            await connection.OpenAsync();

        //            using (var reader = await command.ExecuteReaderAsync())
        //            {
        //                if (await reader.ReadAsync())
        //                {
        //                    cashCounter = new CashCounter
        //                    {

        //                        CCID = (int)reader["CCID"],
        //                        Name = reader["Name"].ToString(),
        //                        Address = reader["Address"].ToString(),
        //                        Phone = reader["Phone"].ToString(),

        //                    };
        //                }
        //            }
        //        }
        //    }

        //    return cashCounter;
        //}


        public async Task<(List<CashCounter> Data, string Message)> GetCashCounterByCCIDAsync(int ccid)
        {
            //ChallanDetails result = null;

            var result = new List<CashCounter>();

            string message = string.Empty;

            //try
            //{
            using (var connection = new SqlConnection(_dBServices.GetBGSConn()))
            {

                using (var command = new SqlCommand("spBGS_GetCashCounterByCCID", connection))
                {
                    command.CommandTimeout = 120;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CCID", ccid);
                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {

                        await reader.ReadAsync();

                        if (reader.HasRows)
                        {

                            if (reader.FieldCount == 1 && reader.GetName(0) == "Message")
                            {
                                message = string.Empty;
                            }
                            else
                            {

                                do
                                {
                                    result.Add(new CashCounter
                                    {

                                        CCID = (int)reader["CCID"],
                                        Name = reader["Name"].ToString(),
                                        Address = reader["Address"].ToString(),
                                        Phone = reader["Phone"].ToString(),

                                    });

                                } while (await reader.ReadAsync());


                                message = "Successfully Reterived";

                            }

                        }
                        else
                        {

                            message = string.Empty;

                        }

                    }
                }
            }
            //}
            //catch (SqlException ex)
            //{
            //    message = $"SQL Error: {ex.Message}";
            //}
            //catch (Exception ex)
            //{
            //    message = $"An error occurred: {ex.Message}";
            //}


            return (result, message);

        }
    }





}

