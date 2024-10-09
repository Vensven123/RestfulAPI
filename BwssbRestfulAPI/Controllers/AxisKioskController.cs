using BwssbRestfulAPI.DBServices;
using BwssbRestfulAPI.Models.AxisKiosk;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using BwssbRestfulAPI.Repository.AxisKioskRepository;

namespace BwssbRestfulAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AxisKioskController : ControllerBase
    {
        private readonly IDBServices _DBService;
        private readonly IConfiguration _configuration;
        private readonly ChallanRepository _challanRepository;
        private readonly ReceiptRepository _receiptRepository;

        public AxisKioskController(IDBServices dbService,IConfiguration configuration,ChallanRepository challanRepository,ReceiptRepository receiptRepository) {

            _DBService = dbService;
            _configuration = configuration;
            _challanRepository = challanRepository;
            _receiptRepository = receiptRepository;
        }


        //----Challanservice

        [HttpGet("GetChallanDetails")]
        public async Task<IActionResult> GetChallanDetails([FromQuery] ChallanRequest request)
        {

            string BwsspKey = _configuration["Appsettings:BwssBKey"];

            if (request == null || string.IsNullOrEmpty(request.ChallanNumber) || string.IsNullOrEmpty(request.BWSSBKey))
            {
                return BadRequest("Invalid request.Please check the Parameters");
            }

            if (request.BWSSBKey != BwsspKey)
            {
                return BadRequest("BwsspKey is not matching...!");
            }

            var (data, message) = await _challanRepository.GetChallanDetailsAsync(request.ChallanNumber);

            if (data == null || data.Count == 0)
            {
                return NotFound(new { message = "Records Not Found...!" });
            }


            return Ok(data);

        }




        [HttpPost("Insert_Challan")]
        public IActionResult Insert_ChallanTransactions([FromBody] ChallanModel challanModel)
        {

            if (!ModelState.IsValid)
            {
                // Create a custom error response format
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                               .Select(e => e.ErrorMessage)
                                               .ToList();
                return BadRequest(new { Success = false, Errors = errors });
            }

                int status = _challanRepository.InsertChallan(challanModel);
                return Ok(new { Status = status });
           

        }



        //---Receiptservice

        [HttpGet("GetReceiptDetails")]
        public async Task<IActionResult> GetReceiptDetails([FromQuery] ReceiptRequest request)
        {

            string BwsspKey = _configuration["Appsettings:BwssBKey"];

            if (request == null || string.IsNullOrEmpty(request.RrNumber) || string.IsNullOrEmpty(request.BWSSBKey))
            {
                return BadRequest("Invalid request.");
            }

            if (request.BWSSBKey != BwsspKey)
            {
                return BadRequest("BwsspKey is not matching...!");
            }

            var (data, message) = await _receiptRepository.GetReceiptDetails(request.RrNumber);


            //if (!string.IsNullOrEmpty(message))
            //{
            //    // If the stored procedure returned a message, return it as a 404 response
            //    return NotFound(new { message });
            //}

            //if (data == null || data.Count == 0)
            //{
            //    return NotFound(new { message });
            //}


            return Ok(data);
        }


        [HttpPost("Insert_Receipt")]
        public IActionResult Insert_ReceiptTransactions([FromBody] ReceiptModel receiptModel)
        {

            if (!ModelState.IsValid)
            {
                // Create a custom error response format
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                               .Select(e => e.ErrorMessage)
                                               .ToList();
                return BadRequest(new { Success = false, Errors = errors });
            }

                int status = _receiptRepository.InsertReceipt(receiptModel);
                return Ok(new { Status = status });
           
        }
    }
}
