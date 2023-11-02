namespace QLCuaHangTienLoiV1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ImportSource")]
    public partial class ImportSource
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ImportSource()
        {
            Import = new HashSet<Import>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ImportSourceID { get; set; }

        [Required]
        [StringLength(200)]
        public string ImportSourceName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Import> Import { get; set; }
    }
}
