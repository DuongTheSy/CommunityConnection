namespace CommunityConnection.Common 
{ 
    public class ApiResponse<T>
    {
        public bool status { get; set; } = false;
        public string message { get; set; } = "Chưa xác định";
        public T? data { get; set; }
    }

    public class CheckGoalResponse
    {
        public bool status { get; set; }
        public string? result { get; set; }

    }
}
