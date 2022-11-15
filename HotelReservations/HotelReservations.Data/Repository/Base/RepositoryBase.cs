using HotelReservations.Data.Context;
using HotelReservations.Data.Repository.Base.Interfaces;
using System.Threading.Tasks;

namespace HotelReservations.Data.Repository.Base
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly DatabaseContext databaseContext;

        public RepositoryBase(DatabaseContext apicoreni_DbContext)
        {
            databaseContext = apicoreni_DbContext;
        }

        public async Task AddAsync(TEntity entity)
        {
            await databaseContext.Set<TEntity>().AddAsync(entity);
            await databaseContext.SaveChangesAsync();

        }
        public async Task RemoveAsync(TEntity entity)
        {
            databaseContext.Set<TEntity>().Remove(entity);
            await databaseContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            databaseContext.Set<TEntity>().Update(entity);
            await databaseContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            databaseContext.Dispose();
        }

    }
}
