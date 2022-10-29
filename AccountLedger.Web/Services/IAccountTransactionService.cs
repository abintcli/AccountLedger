using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountLedger.Models;

namespace AccountLedger.Web.Services
{
    public interface IAccountTransactionService
    {
        public Task<List<AccountTransaction>> GetAll();
        public Task<AccountTransaction?> Get(int id);
        public Task<AccountTransaction> Add(AccountTransaction transaction);
        public void Delete(int id);
        public void Update(AccountTransaction transaction, int id);

    }
}