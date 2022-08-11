using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDemo
{
    public class GoodOnWarehouse
    {
        [ForeignKey("Good")]
        public Guid GoodId { get; set; }
        [ForeignKey("Warehouse")]
        public Guid WarehouseId { get; set; }
        public int Count { get; set; }

        public Good Good { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}
