using AccountLedger.Web.Models;

namespace AccountLedger.Services
{
    public interface IAccountService
    {
        public Task<List<AccountModel>> GetAll();
        public Task<AccountModel?> Get(int id);
        public Task<AccountModel> Add(AccountModel account);
        public Task<bool> Delete(int id);
        public Task<bool> Update(AccountModel account, int id);
    }
}
