using System.ComponentModel.DataAnnotations.Schema;

namespace design_pattern_case_1.Features.Bundles.Domain
{
    public class Bundle
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [ForeignKey(nameof(ParentBundle))]
        public int? BundleId { get; set; }
        public Bundle? ParentBundle { get; set; }

        public ICollection<Bundle> ChildBundles { get; set; } = new List<Bundle>();
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
