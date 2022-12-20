using System.Drawing.Printing;

namespace WillowBatMarketWebApiService.Models
{
    public class ResponseModel
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public int TotalRecords { get; set; }
        public ResponseModel()
        {
            Success = true;
            Status = 200;
            Message = "Success";
        }
    }
}
