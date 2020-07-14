using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.Areas.Admin.Models;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class IsEmriController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IGorevService _gorevService;
        private readonly UserManager<AppUser> _userManager;
        public IsEmriController(IAppUserService appUserService, IGorevService gorevService, UserManager<AppUser> userManager)
        {
            _gorevService = gorevService;
            _appUserService = appUserService;
            _userManager = userManager;

        }
        //rolü admin olmayan kullanıcıları listelicez.Bunun icin appuserrepostiroy ve ıappuserdal interface ini oluşturduk.
        public IActionResult Index()
        {
            TempData["Active"] = "isemri";
            //var model = _appUserService.GetirAdminOlmayanlar();

            List<Gorev> gorevler = _gorevService.GetirTumTablolarla();

            List<GorevListAllViewModel> models = new List<GorevListAllViewModel>();
            foreach (var item in gorevler)
            {
                GorevListAllViewModel model = new GorevListAllViewModel();
                model.Id = item.Id;
                model.Aciklama = item.Aciklama;
                model.Aciliyet = item.Aciliyet;
                model.Ad = item.Ad;
                model.AppUser = item.AppUser;
                model.OlusturulmaTarih = item.OlusturulmaTarih;
                model.Raporlar = item.Raporlar;
                models.Add(model);
            }
            return View(models);
        }


        public IActionResult Detaylandir(int id)
        {
            TempData["Active"] = "isemri";
            var gorev=_gorevService.GetirRaporlarileId(id);
            GorevListAllViewModel model = new GorevListAllViewModel();
            model.Id = gorev.Id;
            model.Raporlar = gorev.Raporlar;
            model.Ad = gorev.Ad;
            model.Aciklama = gorev.Aciklama;
            model.AppUser = gorev.AppUser;
            return View(model);
        }

        //ikinci parametre standart olarak s verilir(search den gelir.)
        //Genelde şu şekilde olur : //ysk.com.tr/?s=asiye(aramaya asiye girildigi zaman)
        public IActionResult AtaPersonel(int id,string s,int sayfa=1)
        {
            TempData["Active"] = "isemri";

            ViewBag.AktifSayfa = sayfa;
            //toplamda admin olmayan kullanıcı sayımızı bulduk.Burdan toplam sayfa sayısına geçicez.
            //Bir sayfada 3 tane gösterilsin dediğimiz için 3'e bölerek sayfa sayısına ulasırız.
            //Ancak son sayfada 3 tane yerine 1 tane kalmıs olabilicegi icin double ile küsüratı ögrenip ceiling ile üste yuvarlıyoruz en sonda int'a cast ediyoruz.
            //ViewBag.ToplamSayfa = (int)Math.Ceiling((double)
            //    _appUserService.GetirAdminOlmayanlar().Count / 3);
            ViewBag.Aranan = s;
            int toplamSayfa;
            var gorev = _gorevService.GetirAciliyetileId(id);
 
            var personeller = _appUserService.GetirAdminOlmayanlar(out toplamSayfa,s,sayfa);

            ViewBag.ToplamSayfa = toplamSayfa;

            List<AppUserListViewModel> appUserListModel = new List<AppUserListViewModel>();
            foreach (var item in personeller)
            {
                AppUserListViewModel model = new AppUserListViewModel();
                model.Id = item.Id;
                model.Name = item.Name;
                model.SurName = item.Surname;
                model.Email = item.Email;
                model.Picture = item.Picture;
                appUserListModel.Add(model);
            }
            ViewBag.Personeller = appUserListModel;

            GorevListViewModel gorevModel = new GorevListViewModel();
            gorevModel.Id = gorev.Id;
            gorevModel.Ad = gorev.Ad;
            gorevModel.Aciklama = gorev.Aciklama;
            gorevModel.Aciliyet = gorev.Aciliyet;
            gorevModel.OlusturulmaTarih = gorev.OlusturulmaTarih;
            return View(gorevModel );
        }

        [HttpPost]
        public IActionResult AtaPersonel(PersonelGorevlendirViewModel model)
        {
           var guncellenecekGorev= _gorevService.GetirIdile(model.GorevId);
            guncellenecekGorev.AppUserId = model.PersonelId;
            _gorevService.Guncelle(guncellenecekGorev);
            return RedirectToAction("Index");
        }




        public IActionResult GorevlendirPersonel(PersonelGorevlendirViewModel model)
        {
            TempData["Active"] = "isemri";
            var user =_userManager.Users.FirstOrDefault(I => I.Id == model.PersonelId);
            var gorev = _gorevService.GetirAciliyetileId(model.GorevId);

            AppUserListViewModel userModel = new AppUserListViewModel();
            userModel.Id = user.Id;
            userModel.Name = user.Name;
            userModel.Picture = user.Picture;
            userModel.SurName = user.Surname;
            userModel.Email = user.Email;


            GorevListViewModel gorevModel = new GorevListViewModel();
            gorevModel.Id = gorev.Id;
            gorevModel.Aciklama = gorev.Aciklama;
            gorevModel.Aciliyet = gorev.Aciliyet;
            gorevModel.Ad = gorev.Ad;


            PersonelGorevlendirListViewModel personelGorevlendirModel = new PersonelGorevlendirListViewModel();
            personelGorevlendirModel.AppUser = userModel;
            personelGorevlendirModel.Gorev = gorevModel;
            return View(personelGorevlendirModel);
        }

    }
}
