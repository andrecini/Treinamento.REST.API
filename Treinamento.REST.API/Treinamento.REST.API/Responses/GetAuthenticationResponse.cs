namespace Treinamento.REST.API.Responses
{
    public class GetAuthenticationResponse<T> : BaseResponse
    {
        public T Auth { get; set; }
    }
}
