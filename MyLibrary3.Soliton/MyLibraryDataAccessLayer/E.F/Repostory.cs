using MyLibrary.Common;
using MyLibrary.Entities;
using MyLibraryDataAccessLayer;
using MyLibrary.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace  MyLibraryDataAccessLayer.E.F
{
   public class Repostory<T> : IDataAccess<T> where T : class
    {
        private DataBaseContext db;
        private DbSet<T> _objectSet;
        public Repostory()
        {
            db=repostoryBase.CreateContext();
            _objectSet = db.Set<T>();
        }
        public List<T> List()
        {
            return db.Set<T>().ToList();
        }
        public List<T> List(Expression<Func<T,bool>> where)
        {
            return _objectSet.Where(where).ToList();
        }
        public int Insert (T obj)
        {
            db.Set<T>().Add(obj);
            if (obj is MyEntitesBase)
            {
                MyEntitesBase o = obj as MyEntitesBase;
                DateTime now = DateTime.Now;
                o.CreatedOn = now;
                o.ModifieOn = now;
                o.ModifieOnUserName = App1.Common.GetCurrentUsername();

            }
            return Save();
        }

        public IQueryable<T> ListQueryable()
        {
            return _objectSet.AsQueryable<T>();
        }

        public int Update(T obj)
        {
            if (obj is MyEntitesBase)
            {
                MyEntitesBase o = obj as MyEntitesBase;
                 o.ModifieOn = DateTime.Now;
                o.ModifieOnUserName = App1.Common.GetCurrentUsername();
            }
            return Save();
        }
        public int Delete(T obj)
        {
        _objectSet.Remove(obj);
            return Save();
        }
        public  int Save()
        {
            return db.SaveChanges();
        }
        public T Find(Expression<Func<T, bool>> where)
        {
            return _objectSet.FirstOrDefault(where);
        }
    }
}
