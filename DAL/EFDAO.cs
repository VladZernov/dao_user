using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
 
namespace DAOUserProject.DAL
{
    public class EFDAO<TEntity> : IDAO<TEntity> where TEntity : class
    {
        private readonly EFContext context;
        private DbSet<TEntity> entities;
 
        public EFDAO(EFContext context)
        {
            this.context = context;
            entities = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return entities.AsEnumerable();
        }
 
        public TEntity Get(int? id)
        {
            if (id == null) {
                return null;
            }

            return entities.Find(id);
        }

        public void Create(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }
 
        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.Update(entity);
            context.SaveChanges();
        }
 
        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}