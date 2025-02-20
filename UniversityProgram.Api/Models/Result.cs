namespace UniversityProgram.Api.Models
{
    public class Result
    {
        public Result(bool success, string errorMessage)
        {
            Success = success;
            Message = errorMessage;
        }
        public bool Success { get; set;}
        public string Message { get; set; } = default!;
    }
}
