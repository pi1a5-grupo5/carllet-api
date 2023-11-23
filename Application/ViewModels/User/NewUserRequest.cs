namespace Application.ViewModels.User
{
    public class NewUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DeviceId { get; set; }
        public string? ImageName { get; set; }
    }
}
