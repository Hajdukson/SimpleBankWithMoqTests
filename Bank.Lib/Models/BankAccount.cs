namespace Bank.Lib
{
    public class BankAccount
    {
        public int Id { get; set; }
        public User User { get; set; }
        public decimal CurrentBalance { get; set; }
        public decimal CurrentOverdraft { get; set; }
        public decimal OverdraftLimit { get; set; } = 0;

        public virtual void Debit(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else if (amount <= CurrentBalance)
            {
                CurrentBalance -= amount;
            }
            else
            {
                decimal overdraft = (CurrentBalance - amount) * -1;
                if (overdraft > OverdraftLimit)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                {
                    CurrentOverdraft = overdraft;
                }
            }
        }
        public virtual void Credit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            CurrentBalance += amount;
        }
    }
}