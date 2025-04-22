using Core.Interfaces;
using Utils.Attributes;

namespace Core.Models;

public record BankTransaction : IDbEntity
{
    [DbColumn("bin")]
    public decimal Bin { get; private set; }
    
    [DbColumn("account")]
    public decimal Account { get; private set; }
    
    [DbColumn("amount")]
    public float Amount { get; private set; }
    
    [DbColumn("time")]
    public DateTime Time { get; private set; }
    
    [DbPrimaryKey]
    [DbColumn("ride")]
    public Guid Ride { get; private set; }

    public BankTransaction(decimal bin, decimal account, float amount, DateTime time, Guid ride)
    {
        Bin = bin;
        Account = account;
        Amount = amount;
        Time = time;
        Ride = ride;
    }

    public string AsSqlRow() => $"{Bin}, {Account}, {Amount}, '{Time}', '{Ride}'";
    
    public static readonly string[] Columns = ["bin", "account", "amount", "time", "ride"];
}