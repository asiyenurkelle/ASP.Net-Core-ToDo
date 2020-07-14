using System;
using System.Collections.Generic;
using System.Text;
using YSKProje.ToDo.DataAccess.Interfaces;
using YSKProje.ToDo.Entities;

namespace YSKProje.ToDo.DataAccess.Concrete.EntitiyFrameworkCore.Repositories
{
    public class EfAciliyetRepository : EfGenericRepository<Aciliyet>, IAciliyetDal
    {
    }
}
