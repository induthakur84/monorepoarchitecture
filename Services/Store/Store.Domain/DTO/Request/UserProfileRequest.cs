namespace Store.Domain.DTO.Request
{
    public class UserProfileRequest
    {
        public int UserId { get; set; }

        public string Address { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
    }
}