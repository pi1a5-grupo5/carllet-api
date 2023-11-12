using System.ComponentModel.DataAnnotations.Schema;

namespace Application.ViewModels.Vehicle
{
    public class VehicleTypeResponse
    {
        public int VehicleTypeId { get; set; }
        public string Name { get; set; }
        public int VehicleBrandId { get; set; }
    }
}
