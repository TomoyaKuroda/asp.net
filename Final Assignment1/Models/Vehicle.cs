namespace Final_Assignment1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Vehicle")]
    public partial class Vehicle
    {
        public int VehicleId { get; set; }

        public int MakeId { get; set; }

        public int ModelId { get; set; }

        public int Year { get; set; }

        public decimal Price { get; set; }

        [Column(TypeName = "date")]
        public DateTime? SoldDate { get; set; }

        public virtual Make Make { get; set; }

        public virtual Model Model { get; set; }
    }
}
