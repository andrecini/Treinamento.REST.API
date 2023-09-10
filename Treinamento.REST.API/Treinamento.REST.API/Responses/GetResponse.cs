namespace Treinamento.REST.API.Responses
{
    public class GetResponse<T> : BaseResponse
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalAmount { get; set; }
        public IEnumerable<T> Users { get; set; }
    }
}
