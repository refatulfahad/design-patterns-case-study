using System.ComponentModel.DataAnnotations;

namespace design_pattern_case_1.Entity
{
    public class AppConfig
    {
        [Key]
        public int Id { get; set; }
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string ValueType { get; set; } = string.Empty;
    }
}
