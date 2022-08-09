
namespace Calculator
{
    public class Item
    {
        public enum ItemType { NULL, Number, Sum, Minus, Multiply, Divide, OpenBracket, CloseBracket }

        public ItemType Type { get; set; }
        public decimal Number { get; set; }

        public Item(ItemType type, decimal number)
        {
            Type = type;
            Number = number;
        }

        public Item(ItemType type)
        {
            Type = type;
        }

        public override string ToString()
        {
            if (Type == ItemType.Number) return Number.ToString();
            else return Type.ToString();
        }
    }
}
