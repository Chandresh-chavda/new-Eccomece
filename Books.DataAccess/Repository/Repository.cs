﻿using books.DataAccess;
using Books.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Books.DataAccess.Repository
{
    public class Repository<T> : Irepository<T> where T : class
    
    {
            private readonly ApplicationDbContext _db;
            internal DbSet<T> dbset;
            public Repository(ApplicationDbContext db)
            {
                _db = db;
                this.dbset = _db.Set<T>();

            }
            public void Add(T entity)
            {
                dbset.Add(entity);
            
            }
            // include properties category , covertype
            public IEnumerable<T> GetAll(string? includeProperties = null)
            {
                IQueryable<T> query = dbset;
                if (includeProperties != null)
                {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
                }
                return query.ToList();
            }

            public T GetFirstOrDefault(Expression<Func<T, bool>> Filter, string? includeProperties = null)
            {
                IQueryable<T> query = dbset;
                query= query.Where(Filter);
                if (includeProperties != null)
                {
                    foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp);
                    }
                }
                return query.FirstOrDefault();
            }

            public void Remove(T entity)
            {
                dbset.Remove(entity);
            }

            public void RemoveRange(IEnumerable<T> entity)
            {
                dbset.RemoveRange(entity);
            }
    }
}
