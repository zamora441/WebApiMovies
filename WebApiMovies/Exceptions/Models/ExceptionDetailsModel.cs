using System.Text.Json;

namespace WebApiMovies.Exceptions.Models
{
    public class ExceptionDetailsModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = null!;
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

    }
}
