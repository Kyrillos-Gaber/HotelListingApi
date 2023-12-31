﻿using Microsoft.VisualStudio.Services.WebApi;
using System.Linq;
using System.Linq.Expressions;

namespace HotelListingApi.Data.IRepository
{
    public interface IGenericRepo<T> where T : class
    {
        Task<IList<T>> GetAll(
            QueryParams queryParams = null,
            Expression<Func<T, bool>>? expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            List<string>? includes = null
        );

        

        Task<T> Get(Expression<Func<T, bool>> expression, List<string>? includes = null);

        Task Insert(T entity);

        Task InsertRange(IEnumerable<T> entities);

        Task Delete(int id);

        void DeleteRange(IEnumerable<T> entities);

        void Update(T  entity);
        Task<IList<T>> GetAll(Expression<Func<T, bool>>? expression, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy, List<string>? includes, QueryParams? queryParams);
    }
}
