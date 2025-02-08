namespace TradeProject.Core.DTO
{
    public record AccountDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}