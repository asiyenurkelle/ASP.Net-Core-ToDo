using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YSKProje.ToDo.Web.Models
{
    public class AppUserAddViewModel
    {
        [Required(ErrorMessage = "Kullanıcı Adı Boş Geçilemez.")]
        [Display(Name="Kullanıcı Adı:")]
        public string UserName { get; set; }

        [Display(Name = "Şifre:")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre Boş Geçilemez.")]
        public string Password { get; set; }

        [Display(Name="Şifrenizi Tekrar Giriniz:")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Parolalar Eşleşmiyor.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Email:")]
        [EmailAddress(ErrorMessage ="Geçersiz email adresi")]
        [Required(ErrorMessage = "Email  Boş Geçilemez.")]
        public string Email { get; set; }

        [Display(Name="Ad:")]
        [Required(ErrorMessage = "Ad  Boş Geçilemez.")]
        public string Name { get; set; }

        [Display(Name="Soyad:")]
        [Required(ErrorMessage = "Soyad Boş Geçilemez.")]
        public string Surname { get; set; }

    }
}
