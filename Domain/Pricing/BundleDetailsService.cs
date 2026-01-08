using design_pattern_case_1.Entity.Book;

namespace design_pattern_case_1.Domain.Pricing
{
    public class BundleDetailsService
    {
        public string GetDetails(Bundle bundle)
        {
            var root = BuildTree(bundle);
            return "Bundle Details:\n" + root.GetDetails();
        }

        private BundleComponent BuildTree(Bundle bundle)
        {
            var component = new BundleComponent();

            // Add books
            foreach (var book in bundle.Books)
            {
                component.Add(new BookComponent(book));
            }

            // Add child bundles recursively
            foreach (var child in bundle.ChildBundles)
            {
                component.Add(BuildTree(child));
            }

            return component;
        }
    }
}
