
using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Domain.Events;
using Mc2.CrudTest.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Persistanse.Context
{
    public class Repository : IRepository
    {
        private readonly MyDbContext _dbContext;

        public Repository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> UpdateAsync(Customer customers)
        {
            var old = _dbContext.Customers.Where(w => w.Id == customers.Id).FirstOrDefault();
            var a = _dbContext.Entry(old).CurrentValues;
            a.SetValues(customers);
            ;
            return await SaveChangesAsync();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        
    }
}
