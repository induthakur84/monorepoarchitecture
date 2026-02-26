namespace Store.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }

        //navigation
        public UserProfile UserProfile { get; set; }

        //one User has many Orders

        public ICollection<Order> Orders { get; set; } =new List<Order>();
    }
}


//many  to many relationship