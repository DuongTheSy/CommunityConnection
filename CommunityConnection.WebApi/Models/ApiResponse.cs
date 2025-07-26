namespace CommunityConnection.WebApi.Models
{
    public class ApiResponse
    {
        public string status { get; set; } = "false";
        public string message { get; set; } = "Chưa xác định";
        public Data? data { get; set; }
    }

    public class Data
    {
        public string status { get; set; }
        public string? result { get; set; }
    }
}
