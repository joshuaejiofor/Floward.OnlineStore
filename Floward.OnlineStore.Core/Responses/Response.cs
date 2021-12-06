namespace Floward.OnlineStore.Core.Responses
{
    public class Response<T>
    {
        public Response()
        {
            HttpStatusCode = 200;
        }

        public Response(T data)
        {
            Succeeded = true;
            Message = string.Empty;
            Error = string.Empty;
            Data = data;
            TotalRecords = 1;
            HttpStatusCode = 200;
        }

        public Response(T data, int totalRecords)
        {
            Succeeded = true;
            Message = string.Empty;
            Error = string.Empty;
            Data = data;
            TotalRecords = totalRecords;
            HttpStatusCode = 200;
        }
        public Response(T data, string error, int httpStatusCode, bool succeeded = false, int totalRecords = 0)
        {
            Succeeded = succeeded;
            Error = error;
            Data = data;
            TotalRecords = totalRecords;
            HttpStatusCode = httpStatusCode;
        }

        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
        public int TotalRecords { get; set; }
        public int HttpStatusCode { get; set; }
    }
}
