namespace Store.Domain.DTO.Response
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        // WE CAN THE Fields from User Navigation Property
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public DateTime OrderCreatedAt { get; set; }
    }
}
