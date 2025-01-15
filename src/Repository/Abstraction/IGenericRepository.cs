using API.Models.Entities;

namespace API.Repository.Abstraction;

public interface IGenericRepository
{
    public Task<T> GetAsync<T>(int id) where T : Entitie;
    public Task PostAsync<T>(T entity) where T : Entitie;
    public Task PutAsync<T>(int id, T entity) where T : Entitie;
    public Task DeleteAsync<T>(int id) where T : Entitie;
}
