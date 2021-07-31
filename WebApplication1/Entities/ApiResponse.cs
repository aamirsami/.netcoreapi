namespace WebApplication1.Entities
{
    public class ApiResponse
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
