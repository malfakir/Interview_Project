using Interview_Project.IServices;

namespace Interview_Project.Service
{
    public class User_Invoice : IUser_Invoice
    {
        public string UserName { get; set; }
        public string RateType { get; set; }
        public string CallDate { get; set; }
        public int Call_Duration_Minute { get; set; }
        public decimal CallCharge { get; set; }
    }
}
