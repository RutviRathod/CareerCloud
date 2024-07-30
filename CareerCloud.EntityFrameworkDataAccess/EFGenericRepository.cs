using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using CareerCloud.DataAccessLayer;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class EFGenericRepository<T> : IDataRepository<T> where T : class
    {
        public void Add(params T[] items)
        {
            using (var _context = new CareerCloudContext())
            { 
                foreach (var item in items)
                {
                    _context.Set<T>().Add(item);
                    _context.SaveChanges();
                }
                
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            using(var _context = new CareerCloudContext()) 
            {
                IQueryable<T> query = _context.Set<T>();
                foreach (var navigationProperty in navigationProperties)
                {
                    query = query.Include(navigationProperty);
                }
                return query.ToList();
            }
        }

        public IList<T> GetList(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            using (var _context = new CareerCloudContext())
            {
                IQueryable<T> query = _context.Set<T>().Where(where);
                foreach (var navigationProperty in navigationProperties)
                {
                    query = query.Include(navigationProperty);
                }
                return query.ToList();
            }
        }

        public T GetSingle(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            using (var _context = new CareerCloudContext())
            {
                IQueryable<T> query = _context.Set<T>().Where(where);
                foreach (var navigationProperty in navigationProperties)
                {
                    query = query.Include(navigationProperty);
                }
                return query.FirstOrDefault();
            }
        }

        public void Remove(params T[] items)
        {
            using (var _context = new CareerCloudContext())
            {
                foreach (var item in items)
                {
                    _context.Set<T>().Remove(item);
                    _context.SaveChanges();
                }

            }
        }

        public void Update(params T[] items)
        {
            using (var _context = new CareerCloudContext())
            {
                foreach (var item in items)
                {
                    _context.Set<T>().Attach(item);
                    _context.Entry(item).State = EntityState.Modified;
                    _context.SaveChanges();
                }

            }
        }
    }
}
