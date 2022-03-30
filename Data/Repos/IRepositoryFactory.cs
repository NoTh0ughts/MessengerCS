namespace Data.Repos
{
    public interface IRepositoryFactory
    {
        public IGenericRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) 
            where TEntity : class;
    }
}
