using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Item
    {
        public enum ItemType { NULL, Number, Sum, Minus, Multiply, Divide, OpenBracket, CloseBracket }

        public ItemType Type { get; set; }
        public decimal Number { get; set; }

        public override string ToString()
        {
            if (Type == ItemType.Number) return Number.ToString();
            else return Type.ToString();
        }
    }
}
