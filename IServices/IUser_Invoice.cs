namespace Interview_Project.IServices
{
    public interface IUser_Invoice
    {
        int Call_Duration_Minute { get; set; }
        decimal CallCharge { get; set; }
        string CallDate { get; set; }
        string RateType { get; set; }
        string UserName { get; set; }
    }
}