namespace QLCuaHangTienLoiV1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ImportDetail")]
    public partial class ImportDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RequestNumber { get; set; }

        public int ImportSourceID { get; set; }

        public int ProductID { get; set; }

        public int ImportAmount { get; set; }

        public int Total { get; set; }

        [Column(TypeName = "date")]
        public DateTime ImportDate { get; set; }

        public virtual Import Import { get; set; }
    }
}
