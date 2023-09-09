namespace Treinamento.REST.API.Responses
{
    public class PostResponse<T> : BaseResponse
    {
        public string URI { get; set; }
        public T CreatedUser { get; set; }
    }
}
