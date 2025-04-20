
using Marketteto.Models;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Drawing.Printing;
using System.Linq.Expressions;

namespace Marketteto.Data.Base
{
    public class EntityBaseRepository<T> : IBaseRepositoryEntity<T> where T : class, IBaseEntity
    {
        private readonly MarkettetoDbContext _context;
        public EntityBaseRepository(MarkettetoDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(T Entity)
        {
            await _context.Set<T>().AddAsync(Entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entityId = await _context.Set<T>().FirstOrDefaultAsync(i => i.Id == id);
            if(entityId != null)
            {
                _context.Set<T>().Remove(entityId);
                await _context.SaveChangesAsync();
            }
        }

            public async Task<List<T>> GetAllAsync()
                 => await _context.Set<T>().ToListAsync();
        //***************************************************
        public async Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] include)
        {
            IQueryable<T> query = _context.Set<T>().AsQueryable();
            query = include.Aggregate(query, (current, include) => current.Include(include));
            return await query.ToListAsync();
        }

        //**********************************************************
        public async Task<T> GetByIdAsync(int id)
        => await _context.Set<T>().FirstOrDefaultAsync(i => i.Id == id);
        public async Task<T> GetByIdAsync(int id,params Expression<Func<T, object>>[] include)
        {
            IQueryable<T> query = _context.Set<T>().AsQueryable();
            query = include.Aggregate(query, (current, include) => current.Include(include));
            return await query.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task UpdateAysnc(T Entity)
        {
            EntityEntry entityEntry = _context.Entry<T>(Entity);
            entityEntry.State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
