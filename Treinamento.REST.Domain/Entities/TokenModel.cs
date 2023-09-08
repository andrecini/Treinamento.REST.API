namespace Treinamento.REST.Domain.Entities
{
    public class TokenModel
    {
        public string? Token { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
