/*
Skapa ett program som simulerar ett bankkonto. 
Användaren ska kunna skapa konton, göra insättningar, göra uttag och visa kontoinformation.
Programmet ska hålla reda på saldot för varje konto.
*/




class Program
{

    public static void Main()
    {
        Bank bank = new Bank();
        bool isRunning = true;

       System.Console.WriteLine("*****Välkommen till banken*****");
       
       while(isRunning)
       {
            System.Console.WriteLine("---------------------------");
            System.Console.WriteLine("Gör ett av följande val:");
            System.Console.WriteLine("1. Skapa nytt konto ");
            System.Console.WriteLine("2. Visa saldo ");
            System.Console.WriteLine("3. Sätt in pengar ");
            System.Console.WriteLine("4. Ta ut pengar ");
            System.Console.WriteLine("5. Avsluta konto ");
            System.Console.WriteLine("6. Stäng programmet");
            System.Console.WriteLine("---------------------------");
            
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int choice))
            {
                System.Console.WriteLine("Felaktig inmatning. Ange ett giltigt nummer");
                continue;
            }

            if (choice < 1 || choice > 6)
            {
                System.Console.WriteLine("Ogiltigt val. Välje ett nummer mellan 1 och ");
                continue;
            }

            switch(choice)
            {

                case 1:
                {
                    bank.CreateAccount(); 
                    break;
                }
                case 2:
                {
                    bank.ShowBalance();
                    break;
                }
                case 3:
                {
                    bank.DepositMoney();
                    break;
                }
                case 4:
                {
                    bank.WithDrawMoney();
                    break;
                }
                case 5:
                {
                    System.Console.WriteLine("Avsluta konto");
                    break;
                }
                case 6:
                {               
                    isRunning = false;
                    System.Console.WriteLine("Välkommen åter!");
                    break;

                }
            }
        }
    }
}


public class Bankkonto //En klass som innehåller all information som behövs för att kunna skapa och admin ett konto
{
    public string Name {get; set;}
    public int Balance {get; set;}
    public int AccountNr {get; set;}

    public  Bankkonto(string name, int balance, int accountNr) //En konstruktor som möjliggör instansiering av objekten(????)
    {
        this.Name = name;
        this.Balance = balance;
        this.AccountNr = accountNr;
    }   
}

public class Bank //En klass som innehåller en lista för bankkonton samt metoder för skapa konto/insättning/uttag 
{

    List<Bankkonto> bankkonton = new List<Bankkonto>();
    private Bankkonto konto;
    private Bankkonto myAccount;

    public void CreateAccount() //Metod för att skapa ett nytt konto
    {      
        
        System.Console.Write("Skriv in ditt namn (för- och efternamn): ");
        string name = Console.ReadLine();
        System.Console.Write("Skriv in det saldo som du vill sätta in på ditt nya konto: ");
        string input = Console.ReadLine();
        if (!int.TryParse(input, out int deposit))
        {
            System.Console.WriteLine(" Felaktig inmatning. Du behöver ange beloppet med siffror.");
            return;
        }

        Random rnd = new Random(); 
        int accountNr = rnd.Next(10000, 99999); //slumpar fram ett femsiffrigt kontonummer

        Bankkonto newAccount = new Bankkonto(name, deposit, accountNr);

        bankkonton.Add(newAccount);

        System.Console.WriteLine($"Ditt nya bankkonto har skapats med kontonr: {accountNr}. Ditt saldo är: {deposit}");    

    }

    public void EnterAccountNumber()
    {
        System.Console.Write("Ange ditt kontonummer: ");
        string input = Console.ReadLine();
        if(!int.TryParse(input, out int myAccount))
        {
            System.Console.WriteLine("Felaktig inmatning. Du kan endast ange kontonummer. ");
            konto = null;
            return;
        }
        
        konto = bankkonton.FirstOrDefault(b => b.AccountNr == myAccount);
        

        if (konto == null)
        {
            System.Console.WriteLine("Inget konto med angivet nummer hittat. ");
            return;
        }
    }

    public void DepositMoney()
    {   
        EnterAccountNumber();    
        
        System.Console.Write("Ange summa som du vill sätta in på kontot: ");
        string depositInput = Console.ReadLine();
        if(!int.TryParse(depositInput, out int depositAmount) || depositAmount <= 0)
        {
            System.Console.WriteLine("Felaktig inmatning. Du måste ange ett giltigt belopp");
            return;
        }

        konto.Balance += depositAmount;
        System.Console.WriteLine("----------------------------------------------------------------------------------------------");
        System.Console.WriteLine($"Du har satt in {depositAmount} kronor på konto {myAccount}. Ditt nya saldo är {konto.Balance}");
        System.Console.WriteLine("----------------------------------------------------------------------------------------------");        
    }

    public void WithDrawMoney()
    {
        EnterAccountNumber();     

        System.Console.Write("Ange summan som du vill ta ut: ");
        string withDrawInput = Console.ReadLine();
        if(!int.TryParse(withDrawInput, out int withDrawAmount) || withDrawAmount <= 0)
        {
            System.Console.WriteLine("Felaktig inmatning. Ange ett giltigt belopp.");
            System.Console.WriteLine();
            return;            
        }

        konto.Balance -=  withDrawAmount;
        System.Console.WriteLine("---------------------------------------------------------------------------------------------------");
        System.Console.WriteLine($"Du har tagit ut {withDrawAmount} kronor från konto {myAccount}. Det nya saldot är {konto.Balance} ");
         System.Console.WriteLine("---------------------------------------------------------------------------------------------------");

    }

    public void ShowBalance()
    {
        EnterAccountNumber();

        if (konto == null)
        {
            System.Console.WriteLine("Du måste först ange ett kontonummer");
            return;
        }

        System.Console.WriteLine($"Ditt saldo för konto {konto.AccountNr} är {konto.Balance}");
    }

    

        
    
}

