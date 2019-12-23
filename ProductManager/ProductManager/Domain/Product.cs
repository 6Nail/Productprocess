using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Domain
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
