using System.Security.Principal;
using System.Xml.Linq;

namespace TaskFour
{
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
            AccountUtil.Withdraw(trustAccounts, 200);
            AccountUtil.Withdraw(trustAccounts, 100);
            AccountUtil.Withdraw(trustAccounts, 100);
            AccountUtil.Withdraw(trustAccounts, 100);
            AccountUtil.Withdraw(trustAccounts, 100);


            Console.WriteLine();
        }
    }

    public class Account
    {
        public string Name { get; set; }
        public double Balance { get; set; }

        public Account(string Name = "UnNamed Account", double Balance = 0.0)
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
            return $"Account Name : {Name}";
        }

        public static Account operator+(Account lhs , Account rhs)
        {
            return new Account(" " , lhs.Balance + rhs.Balance);
        } 
        public static Account operator-(Account lhs , Account rhs)
        {
            return new Account("", lhs.Balance - rhs.Balance);
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
                    Console.WriteLine($"Deposited {amount} to {acc} and balance become {acc.Balance:C}");
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
                    Console.WriteLine($"Withdrew {amount} from {acc} and balance become {acc.Balance:C}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
            }
        }
    }

    public class SavingsAccount: Account
    {
        public double InterestRata { get; set; }

        public SavingsAccount(string Name = "Unnamed Account", double Balance = 0.0, double InterRata = 0.0):base(Name,Balance)
        {
            InterestRata = InterRata;
        }
        public override bool Deposit(double amount)
        {
            return base.Deposit(amount);
        }
        public override bool Withdraw(double amount)
        {
            return base.Withdraw(amount);
        }
        public override string ToString()
        {
            return $"Savings Account Name : {Name}";
        }

    }
    
    public class CheckingAccount:Account
    {
        public CheckingAccount(string name="unnamed account", double balance = 0.0) : base(name, balance)
        {
            //Name = name;
            //Balance = balance;
        }

        //public new string Name { get; set; }
        //public new double Balance { get; set; }

        public const double Fee = 1.50;

        public override bool Deposit(double amount)
        {
            return base.Deposit(amount);
        }
        public override bool Withdraw(double amount)
        {
            return base.Withdraw(amount+Fee);
        }
        public override string ToString()
        {
            return $"Checking Account Name : {Name}";
        }

    }

    public class TrustAccount:SavingsAccount
    {
        public TrustAccount(string Name = "Unnamed Account", double Balance = 0.0, double InterRata = 0.0) : base(Name, Balance , InterRata)
        {
         
        }

        //int currentYear = DateTime.Now.Year;
        int flag = 1;
        public override bool Withdraw(double amount)
        {
            if (amount <= Balance * 0.2)
            {  
                if (DateTime.Now.Year == 2024 && flag <= 3)
                {
                    flag++;
                    return base.Withdraw(amount); 
                }
                else
                    Console.WriteLine($"Three transactions have been made during this year {DateTime.Now.Year} Wait for the new year.");
            }
            return base.Withdraw(0);
        }
        public const double Bouns = 50.0;
        public override bool Deposit(double amount)
        {
            if (amount >= 5000)
            {
                return base.Deposit(amount + Bouns);
            }
            return base.Deposit(amount);
        }

        public override string ToString()
        {
            return $"Trust Account Name : {Name}";
        }
    }

}
