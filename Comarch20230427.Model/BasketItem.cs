using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comarch20230427.Model
{
    public class BasketItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal NetPrice { get; set; }

        public decimal Tax { get; set; }
        public int Count { get; set; }
    }
}
