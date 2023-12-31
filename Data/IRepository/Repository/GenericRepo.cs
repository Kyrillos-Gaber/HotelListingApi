﻿using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Services.WebApi;
using System.Linq.Expressions;

namespace HotelListingApi.Data.IRepository.Repository
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly DatabaseContext context;
        private readonly DbSet<T> dbSet;

        public GenericRepo(DatabaseContext context)
        {
            this.context = context;
            dbSet = this.context.Set<T>();
        }

        public async Task Delete(int id)
        {
            T? item = await dbSet.FindAsync(id);
            dbSet.Remove(item!);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression, List<string>? includes = null)
        {
            IQueryable<T> query = dbSet;

            if (includes != null)
                foreach (string includeProperty in includes)
                    query = query.Include(includeProperty);
            
            T? item = await query.AsNoTracking().FirstOrDefaultAsync(expression);
            return item!;
        }

        public async Task<IList<T>> GetAll(QueryParams queryParams, Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<string>? includes = null)
        {
            IQueryable<T> query = dbSet;

            if (expression != null)
                query = query.Where(expression);

            if (includes != null)
                foreach (string includeProperty in includes)
                    query = query.Include(includeProperty);

            if (orderBy != null)
                query = orderBy(query);

            if (queryParams is not null)
                return
                    await query
                    .AsNoTracking()
                    .Skip((queryParams!.PageNumber - 1) * queryParams.PageSize)
                    .Take(queryParams.PageSize).ToListAsync();


            return await query.AsNoTracking().ToListAsync();
        }

        

        public Task<IList<T>> GetAll(Expression<Func<T, bool>>? expression, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy, List<string>? includes, QueryParams? queryParams)
        {
            throw new NotImplementedException();
        }

        public async Task Insert(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task InsertRange(IEnumerable<T> entities)
        {
            await dbSet.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        
    }
}
