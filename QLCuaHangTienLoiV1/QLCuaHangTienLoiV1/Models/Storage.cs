namespace QLCuaHangTienLoiV1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Storage")]
    public partial class Storage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Area { get; set; }

        public int? ProductID { get; set; }

        public int StockAmount { get; set; }

        public int MaxStock { get; set; }

        public virtual Product Product { get; set; }
    }
}
