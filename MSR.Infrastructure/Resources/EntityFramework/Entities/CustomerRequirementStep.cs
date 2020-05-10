using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(CustomerRequirementStep))]
    public partial class CustomerRequirementStep
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustomerRequirementId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string ObjectId { get; set; }

        public string Process { get; set; }

        public int? Step { get; set; }
    }
}
