using Domain.Entities;
using Domain.Entities.VehicleNS;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.ViewModels.Vehicle
{
    public class VehicleResponse
    {
        public Guid UserVehicleId { get; set; }
        public Guid VehicleId { get; set; }
        public int VehicleTypeId { get; set; }
        public short FabricationDate { get; set; }
        public int Odometer { get; set; }
        public bool Rented { get; set; }
    }
}