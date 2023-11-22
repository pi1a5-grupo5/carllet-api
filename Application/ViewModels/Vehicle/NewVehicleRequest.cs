namespace Application.ViewModels.Vehicle
{
    public class NewVehicleRequest
    {
        public Guid UserId { get; set; }
        public int? VehicleTypeId { get; set; }
        public string? VehicleTypeName { get; set; }
        public string? VehicleBrandName { get; set; }
        public short FabricationYear { get; set; }
        public int Odometer { get; set; }
        public bool Rented { get; set; }
    }
}
