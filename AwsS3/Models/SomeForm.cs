using Microsoft.AspNetCore.Http;

namespace AwsS3.Models
{
    public class SomeForm
   {
        public string Name { get; set; }
 
        public IFormFile File { get; set; }
    }
}
