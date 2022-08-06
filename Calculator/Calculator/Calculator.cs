using static Calculator.Item;

namespace Calculator
{
    public static class Calculator
    {
        public static double Calculate(string sum)
        {
            var items = new ExpressionDecoder().Decode(sum);
            var index = 0;
            var number = (double)Calculate(items, ref index);

            return number;
        }

        private static decimal Calculate(IList<Item> items, ref int index)
        {
            var left = 0m;
            var right = 0m;
            var @operator = ItemType.NULL;

            for (; index < items.Count; index++)
            {
                var item = items[index];

                switch (item.Type)
                {
                    case ItemType.OpenBracket:
                        index++;
                        if (@operator == ItemType.NULL)
                        {
                            left = Calculate(items, ref index);
                        }
                        else
                        {
                            right = Calculate(items, ref index);
                            left = Evaluate(left, right, @operator);
                            @operator = ItemType.NULL;
                        }
                        break;

                    case ItemType.CloseBracket:
                        return left;

                    case ItemType.Number:
                        if (@operator == ItemType.NULL)
                        {
                            left = item.Number;
                        }
                        else
                        {
                            right = item.Number;
                            left = Evaluate(left, right, @operator);
                            @operator = ItemType.NULL;
                        }
                        break;

                    default:
                        @operator = item.Type;
                        break;
                }
            }

            return left;
        }

        private static decimal Evaluate(decimal left, decimal right, ItemType @operator)
        {
            switch (@operator)
            {
                case ItemType.Sum: return left + right;
                case ItemType.Minus: return left - right;
                case ItemType.Multiply: return left * right;
                case ItemType.Divide: return left / right;
                default: throw new Exception($"Unknown exception captured : Unhandled operator {@operator.ToString()}.");
            }
        }
    }
}
