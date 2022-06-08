namespace DatabaseLib;

public interface IRepository<T> where T : class
{
    public Task<bool> AddAsync(T model);
    public Task<T?> GetByIdAsync(Guid id);
    public Task<List<T>> GetAllAsync(int skip, int take);
    public Task<bool> UpdateAsync(T model);
    public Task<bool> DeleteByIdAsync(Guid id);
}