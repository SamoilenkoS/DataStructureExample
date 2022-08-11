using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDemo
{
    public class GoodSummary
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}\t{nameof(Title)}: {Title}\t{nameof(Count)}: {Count}";
        }
    }
}
