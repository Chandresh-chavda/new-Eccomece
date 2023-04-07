using Books.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.DataAccess.Repository.IRepository
{
    public interface ICompanyRepository: Irepository<Company>
    {
        void Update(Company obj);
      
    }
}
