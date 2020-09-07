﻿using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using ServiceTracker.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ServiceTracker.DAL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly STContext _context;
        public DbSet<TEntity> Entities { get; set; }

        public Repository(STContext context)
        {
            _context = context;
            Entities = context.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            Entities.Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            Entities.AddRange(entities);
        }


        public virtual void Update(TEntity entity)
        {
            Entities.Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            Entities.UpdateRange(entities);
        }



        public virtual void Remove(TEntity entity)
        {
            Entities.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            Entities.RemoveRange(entities);
        }


        public virtual int Count()
        {
            return Entities.Count();
        }


        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.Where(predicate);
        }



        public virtual TEntity GetSingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.SingleOrDefault(predicate);
        }

        public virtual TEntity Get(int id)
        {
            return Entities.Find(id);
        }

        
        public virtual IEnumerable<TEntity> GetAll()
        {
            return Entities;
        }

    }

}
