namespace Prosperitopia.Domain.Model.Dto
{
    public class ItemDto
    {
        public long? Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal? Price { get; set; }
        public string Category { get; set; } = "";
    }
}
