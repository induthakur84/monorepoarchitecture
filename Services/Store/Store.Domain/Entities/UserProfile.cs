namespace Store.Domain.Entities
{
    public class UserProfile
    {
        //pk
        public int Id { get; set; }

        //fk
        public int UserId { get; set; }

        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        //navigation
        public User User { get; set; }
    }
}
