using System.ComponentModel.DataAnnotations.Schema;

namespace Application.ViewModels.Vehicle
{
    public class VehicleTypeRequest
    {
        public string Name { get; set; }
        public int VehicleBrandId { get; set; }
    }
}