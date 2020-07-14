using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.DataAccess.Interfaces
{
    public interface IAppUserDal
    {
        List<AppUser> GetirAdminOlmayanlar();
        //out ile toplam sayfayı dışarıya fırlatılıcak
        List<AppUser> GetirAdminOlmayanlar(out int toplamSayfa,string aranacakKelime, int aktifSayfa = 1);
    }
}
