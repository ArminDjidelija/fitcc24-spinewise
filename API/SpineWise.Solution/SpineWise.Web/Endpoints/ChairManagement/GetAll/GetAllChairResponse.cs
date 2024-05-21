using SpineWise.ClassLibrary.Models;

namespace SpineWise.Web.Endpoints.ChairManagement.GetAll
{
    public class GetAllChairResponse
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public DateTime DateOfCreating { get; set; }
        public ChairModel ChairModel { get; set; }
        public UserGetVm User { get; set; }
    }

    public class UserGetVm
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
