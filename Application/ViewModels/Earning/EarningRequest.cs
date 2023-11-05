using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Application.ViewModels.Earning
{
    public class EarningRequest
    {
        public Guid OwnerId{ get; set; }
        public DateTime InsertionDateTime { get; set; }
        public double EarningValue { get; set; }
    }
}