using Interview_Project.IServices;

namespace Interview_Project.Service
{
    public class Company_Invoice : ICompany_Invoice
    {
        public string CompanyName { get; set; }
        public string PlanName { get; set; }
        public List<User_Invoice> Users { get; set; }      
        public decimal Company_Charge { get; set; }


    }
}
