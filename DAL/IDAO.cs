using System;
using System.Collections.Generic;
 
namespace DAOUserProject.DAL
{
    public interface IDAO<TEntity> where TEntity : class
    {
        void Create(TEntity item);
        IEnumerable<TEntity> GetAll();
        TEntity Get(int? id);
        void Delete(TEntity item);
        void Update(TEntity item);
    }
}