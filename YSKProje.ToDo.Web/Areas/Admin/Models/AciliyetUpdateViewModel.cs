using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YSKProje.ToDo.Web.Areas.Admin.Models
{
    public class AciliyetUpdateViewModel
    {
        [Display(Name="Tanım:")]
        public int Id { get; set; }
        [Required(ErrorMessage ="Tanım Alanı Gereklidir.")]
        public string Tanim { get; set; }
    }
}
