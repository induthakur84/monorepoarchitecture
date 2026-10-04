namespace Store.Domain.DTO.Response
{
    public class UserProfileResponse
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public string UserAddress { get; set; } = string.Empty;
        public string UserPhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        // ⭐ Optional (Very Useful for Search + Display)
        public string? UserName { get; set; }

        public string? Email { get; set; }
    }
}

// how to any field nullable
//=null
