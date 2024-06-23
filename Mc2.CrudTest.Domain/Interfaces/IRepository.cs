using Mc2.CrudTest.Domain.Customers;
using System.Linq.Expressions;

namespace Mc2.CrudTest.Domain.Interfaces
{
    public interface IRepository
    {
        public  Task<int> UpdateAsync(Customer customers);
        public Task<int> SaveChangesAsync();

    }
}
