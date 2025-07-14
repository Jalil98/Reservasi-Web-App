using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ReservasiRuangApp.Models
{
    public class Reservasi
    {
        public int Id { get; set; }

        [Required]
        public string NamaPemesan { get; set; }

        [Required]
        [Display(Name = "Ruangan")]
        public string NamaRuangan { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "waktu mulai")]
        public DateTime WaktuMulai { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "waktu selesai")]
        public DateTime WaktuSelesai { get; set; }
            
        public string Keterangan { get; set; }
    }
}
