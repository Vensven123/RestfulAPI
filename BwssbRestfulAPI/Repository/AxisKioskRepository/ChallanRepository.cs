using BwssbRestfulAPI.DBServices;
using BwssbRestfulAPI.Models.AxisKiosk;
using System.Data;
using System.Data.SqlClient;

namespace BwssbRestfulAPI.Repository.AxisKioskRepository
{
    public class ChallanRepository
    {
       
        private readonly IDBServices _dBServices;

        public ChallanRepository(IDBServices dbService)
        {
            _dBServices = dbService;
        }

        //public async Task<DataTable> GetChallanDetailsAsync(string challanNumber)
        //{
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        using (var command = new SqlCommand("spBGS_GetChallanDetails", connection))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.AddWithValue("@ChallanNumber", challanNumber);

        //            var dataTable = new DataTable();
        //            await connection.OpenAsync();
        //            using (var reader = await command.ExecuteReaderAsync())
        //            {
        //                dataTable.Load(reader);
        //            }
        //            return dataTable;
        //        }
        //    }
        //}


        public async Task<(List<ChallanDetails> Data, string Message)> GetChallanDetailsAsync(string challanNumber)
        {
            //ChallanDetails result = null;

            var result = new List<ChallanDetails>();

            string message = string.Empty;

            //try
            //{
                using (var connection = new SqlConnection(_dBServices.GetBGSConn()))
                {

                    using (var command = new SqlCommand("spBGS_GetChallanDetails", connection))
                    {
                        command.CommandTimeout = 120;
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ChallanNumber", challanNumber);
                        await connection.OpenAsync();

                        using (var reader = await command.ExecuteReaderAsync())
                        {

                            await reader.ReadAsync();

                            if (reader.HasRows)
                            {

                                if (reader.FieldCount == 1 && reader.GetName(0) == "Message")
                                {

                                    message = reader["Message"].ToString();
                                }
                                else
                                {

                                    //if (await reader.ReadAsync())
                                    //{
                                    //    result = new ChallanDetails
                                    //    {
                                    //        ChallanNumber = reader["ChallanNumber"].ToString(),
                                    //        ChallanDate = Convert.ToDateTime(reader["ChallanDate"]),
                                    //        SDID = Convert.ToInt32(reader["SDID"]),
                                    //        ConsumerName = reader["ConsumerName"].ToString(),
                                    //        Address = reader["Address"].ToString(),
                                    //        ChallanAmount = Convert.ToDecimal(reader["ChallanAmount"])
                                    //    };
                                    //}

                                    do
                                    {
                                        result.Add(new ChallanDetails
                                        {

                                            ChallanNumber = reader["ChallanNumber"].ToString(),
                                            ChallanDate = Convert.ToDateTime(reader["ChallanDate"]),
                                            SDID = reader["SDID"].ToString(),
                                            ConsumerName = reader["ConsumerName"].ToString(),
                                            Address = reader["Address"].ToString(),
                                            ChallanAmount = Convert.ToDecimal(reader["ChallanAmount"])

                                        });

                                    } while (await reader.ReadAsync());


                                    message = "Successfully Reterived";

                                }

                            }
                            else
                            {

                                message = reader["Message"]?.ToString();

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


        public int InsertChallan(ChallanModel challan)
        {
            using (SqlConnection conn = new SqlConnection(_dBServices.GetBGSConn()))
            {
                using (SqlCommand cmd = new SqlCommand("spBGS_InsertAxisKioskOnlineChallan_05_10_24", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ChallanNumber", challan.ChallanNumber);
                    cmd.Parameters.AddWithValue("@ChallanAmount", challan.ChallanAmount);
                    cmd.Parameters.AddWithValue("@CashierC", challan.CashierC);
                    cmd.Parameters.AddWithValue("@PayDateC", challan.PayDateC);
                    cmd.Parameters.AddWithValue("@ccid", challan.Ccid);
                    cmd.Parameters.AddWithValue("@KReceiptNumberC", challan.KReceiptNumberC);
                    cmd.Parameters.AddWithValue("@ReceiptNumberC", challan.ReceiptNumberC);
                    cmd.Parameters.AddWithValue("@AmountCollectedC", challan.AmountCollectedC);
                    cmd.Parameters.AddWithValue("@SdidC", challan.SdidC);
                    cmd.Parameters.AddWithValue("@RealisedC", challan.RealisedC);
                    cmd.Parameters.AddWithValue("@UserIdC", challan.UserIdC);
                    cmd.Parameters.AddWithValue("@BatchNoC", challan.BatchNoC);
                    cmd.Parameters.AddWithValue("@CollectionIdCP", challan.CollectionIdCP);
                    cmd.Parameters.AddWithValue("@ReceiptNumberCP", challan.ReceiptNumberCP);
                    cmd.Parameters.AddWithValue("@CollectionIdCC", challan.CollectionIdCC);
                    cmd.Parameters.AddWithValue("@modeC", challan.ModeC);
                    cmd.Parameters.AddWithValue("@CnumberC", challan.CnumberC ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@bankIdC", challan.BankIdC);
                    cmd.Parameters.AddWithValue("@BankBranchC", challan.BankBranchC ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@AmountCollectedCC", challan.AmountCollectedCC);
                    cmd.Parameters.AddWithValue("@ChdateC", challan.ChdateC ?? (object)DBNull.Value);

                    SqlParameter statusParam = new SqlParameter("@status", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(statusParam);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    return (int)statusParam.Value;
                }
            }
        }

    }
}
