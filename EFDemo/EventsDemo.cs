using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDemo
{
    public class EventsDemo
    {
        public class Printer
        {
            private Inc _blackInc;
            public event Action<string> IncLeak;

            public Printer()
            {
                Console.WriteLine("Initialize printer");
                _blackInc = new Inc();
            }

            public void PrintDocument(Document document)
            {
                if(document.Length > _blackInc.CurrentAmount)
                {
                    IncLeak?.Invoke(nameof(_blackInc));
                    return;
                }

                Console.WriteLine($"\tPrinting document\t\n" + document.Text + document.Title);
                _blackInc.Use(document.Length);
            }

            public void Refill(string color)
            {
                Console.WriteLine($"Trying to refil {color} in printer");
                switch (color)
                {
                    case "_blackInc":
                        _blackInc.Refill();
                        break;
                    default:
                        break;
                }
            }
        }

        public class Inc
        {
            private int _currentAmount;
            public int CurrentAmount { get => _currentAmount; }

            public Inc()
            {
                _currentAmount = 100;
                Console.WriteLine($"Initialize inc. Current amount:{_currentAmount}");
            }

            public void Use(int amount)
            {
                _currentAmount -= amount;
                Console.WriteLine($"{amount} inc used. Current amount:{_currentAmount}");
            }

            internal void Refill()
            {
                _currentAmount = 100;
                Console.WriteLine("Inc refilled");
            }
        }

        public class Document
        {
            public string Title { get; set; }
            public string Text { get; set; }

            public int Length => Title.Length + Text.Length;
        }
    }
}
