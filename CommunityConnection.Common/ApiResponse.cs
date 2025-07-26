namespace CommunityConnection.WebApi.Models
{
    public class ApiResponse<T>
    {
        public string status { get; set; } = "false";
        public string message { get; set; } = "Chưa xác định";
        public T? data { get; set; }
    }

    public class CheckGoalResponse
    {
        public string status { get; set; }
        public string? result { get; set; }
    }
}
