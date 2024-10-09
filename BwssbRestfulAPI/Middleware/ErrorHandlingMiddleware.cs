using BwssbRestfulAPI.DBServices;
using BwssbRestfulAPI.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace BwssbRestfulAPI.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private IConfiguration _configuration;
       
        public ErrorHandlingMiddleware(RequestDelegate next,IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;

        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }


        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {

            // Get the client's IP address
            string ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown IP";

            var errorCode = HttpStatusCode.InternalServerError; // Default error code 500
            var errorMessage = "An unexpected error occurred.";

            if (ex is SqlException sqlEx)
            {
                errorCode = HttpStatusCode.BadRequest; // For example, you can handle SQL errors with 400 code
                errorMessage = sqlEx.Message;
            }

            // Log the error to the database using stored procedure
            await LogErrorToDatabase(errorCode.ToString(), errorMessage, ex.StackTrace, context.Request.Path, ipAddress.ToString());

            // Set up response for the API
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)errorCode;
            var result = JsonSerializer.Serialize(new { error = errorMessage });
            await context.Response.WriteAsync(result);

        }

        private async Task LogErrorToDatabase(string errorCode, string errorMessage, string errorDetails, string endpoint,string ipAddress)
        {
           
                //configuration.GetConnectionString("BGS");
                var connectionString = _configuration.GetConnectionString("BGS");
               
                using (var connection = new SqlConnection(connectionString))
                {
                    using (var command = new SqlCommand("BwssB_sp_LogCommonError", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ErrorCode", errorCode);
                        command.Parameters.AddWithValue("@ErrorMessage", errorMessage);
                        command.Parameters.AddWithValue("@ErrorDetails", errorDetails ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Endpoint", endpoint ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@UserID", ipAddress ?? (object)DBNull.Value); 
                        command.Parameters.AddWithValue("@RequestData", DBNull.Value); 
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();

                    }
                }
            
           
        }

    }



    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
