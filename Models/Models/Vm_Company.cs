using Microsoft.AspNetCore.Http;
namespace newproject.Models.Models
{
    public class Vm_Company
    {
        public int Vm_Id { get; set; }
        public string Vm_Image { get; set; }
        public IFormFile  Vm_Img { get; set; }
        public string Vm_Title { get; set; }
        public bool Vm_Status { get; set; }
    }
}