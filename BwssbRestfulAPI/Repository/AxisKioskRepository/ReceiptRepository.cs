using System.Data.SqlClient;
using System.Data;
using BwssbRestfulAPI.Models.AxisKiosk;
using BwssbRestfulAPI.DBServices;

namespace BwssbRestfulAPI.Repository.AxisKioskRepository
{
    public class ReceiptRepository
    {
       
        private IDBServices _dBServices; 

        public ReceiptRepository(IDBServices dbService)
        {
            _dBServices = dbService;
        }

        //public async Task<DataTable> GetReceiptDetails(string RRNumber)
        //{
        //    string message = string.Empty;
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        using (var command = new SqlCommand("sp_BGS_GetBillAndReceiptInfo", connection))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.AddWithValue("@RRNumber", RRNumber);                 
        //            var dataTable = new DataTable();
        //            await connection.OpenAsync();
        //            using (var reader = await command.ExecuteReaderAsync())
        //            {
        //                if (reader.HasRows)
        //                {
        //                    dataTable.Load(reader);

        //                }
        //                else
        //                {
        //                    if (await reader.ReadAsync())
        //                    {
        //                        message = reader["Message"].ToString();
        //                    }
        //                }

        //            }
        //            return dataTable;
        //        }
        //    }
        //}

        public async Task<(List<ReceiptDetails> Data, string Message)> GetReceiptDetails(string rrNumber)
        {

            var result = new List<ReceiptDetails>();
            string message = string.Empty;

            //try
            //{
                using (var connection = new SqlConnection(_dBServices.GetBGSConn()))
                {

                    using (var command = new SqlCommand("sp_BGS_GetBillAndReceiptInfo", connection))
                    {
                        command.CommandTimeout = 120;
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@RRNumber", rrNumber));
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

                                    //while (await reader.ReadAsync())
                                    //{

                                    //    result.Add(new BillReceiptInfo
                                    //    {

                                    //        RRNumber = reader["RRNumber"].ToString(),
                                    //        ConsumerID = Convert.ToInt32(reader["ConsumerID"]),
                                    //        ConsumerName = reader["ConsumerName"].ToString(),
                                    //        Address = reader["Address"].ToString(),
                                    //        BillNumber = reader["BillNumber"].ToString(),
                                    //        BillAmount = Convert.ToDecimal(reader["BillAmount"]),
                                    //        BillDate = Convert.ToDateTime(reader["BillDate"]),
                                    //        BillDueDate = Convert.ToDateTime(reader["BillDueDate"]),
                                    //        SDID = reader["SDID"].ToString()

                                    //    });

                                    //}


                                    do
                                    {
                                        result.Add(new ReceiptDetails
                                        {

                                            RRNumber = reader["RRNumber"].ToString(),
                                            ConsumerID = Convert.ToInt32(reader["ConsumerID"]),
                                            ConsumerName = reader["ConsumerName"].ToString(),
                                            Address = reader["Address"].ToString(),
                                            BillNumber = reader["BillNumber"].ToString(),
                                            BillAmount = Convert.ToDecimal(reader["BillAmount"]),
                                            BillDate = Convert.ToDateTime(reader["BillDate"]),
                                            BillDueDate = Convert.ToDateTime(reader["BillDueDate"]),
                                            SDID = reader["SDID"].ToString()

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


        public int InsertReceipt(ReceiptModel receipt)
        {
            using (SqlConnection conn = new SqlConnection(_dBServices.GetBGSConn()))
            {
                using (SqlCommand cmd = new SqlCommand("spBGS_InsertAxisKioskOnlineReceipts_new", conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    cmd.Parameters.AddWithValue("@rrnumber", receipt.RrNumber);
                    cmd.Parameters.AddWithValue("@billnumber", receipt.BillNumber);
                    cmd.Parameters.AddWithValue("@billamount", receipt.BillAmount);
                    cmd.Parameters.AddWithValue("@cashier", receipt.Cashier);
                    cmd.Parameters.AddWithValue("@paydate", receipt.PayDate);
                    cmd.Parameters.AddWithValue("@ccid", receipt.Ccid);
                    cmd.Parameters.AddWithValue("@kreceiptnumber", receipt.KReceiptNumber ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@receiptnumber", receipt.ReceiptNumber ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@AmountCollected", receipt.AmountCollected);
                    cmd.Parameters.AddWithValue("@sdid", receipt.Sdid);
                    cmd.Parameters.AddWithValue("@org_sdid", receipt.OrgSdid ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@realised", receipt.Realised);
                    cmd.Parameters.AddWithValue("@userid", receipt.UserId);
                    cmd.Parameters.AddWithValue("@BatchNo", receipt.BatchNo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CollectionIdRP", receipt.CollectionIdRP ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ReceiptNumberRP", receipt.ReceiptNumberRP ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CollectionIdRC", receipt.CollectionIdRC ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@mode", receipt.Mode ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Cnumber", receipt.CNumber ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@bankId", receipt.BankId);
                    cmd.Parameters.AddWithValue("@BankBranch", receipt.BankBranch ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@AmountCollectedRC", receipt.AmountCollectedRC);
                    cmd.Parameters.AddWithValue("@Chdate", receipt.Chdate ?? (object)DBNull.Value);

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
