using Interview_Project.Service;
using Microsoft.AspNetCore.Mvc;

namespace Interview_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CDRController : ControllerBase
    {
      

        [HttpPost("upload")]
        public string UploadFileAsync(IFormFile file)
        {
            string Calls = "";
            if (file != null && file.Length > 0)
            {
               CSV_Service cSV_Service = new CSV_Service();
                Calls = cSV_Service.Read_CDR(file);
            }
            return Calls;
        }
    }

   
}