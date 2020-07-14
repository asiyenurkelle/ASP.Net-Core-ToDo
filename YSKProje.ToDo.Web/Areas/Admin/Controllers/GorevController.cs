﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using YSKProje.ToDo.Business.Interfaces;
using YSKProje.ToDo.Entities;
using YSKProje.ToDo.Entities.Concrete;
using YSKProje.ToDo.Web.Areas.Admin.Models;

namespace YSKProje.ToDo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class GorevController : Controller
    {
        private readonly IGorevService _gorevService;
        private readonly IAciliyetService _aciliyetService;
        public GorevController(IGorevService gorevService, IAciliyetService aciliyetService)
        {
            _aciliyetService = aciliyetService;
            _gorevService = gorevService;

        }
        public IActionResult Index()
        {
            TempData["Active"] = "gorev";
            List<Gorev> gorevler = _gorevService.GetirAciliyetIleTamamlanmayan();
            List<GorevListViewModel> models = new List<GorevListViewModel>();
            foreach (var item in gorevler)
            {
                GorevListViewModel model = new GorevListViewModel
                {
                    Aciklama = item.Aciklama,
                    Aciliyet = item.Aciliyet,
                    AciliyetId = item.AciliyetId,
                    Ad = item.Ad,
                    Durum = item.Durum,
                    Id = item.Id,
                    OlusturulmaTarih = item.OlusturulmaTarih
                };
                models.Add(model);
            }
            return View(models);
        }
        public IActionResult EkleGorev()
        {
            //Tempdata active ile seçili olan sayfanın aktif renk almasını sagladık(yandaki kayan menüden)
            TempData["Active"] = "gorev";
            //ViewBag ile aciliyet seceneklerini seçenek kutusuna getiricez.
            //Selectlist ile 2.parametrede değerini atadık,3.parametrede kullanıcıya gösterilecek olan kısmı atadık.
            ViewBag.Aciliyetler = new SelectList(_aciliyetService.GetirHepsi(), "Id", "Tanim");
            return View(new GorevAddViewModel());
        }

        [HttpPost]
        public IActionResult EkleGorev(GorevAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                _gorevService.Kaydet(new Gorev
                {
                    Aciklama = model.Aciklama,
                    Ad = model.Ad,
                    AciliyetId = model.AciliyetId,

                });
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult GuncelleGorev(int id)
        {
            TempData["Active"] = "gorev";
            var gorev = _gorevService.GetirIdile(id);
            GorevUpdateViewModel model = new GorevUpdateViewModel
            {
                Id = gorev.Id,
                Aciklama = gorev.Aciklama,
                AciliyetId = gorev.AciliyetId,
                Ad = gorev.Ad
            };
            //Son parametre seçili olarak gelmesini istediğimiz değer.
            ViewBag.Aciliyetler = new SelectList(_aciliyetService.GetirHepsi(), "Id", "Tanim", gorev.AciliyetId);
            return View(model);
        }

        [HttpPost]
        public IActionResult GuncelleGorev(GorevUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                _gorevService.Guncelle(new Gorev()
                {
                    Id = model.Id,
                    Aciklama = model.Aciklama,
                    AciliyetId = model.AciliyetId,
                    Ad = model.Ad
                });
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult SilGorev(int id )
        {
            _gorevService.Sil(new Gorev { Id = id });
            return Json(null);
        }
    }
}
