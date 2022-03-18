namespace Messenger.Host.Repository
{
    public interface IRepositoryFactory
    {
        public IGenericRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) 
            where TEntity : class;
    }
}
