using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Odev.Models.Model
{
    [Table("Kimlik")]
    public class Kimlik
    {
        public int KimlikId { get; set; }

        [DisplayName("Site Başlık")]
        [Required,StringLength(100,ErrorMessage ="En fazla 100 karakter olabilir!")]
        public string Title { get; set; }

        [DisplayName("Anahtar Kelimeler")]
        [Required, StringLength(200, ErrorMessage = "En fazla 200 karakter olabilir!")]
        public string Keywords { get; set; }

        [DisplayName("Site Açıklama")]
        [Required, StringLength(300, ErrorMessage = "En fazla 300 karakter olabilir!")]
        public string Description { get; set; }

        [DisplayName("Site Logo")]
        public string LogoURL { get; set; }

        [DisplayName("Site Unvan")]
        public string Unvan { get; set; }
    }
}