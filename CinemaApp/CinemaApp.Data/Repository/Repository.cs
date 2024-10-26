using CinemaApp.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CinemaApp.Data.Repository
{
    public class Repository<TType, TId> : IRepository<TType, TId> where TType : class
    {
        private readonly CinemaDbContext context;
        private readonly DbSet<TType> dbSet;


        public Repository(CinemaDbContext _context)
        {
            this.context = _context;
            this.dbSet = context.Set<TType>();
        }

        public bool Add(TType item)
        {
            if (dbSet.Contains(item))
            {
                return false;
            }

            this.dbSet.Add(item);
            this.context.SaveChanges();

            return true;
        }

        public async Task<bool> AddAsync(TType item)
        {
            if (dbSet.Contains(item))
            {
                return false;
            }

            await this.dbSet.AddAsync(item);
            await this.context.SaveChangesAsync();

            return true;
        }

        public bool Delete(TId id)
        {
            TType entity = GetById(id);
            if (entity != null) 
            {
                this.dbSet.Remove(entity);
                this.context.SaveChanges(); 
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(TId id)
        {
            TType entity = GetById(id);
            if (entity != null)
            {
                this.dbSet.Remove(entity);
                await this.context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public IEnumerable<TType> GetAll()
        {
            return dbSet.ToList();
        }

        public async Task<IEnumerable<TType>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public IEnumerable<TType> GetAllAttached()
        {
            return dbSet.AsQueryable();
        }

        public TType GetById(TId id)
        {
            TType? entity = dbSet
                .Find(id);

            return entity;
        }

        public async Task<TType> GetByIdAsync(TId id)
        {
            TType? entity = await dbSet
                .FindAsync(id);

            return entity;
        }

        public bool Update(TType item)
        {
            try
            {
                this.dbSet.Attach(item);
                this.context.Entry(item).State = EntityState.Modified;
                this.context.SaveChanges();

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(TType item)
        {
            try
            {
                this.dbSet.Attach(item);
                this.context.Entry(item).State = EntityState.Modified;
                await this.context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
