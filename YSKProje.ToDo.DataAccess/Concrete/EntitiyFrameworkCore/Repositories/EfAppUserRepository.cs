using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.XPath;
using YSKProje.ToDo.DataAccess.Concrete.EntitiyFrameworkCore.Contexts;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Concrete.EntitiyFrameworkCore.Repositories
{
    public class EfAppUserRepository :  IAppUserDal
    {
        public List<AppUser> GetirAdminOlmayanlar()
        {
            using var context = new ToDoContext();
            //aşağıdaki sorguyu yazmaya calısıcaz.
            /*select*from aspnetusers inner join aspnetuserroles 
            on aspnetusers.ıd=aspnetuserroles.userıd
            inner join aspnetroles
            on aspnetuserroles.roleıd=aspnetroles.ıd where aspnetroles.name='Member'*/

            return context.Users.Join(context.UserRoles, user => user.Id, userRole => userRole.UserId, (resultUser, resultUserRole) => new
            {
                //user ile userroles tablolarını birleştiricez.İkinci parametrede tabloların hangi alanlarının eşleşeceğini verdik.
                //tablolar birleştirdikten sonra resultuser ve resultuserrole isimlerini kendimiz verdik.
                //aşağıdaki user yerine a falanda diyebiliriz.
                user = resultUser,
                userRole = resultUserRole
                //bu sefer iki tablonun birleşiminden gelen değerleri roles tablosuyla birleştiricez.İkinci parametrede roleıd ile ıd alanlarını birleştirdiğimizi bildirdik.
            }).Join(context.Roles, twoTableResult => twoTableResult.userRole.RoleId, role => role.Id,
            (resultTable, resultRole) => new
            {
                user = resultTable.user,
                userRoles = resultTable.userRole,
                roles = resultRole

                //ARTIK BURDAN SONRA ELİMİZDEKİLER 3 TABLONUN BİRLEŞİMİDİR.
            }).Where(I => I.roles.Name == "Member").Select(I => new AppUser()
            {
                Id=I.user.Id,
                Name=I.user.Name,
                Surname=I.user.Surname,
                Picture=I.user.Picture,
                Email=I.user.Email,
                UserName=I.user.UserName
                //artık sql sorgumuzun bittiğini bildirmek icin tolist ile sorguyu kapattık.
            }).ToList();
        }
        //override edicez.
        public List<AppUser> GetirAdminOlmayanlar( out int toplamSayfa,string aranacakKelime, int aktifSayfa=1)
        {
            using var context = new ToDoContext();
      
            var result= context.Users.Join(context.UserRoles, user => user.Id, userRole => userRole.UserId, (resultUser, resultUserRole) => new
            {
                //user ile userroles tablolarını birleştiricez.İkinci parametrede tabloların hangi alanlarının eşleşeceğini verdik.
                //tablolar birleştirdikten sonra resultuser ve resultuserrole isimlerini kendimiz verdik.
                //aşağıdaki user yerine a falanda diyebiliriz.
                user = resultUser,
                userRole = resultUserRole
                //bu sefer iki tablonun birleşiminden gelen değerleri roles tablosuyla birleştiricez.İkinci parametrede roleıd ile ıd alanlarını birleştirdiğimizi bildirdik.
            }).Join(context.Roles, twoTableResult => twoTableResult.userRole.RoleId, role => role.Id,
            (resultTable, resultRole) => new
            {
                user = resultTable.user,
                userRoles = resultTable.userRole,
                roles = resultRole

                //ARTIK BURDAN SONRA ELİMİZDEKİLER 3 TABLONUN BİRLEŞİMİDİR.
            }).Where(I => I.roles.Name == "Member").Select(I => new AppUser()
            {
                Id = I.user.Id,
                Name = I.user.Name,
                Surname = I.user.Surname,
                Picture = I.user.Picture,
                Email = I.user.Email,
                UserName = I.user.UserName
                //artık sql sorgumuzun bittiğini bildirmek icin tolist ile sorguyu kapattık.
            });
            //yukarıdaki sorgular sayesinde artık result'ın içerisinde admin olmayan kullanıcılar var.

            toplamSayfa =(int)Math.Ceiling((double)result.Count()/3);

            if (!string.IsNullOrWhiteSpace(aranacakKelime))
            {
                //name'i aranacak kelimeyi içeriyorsa veya soyadı aranacak kelimeyi içeriyorsa bu verileri getir ve resultı buna eşitle.
                result = result.Where(I => I.Name.ToLower().Contains(aranacakKelime.ToLower()) ||
                I.Surname.ToLower().Contains(aranacakKelime.ToLower()));
                //aranacak kelimenin üzerinden sayfa sayısının ayarlanması için sayfa işlemlerini burayada yazdık.       
                toplamSayfa = (int)Math.Ceiling((double)result.Count() / 3);
            }
            //skip metodu geçmemizi saglar.aktif sayfa neyse ondan 1 eksilterek geç.Bir sayfada 3 kullanıcı göstermek istediğimiz için 3 ile carptık.
           //aktif sayfamız 1 oldugu zaman 0 tanesini geçerek 3 tanesini alıcak
            result =result.Skip((aktifSayfa - 1) * 3).Take(3);

            return result.ToList();
        }
    }
}
