using Microsoft.AspNetCore.Http;
namespace newproject.Models.Models
{
    public class Vm_Header
    {
        public int Vm_Id { get; set; }
        public string Vm_Image { get; set; }
        public IFormFile Img { get; set; }
        public string Vm_Title { get; set; }
        public string Vm_Detail { get; set; }
    }
}