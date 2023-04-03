using Interview_Project.Service;

namespace Interview_Project.IServices
{
    public interface ICompany_Invoice
    {
        public string CompanyName { get; set; }
        public string PlanName { get; set; }
        public List<User_Invoice> Users { get; set; }

        public decimal Company_Charge { get; set; }
    }
}
