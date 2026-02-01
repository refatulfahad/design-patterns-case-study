using design_pattern_case_1.Features.Bundles.Domain;

namespace design_pattern_case_1.Features.Bundles.Domain.Pricing
{
    public class BookComponent : IPricingComponent
    {
        private readonly Book _book;

        public BookComponent(Book book)
        {
            _book = book;
        }

        public string GetDetails() => _book.Name;
    }
}
