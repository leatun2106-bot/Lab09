using BankAccountApp;
var acc = new BankAccount("Trung Pham", 1000m);
acc.Debit(200m);
acc.Credit(50m);
acc.Withdraw(100m);
Console.WriteLine($"{acc.CustomerName}: {acc.Balance}");
