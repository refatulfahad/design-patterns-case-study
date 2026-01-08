namespace design_pattern_case_1.Domain.Pricing
{
    public class BundleComponent: IPricingComponent
    {
        private readonly List<IPricingComponent> _children = new();

        public void Add(IPricingComponent component)
        {
            _children.Add(component);
        }

        public string GetDetails()
        {
            string details = "";

            foreach (var child in _children)
            {
                details = details + child.GetDetails() + "\n";
            }

            return details;
        }
    }
}
