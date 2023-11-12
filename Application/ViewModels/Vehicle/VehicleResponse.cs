namespace Application.ViewModels.Vehicle
{
    public class VehicleResponse
    {
        public string UserVehicleId { get; set; }
        public string VehicleTypeName { get; set; }
        public string VehicleBrandName { get; set;}
        public string VehicleId { get; set; }   
        public short FabricationDate { get; set; }
        public int Odometer { get; set; }
        public bool Rented { get; set; }
    }
}
    
