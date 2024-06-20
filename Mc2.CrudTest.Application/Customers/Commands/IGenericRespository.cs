namespace Mc2.CrudTest.Application.Customers.Commands
{
    public interface IGenericRespository<T>
    {
        Task<bool> AddAsync (T entity);
    }
}