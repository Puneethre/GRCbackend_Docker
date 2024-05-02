namespace GRCServices.Models
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; } = 200;
        public string[]? Errors { get; set; }
    }
}
