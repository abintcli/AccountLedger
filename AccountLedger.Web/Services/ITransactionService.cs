using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountLedger.Models;

namespace AccountLedger.Web.Services
{
    public interface ITransactionService
    {
        public Task<List<AccountTransaction>> GetAll(int accountId);
        public Task<List<AccountTransaction>> GetAllFilter(int accountId, TransactionType type);
        public Task<AccountTransaction?> Get(int id);
        public Task<AccountTransaction> Add(AccountTransaction transaction);
        public Task<bool> Delete(int id);
        public Task<bool> Update(AccountTransaction transaction, int id);

    }
}