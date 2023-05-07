using System;
using System.Collections.Generic;

// Example from; adapted to C#:
//
// Martin Fowler. Refactoring: Improving the Design of Existing Code.
// 2nd edition. Addison-Wesley Professional. November 2018. 448 pages.
// ISBN: 978-0134757681.

namespace Refactoring
{
    public class Play
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public class Performance
    {
        public string ID { get; set; }
        public int Audience{ get; set; }
    }

    public class Invoice
    {
        public string Customer { get; set; }
        public List<Performance> Performances { get; set; }
    }

    public class Report
    {
        public static string Statement(Dictionary<string, Play> plays, Invoice invoice) {
            var totalAmount = 0;
            var volumeCredits = 0.0;
            var result = $"Statement for {invoice.Customer}\n";

            var culture = new System.Globalization.CultureInfo("da-DK");
            Func<int, string> format = (value) => value.ToString("C2", culture);
            
            foreach (var perf in invoice.Performances) {
                var play = plays[perf.ID];
                var thisAmount = 0;

                switch (play.Type) {
                    case "tragedy":
                    thisAmount = 40000;
                    if (perf.Audience > 30) {
                        thisAmount += 1000 * (perf.Audience - 30);
                    }
                    break;
                    case "comedy":
                    thisAmount = 30000;
                    if (perf.Audience > 20) {
                        thisAmount += 10000 + 500 * (perf.Audience - 20);
                    }
                    thisAmount += 300 * perf.Audience;
                    break;
                    default:
                        throw new Exception($"unknown type: {play.Type}");
                }
                // add volume credits
                volumeCredits += Math.Max(perf.Audience - 30, 0);
                // add extra credit for every ten comedy attendees
                if ("comedy" == play.Type) volumeCredits += Math.Floor(perf.Audience / 5.0);

                // print line for this order
                result += $"  {play.Name}: {format(thisAmount/100)} ({perf.Audience} seats)\n";
                totalAmount += thisAmount;
            }
            result += $"Amount owed is {format(totalAmount/100)}\n";
            result += $"You earned {volumeCredits} credits\n";
            return result;
        }
    }

    class Program
    {
        

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
