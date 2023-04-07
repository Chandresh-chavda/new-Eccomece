using Books.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.DataAccess.Repository.IRepository
{
    public interface ICoverTypeRepository: Irepository<CoverType>
    {
        void Update(CoverType obj);
      
    }
}
