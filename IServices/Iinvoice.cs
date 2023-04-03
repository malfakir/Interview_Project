namespace Interview_Project.IServices
{
    public interface Iinvoice
    {
        int Calltime(int CallTime);
        string GenerateCharges();
        string MatchRegexCallType(string phoneNumber);
        decimal MatchRegexFilter(string phoneNumber, Guid PlanID);
    }
}