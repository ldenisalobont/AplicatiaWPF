namespace AutoLotModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DoctorSchedule")]
    public partial class DoctorSchedule
    {
        [Key]
        public int Id_Doctor { get; set; }

        [Column("Data&Ora_sosirii")]
        public DateTime? Data_Ora_sosirii { get; set; }

        [Column("Data&Ora_plecarii")]
        public DateTime? Data_Ora_plecarii { get; set; }

        [StringLength(50)]
        public string Nume_Medic { get; set; }
    }
}
