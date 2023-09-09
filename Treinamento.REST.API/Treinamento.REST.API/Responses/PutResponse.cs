namespace Treinamento.REST.API.Responses
{
    public class PutResponse<T> : BaseResponse
    {
        public string URI { get; set; }
        public T UpdatedUser { get; set; }
    }
}
