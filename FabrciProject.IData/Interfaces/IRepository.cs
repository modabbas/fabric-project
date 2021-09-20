using System;
using System.Collections.Generic;
using System.Text;

namespace FabrciProject.IData.Interfaces
{
   public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> Get(string query = null);
        bool Set(TEntity entity);
        bool Remove(int id);
    }
}
