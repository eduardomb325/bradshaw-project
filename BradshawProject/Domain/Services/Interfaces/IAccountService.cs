using BradshawProject.Objects;

namespace BradshawProject.Services.Interface
{
    public interface IAccountService
    {
        Account RegisterDataToAccount(Account account);

        Account GetAccountData();
    }
}
