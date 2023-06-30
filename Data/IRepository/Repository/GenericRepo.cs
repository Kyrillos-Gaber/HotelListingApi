using Microsoft.EntityFrameworkCore;
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

        public async Task<IList<T>> GetAll(Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<string>? includes = null)
        {
            IQueryable<T> query = dbSet;

            if (expression != null)
                query = query.Where(expression);

            if (includes != null)
                foreach (string includeProperty in includes)
                    query = query.Include(includeProperty);

            if (orderBy != null)
                query = orderBy(query);

            List<T>? item = await query.AsNoTracking().ToListAsync();
            return item!;
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
