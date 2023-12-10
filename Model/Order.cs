using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Order
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }

        public string? Description { get; set; }

        public double? Price { get; set; }
        public string Key { get; set; }
    }
}
