using Microsoft.AspNetCore.Http;
namespace newproject.Models.Models
{
    public class Vm_AboutUSItem
    {
        public int Vm_Id { get; set; }
        public string Vm_Title { get; set; }
        public string Vm_Detail { get; set; }
        public string Vm_Icon { get; set; }
        public int Vm_Number { get; set; }
        public bool Vm_Status { get; set; }
    }
}