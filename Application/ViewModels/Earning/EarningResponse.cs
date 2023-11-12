namespace Application.ViewModels.Earning
{
    public class EarningResponse
    {   
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public DateTime InsertionDateTime { get; set; }
        public double EarningValue { get; set; }
    }
}
