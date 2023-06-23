namespace Application.Requests.Vehicle
{
    public class NewVehicleRequest
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public short FabricationYear { get; set; }
        public int Odometer { get; set; }
        public bool Rented { get; set; }
    }
}
