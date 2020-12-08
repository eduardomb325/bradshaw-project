using BradshawProject.Objects;

namespace BradshawProject.Services.Interface
{
    public interface IAccountService
    {
        void RegisterDataToAccount(Account account);

        Account GetAccountData();
    }
}
