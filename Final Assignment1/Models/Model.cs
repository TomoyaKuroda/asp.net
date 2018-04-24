namespace Final_Assignment1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Model")]
    public partial class Model
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Model()
        {
            Vehicle = new HashSet<Vehicle>();
        }

        public int ModelId { get; set; }

        public int Engine_Size { get; set; }

        [Column("Number of Doors")]
        public int Number_of_Doors { get; set; }

        [Required]
        [StringLength(20)]
        public string Colour { get; set; }

        public int VehicleTypeId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vehicle> Vehicle { get; set; }

        public virtual VehicleType VehicleType { get; set; }
    }
}
