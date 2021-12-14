using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Odev.Models.Model
{
    [Table("Urun")]
    public class Urun
    {
        [Key]
        public int UrunId { get; set; }
        [Required,StringLength(150,ErrorMessage ="En fazla 150 karakter olabilir!")]
        [DisplayName("Ürün Başlık")]
        public string Baslik { get; set; }
        [DisplayName("Ürün Açıklama")]
        public string Aciklama { get; set; }
        [DisplayName("Ürün Resim")]
        public string ResimURL { get; set; }
    }
}