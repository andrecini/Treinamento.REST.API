namespace Treinamento.REST.API.Responses
{
    public class GetResponse<T> : BaseResponse
    {
        public IEnumerable<T> Users { get; set; }
    }
}
