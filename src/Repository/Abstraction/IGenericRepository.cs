namespace API.Repository.Abstraction;

public interface IGenericRepository
{
    public Task<T> GetAsync<T>(int id);
    public Task PostAsync<T>(T entity);
    public Task PutAsync<T>(int id, T entity);
    public Task DeleteAsync<T>(int id);
}
