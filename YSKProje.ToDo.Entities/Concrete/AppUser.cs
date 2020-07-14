using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.Entities.Interfaces;

namespace YSKProje.ToDo.Entities.Concrete
{
    //sistemdeki kullanıcılar için oluşturdugumuz class.Primary kimizi int belirledik.
    public class AppUser:IdentityUser<int>,ITablo
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Picture { get; set; } = "default.png";

        public List <Gorev> Gorevler { get; set; }
    }
}
