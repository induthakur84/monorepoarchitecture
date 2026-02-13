namespace SharedModel.Response
{
    public class ErrorResponse
    {
        public ErrorResponse(int StatusCode, string message)
        {
            statusCode = StatusCode;

            // "Anil note"
            messages = (message.Split(","));
        }
        public int statusCode { get; set; }
        public string[] messages { get; set; }
    }
}
