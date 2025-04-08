namespace DbCourseWork.Models;

public record BankTransaction
{
    public decimal Bin { get; private set; }
    public decimal Account { get; private set; }
    public float Amount { get; private set; }
    public DateTime Time { get; private set; }
    public Guid Ride { get; private set; }

    public BankTransaction(decimal bin, decimal account, float amount, DateTime time, Guid ride)
    {
        Bin = bin;
        Account = account;
        Amount = amount;
        Time = time;
        Ride = ride;
    }
}