namespace Drivr.Models
{
    public class ApiResponse<T> where T: class 
    {
        public T Object { get; set; }
        public string Error { get; set; }
    }
}