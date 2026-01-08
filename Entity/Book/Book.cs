using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace design_pattern_case_1.Entity.Book
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [ForeignKey(nameof(Bundle))]
        public int? BundleId { get; set; }
        public Bundle? Bundle { get; set; }
    }
}
