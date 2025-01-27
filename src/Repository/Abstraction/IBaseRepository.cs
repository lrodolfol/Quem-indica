namespace API.Repository.Abstraction;

public interface IBaseRepository
{
    public Task OpenConnectionIfClose();
    public Task CloseConnectionIfOpen();
}
