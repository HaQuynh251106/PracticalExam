using System;
using System.Collections.Generic;
using System.Globalization;
using BankSystem;
using MusicStore;

// ##### PHẦN 1: BÀI TOÁN HỆ THỐNG NGÂN HÀNG #####
namespace BankSystem
{
    public abstract class Account
    {
        protected decimal balance;
        public Account(decimal initialBalance) { this.balance = initialBalance; }
        public virtual void CheckBalance() { Console.WriteLine($"Your balancer: {this.balance:N0} đ"); }
        public void BankTransfer(decimal amount)
        {
            if (amount <= 0) { Console.WriteLine("Transfer amount must be positive."); return; }
            if (amount > this.balance) { Console.WriteLine("Insufficient funds for this transfer."); }
            else
            {
                this.balance -= amount;
                Console.WriteLine($"Your transferred {amount:N0} đ, Your balancer : {this.balance:N0} đ");
            }
        }
    }
    public class NormalAccount : Account { public NormalAccount(decimal amount) : base(amount) { } }
    public class ExchangeAccount : Account
    {
        public ExchangeAccount(decimal amount, decimal exchangeRate) : base(amount * exchangeRate)
        {
            Console.WriteLine($"Account created with amount ${amount:N0} at exchange rate {exchangeRate:N0} VND/USD.");
        }
    }
}

// ##### PHẦN 2: BÀI TOÁN CỬA HÀNG NHẠC CỤ #####
namespace MusicStore
{
    public abstract class Instrument
    {
        public string Name { get; set; }
        public int YearOfManufacture { get; set; }
        public Instrument(string name, int year) { Name = name; YearOfManufacture = year; }
        public abstract void Play();
        public override string ToString() { return $"Instrument: {Name}, Year: {YearOfManufacture}"; }
    }
    public class Guitar : Instrument
    {
        public int NumberOfStrings { get; set; }
        public Guitar(string name, int year, int strings) : base(name, year) { NumberOfStrings = strings; }
        public override void Play() { Console.WriteLine("Strumming the guitar strings... 🎸"); }
        public override string ToString() { return $"{base.ToString()}, Type: Guitar, Strings: {NumberOfStrings}"; }
    }
    public class Piano : Instrument
    {
        public int NumberOfKeys { get; set; }
        public Piano(string name, int year, int keys) : base(name, year) { NumberOfKeys = keys; }
        public override void Play() { Console.WriteLine("Playing a melody on the piano keys... 🎹"); }
        public override string ToString() { return $"{base.ToString()}, Type: Piano, Keys: {NumberOfKeys}"; }
    }
}

// ##### PHẦN CHÍNH: CHƯƠNG TRÌNH ĐỂ CHẠY 
class Program
{
    static void Main(string[] args)
    {
        // Thiết lập văn hóa để hiển thị số cho đẹp (dấu chấm ngăn cách hàng nghìn)
        CultureInfo ci = new CultureInfo("vi-VN");
        System.Threading.Thread.CurrentThread.CurrentCulture = ci;
        System.Threading.Thread.CurrentThread.CurrentUICulture = ci;

        // --- Chạy ví dụ 1: Hệ thống ngân hàng ---
        Console.WriteLine("===== BÀI TOÁN 1: HỆ THỐNG NGÂN HÀNG =====");
        Console.WriteLine("--- Testing Exchange Account ---");
        Account exchangeAcc = new ExchangeAccount(1000, 25000);
        exchangeAcc.CheckBalance();
        exchangeAcc.BankTransfer(1000000);

        Console.WriteLine("\n--- Testing Normal Account ---");
        Account normalAcc = new NormalAccount(25000000);
        normalAcc.CheckBalance();
        normalAcc.BankTransfer(1000000);

        Console.WriteLine("\n\n============================================\n");

        // --- Chạy ví dụ 2: Cửa hàng nhạc cụ ---
        Console.WriteLine("===== BÀI TOÁN 2: CỬA HÀNG NHẠC CỤ =====");
        List<Instrument> instruments = new List<Instrument>
        {
            new Guitar("Fender Stratocaster", 2021, 6),
            new Piano("Yamaha Grand", 2023, 88),
            new Guitar("Gibson Les Paul", 2019, 6),
            new Piano("Kawai Digital", 2022, 76),
            new Guitar("Ibanez Acoustic", 2020, 5)
        };

        foreach (var instrument in instruments)
        {
            Console.WriteLine("--------------------");
            Console.WriteLine(instrument);
            instrument.Play();
        }
        Console.WriteLine("--------------------");
    }
}