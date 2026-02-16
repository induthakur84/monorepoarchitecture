namespace Store.Domain.DTO.Response
{
    public class UserProfileResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserAddress { get; set; } = string.Empty;
        public string UserPhoneNumber { get; set; } = string.Empty;

        // here we make username nullable
        // it optional
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
    }
}

// how to any field nullable
//=null
