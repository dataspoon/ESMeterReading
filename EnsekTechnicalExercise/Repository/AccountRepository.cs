using EnsekTechnicalExercise.Api.Data;

namespace EnsekTechnicalExercise.Api.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MeterContext _context; 
        public AccountRepository(MeterContext meterContext)
        {
            _context = meterContext;
        }

        public bool IsValidAccount(int accountId)
        {
            return _context.Accounts.Any(a => a.AccountId == accountId);
        }
    }
}
