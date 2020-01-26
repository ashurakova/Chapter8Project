namespace BankLibrary
{
    public interface IAccount 
    {
        // Положить деньги на счет
        void Put(decimal sum);
        // Взять со счета
        decimal Withdraw(decimal sum);
    }
}