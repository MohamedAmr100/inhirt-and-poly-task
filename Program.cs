namespace inhirtandpolytask
{
	public class Account
	{
		public string Name { get; set; }
		public double Balance { get; set; }

		public Account(string Name = "Unnamed Account", double Balance = 0.0)
		{
			this.Name = Name;
			this.Balance = Balance;
		}

		public virtual bool Deposit(double amount)
		{
			if (amount > 0)
			{
				Balance += amount;
				return true;
			}
			return false;
		}

		public virtual bool Withdraw(double amount)
		{
			if (Balance - amount >= 0)
			{
				Balance -= amount;
				return true;
			}
			return false;
		}
		public override string ToString()
		{
			return $"{Name}-balance :{Balance:C}";
		}
		public static Account operator +(Account lhs,Account rhs)
		{
			Account account = new Account(lhs.Name + " " + rhs.Name, lhs.Balance + rhs.Balance);
			return account;
		}
	}
	public static class AccountUtil
	{
		// Utility helper functions for Account class

		public static void Display(List<Account> accounts)
		{
			Console.WriteLine("\n=== Accounts ==========================================");
			foreach (var acc in accounts)
			{
				Console.WriteLine(acc);
			}
		}

		public static void Deposit(List<Account> accounts, double amount)
		{
			Console.WriteLine("\n=== Depositing to Accounts =================================");
			foreach (var acc in accounts)
			{
				if (acc.Deposit(amount))
					Console.WriteLine($"Deposited {amount} to {acc}");
				else
					Console.WriteLine($"Failed Deposit of {amount} to {acc}");
			}
		}

		public static void Withdraw(List<Account> accounts, double amount)
		{
			Console.WriteLine("\n=== Withdrawing from Accounts ==============================");
			foreach (var acc in accounts)
			{
				if (acc.Withdraw(amount))
					Console.WriteLine($"Withdrew {amount} from {acc}");
				else
					Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
			}
		}
	}
	public class SavingsAccount : Account
	{
		public SavingsAccount(string name= "Unnamed Account", double balance=0.0, double rate=0.0) : base(name, balance)
		{
			this.rate = rate;
		}
		public override bool Deposit(double amount)
		{
			return base.Deposit(amount + rate);
		}

		public override bool Withdraw(double amount)
		{
			return base.Withdraw(amount + rate);
		}
        public override string ToString()
        {
            return $"{base.ToString()},Rate: {rate}";
        }

        public double rate { get; set; }
	}
	public class CheckingAccount:Account
	{
        public CheckingAccount(string name= "Unnamed Account", double balance=0.0,double fee =1.5):base(name,balance)
        {
            this.fee = fee;
        }
        public override string ToString()
        {
           return $"{base.ToString()},fee:{fee}";
        }

        public override bool Withdraw(double amount)
        {
            if (Balance - (amount+fee) >= 0)
            {
                Balance -= amount+fee;
                return true;
            }
            return false;
        }

        public double fee { get; set; }
    }
    public class TrustAccount : SavingsAccount
    {
        int withdrawcount;
        const int maxwithdraw = 3;
        DateTime accountcreated;
        
        public TrustAccount(string name= "Unnamed Account", double balance=0.0, double rate=0.0) : base(name, balance, rate)
        {
            withdrawcount = 0;
            accountcreated = DateTime.Now;
        }
        public override bool Deposit(double amount)
        {
            if (amount > 5000)
            {
                Balance += amount + 50;
                return true;
            }
          else  if (amount > 0)
            {
                Balance += amount;
                return true;
            }
            return false;
        }

        public override bool Withdraw(double amount)
        {
            if (amount > Balance * 0.2)
            {
                Console.WriteLine("the amount above limit of 20%");
                return false;
            }
            if (withdrawcount == maxwithdraw)
            {
                Console.WriteLine($"you consume your 3 times this year!!{DateTime.Now}come after one year from the previous date");
                return false;
            }
            withdrawcount++;
            Balance -= amount;
            return true;
            if (DateTime.Now <= DateTime.Now.AddYears(1))
            {
                withdrawcount = 0;
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            // Accounts
            var accounts = new List<Account>();
            accounts.Add(new Account());
            accounts.Add(new Account("Larry"));
            accounts.Add(new Account("Moe", 2000));
            accounts.Add(new Account("Curly", 5000));

            AccountUtil.Display(accounts);
            AccountUtil.Deposit(accounts, 1000);
            AccountUtil.Withdraw(accounts, 2000);

            // Savings
            var savAccounts = new List<Account>();
            savAccounts.Add(new SavingsAccount());
            savAccounts.Add(new SavingsAccount("Superman"));
            savAccounts.Add(new SavingsAccount("Batman", 2000));
            savAccounts.Add(new SavingsAccount("Wonderwoman", 5000, 5.0));

            AccountUtil.Display(savAccounts);
            AccountUtil.Deposit(savAccounts, 1000);
            AccountUtil.Withdraw(savAccounts, 2000);

            // Checking
            var checAccounts = new List<Account>();
            checAccounts.Add(new CheckingAccount());
            checAccounts.Add(new CheckingAccount("Larry2"));
            checAccounts.Add(new CheckingAccount("Moe2", 2000));
            checAccounts.Add(new CheckingAccount("Curly2", 5000));

            AccountUtil.Display(checAccounts);
            AccountUtil.Deposit(checAccounts, 1000);
            AccountUtil.Withdraw(checAccounts, 2000);
            AccountUtil.Withdraw(checAccounts, 2000);

            // Trust
            var trustAccounts = new List<Account>();
            trustAccounts.Add(new TrustAccount());
            trustAccounts.Add(new TrustAccount("Superman2"));
            trustAccounts.Add(new TrustAccount("Batman2", 2000));
            trustAccounts.Add(new TrustAccount("Wonderwoman2", 5000, 5.0));

            AccountUtil.Display(trustAccounts);
            AccountUtil.Deposit(trustAccounts, 1000);
            AccountUtil.Deposit(trustAccounts, 6000);
            AccountUtil.Withdraw(trustAccounts, 2000);
            AccountUtil.Withdraw(trustAccounts, 3000);
            AccountUtil.Withdraw(trustAccounts, 500);

            Console.WriteLine();
        }
    }
}
