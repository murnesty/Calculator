using static Calculator.Calculator;
using static Calculator.Item;

namespace Calculator
{
    public class Precedence
    {
        public int Rank { get; set; }
        public ItemType ItemType { get; set; }
        public Func<decimal, decimal, decimal> Compute { get; set; }

        public Precedence(int rank, ItemType itemType, Func<decimal, decimal, decimal> compute)
        {
            Rank = rank;
            ItemType = itemType;
            Compute = compute;
        }
    }
}
