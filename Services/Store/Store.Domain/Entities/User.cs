namespace Store.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }

        public string description {  get; set; }
        //
        public UserProfile UserProfile { get; set; }

        //// ✅ One User has many Orders
        public ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}

// single responsibility principle