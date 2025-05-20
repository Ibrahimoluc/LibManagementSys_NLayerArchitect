using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IYazarService
    {
        List<Yazar> GetAllYazarBl();

        void AddYazarBl(Yazar y);

        Yazar GetYazarByIdBl(int id);

        Yazar GetYazarBl(Yazar y);

        void DeleteYazarBl(Yazar y);

        void UpdateYazarBl(Yazar y);

        string GetYazarState(Yazar y);

    }
}
