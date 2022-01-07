namespace AutoLotModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DoctorAppointment")]
    public partial class DoctorAppointment
    {
        [Key]
        public int Id_Doctor { get; set; }

        [Column("Data&Ora")]
        public DateTime? Data_Ora { get; set; }

        [StringLength(50)]
        public string Detalii { get; set; }

        [StringLength(50)]
        public string Nume_pacient { get; set; }
    }
}
