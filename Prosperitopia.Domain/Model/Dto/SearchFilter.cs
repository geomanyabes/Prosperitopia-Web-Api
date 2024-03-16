using Prosperitopia.Domain.Model.Enum;

namespace Prosperitopia.Domain.Model.Dto
{
    public class SearchFilter
    {
        public string? Search { get; set; }
        public SearchType? SearchType { get; set; } = Enum.SearchType.EXACT;
    }
}
