using Application.IRepository;
using Domain.Entities;
using Infrastrucutre_Persistance_Presentation.DBContext;
using Microsoft.EntityFrameworkCore;


namespace Infrastrucutre_Persistance_Presentation.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly NetBankingContext _friendsOnlyContext;
        public GenericRepository(NetBankingContext friendsOnlyContext)
        {
            _friendsOnlyContext = friendsOnlyContext;
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            await _friendsOnlyContext.Set<TEntity>().AddAsync(entity);
            await _friendsOnlyContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task Delete(TEntity entity)
        {
            _friendsOnlyContext.Set<TEntity>().Remove(entity);
            await _friendsOnlyContext.SaveChangesAsync();
        }

        public virtual async Task<List<TEntity>> GetAll(string userId)
        {
            return await _friendsOnlyContext.Set<TEntity>().Where( e => e.UserId == userId).ToListAsync();
        }

        public virtual async Task<TEntity> GetById(int? id)
        {
            return await _friendsOnlyContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual async Task Update(TEntity entity )
        {
            try
            {
                _friendsOnlyContext.Entry(entity).State = EntityState.Modified;
                await _friendsOnlyContext.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException ex)
            {
                
            }
           
        }
    }
}
