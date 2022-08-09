using System.Text.RegularExpressions;
using static Calculator.Item;

namespace Calculator
{
    public static class Calculator
    {
        private readonly static List<Precedence> precedences;

        static Calculator()
        {
            precedences = new List<Precedence> {
                new Precedence(1, ItemType.Multiply, (left, right) => left * right),
                new Precedence(1, ItemType.Divide, (left, right) => left / right),
                new Precedence(2, ItemType.Sum, (left, right) => left + right),
                new Precedence(2, ItemType.Minus, (left, right) => left - right)
            };
        }

        public static double Calculate(string sum)
        {
            var items = ParseStringToItems(sum);
            var number = (double)ProcessBracketAndCalculate(items);

            return number;
        }

        private static IList<Item> ParseStringToItems(string expression)
        {
            var regex = new Regex(@"([0-9]+[\.0-9]*)|(\+)|(-)|(\*)|(\/)|(\()|(\))");
            var matches = regex.Matches(expression);
            var items = new List<Item>();

            foreach (Match match in matches)
            {
                var groups = match.Groups;
                if (match.Success)
                {
                    if (groups[1].Success) items.Add(new Item(Item.ItemType.Number, decimal.Parse(groups[1].Value)));
                    else if (groups[2].Success) items.Add(new Item(Item.ItemType.Sum));
                    else if (groups[3].Success) items.Add(new Item(Item.ItemType.Minus));
                    else if (groups[4].Success) items.Add(new Item(Item.ItemType.Multiply));
                    else if (groups[5].Success) items.Add(new Item(Item.ItemType.Divide));
                    else if (groups[6].Success) items.Add(new Item(Item.ItemType.OpenBracket));
                    else if (groups[7].Success) items.Add(new Item(Item.ItemType.CloseBracket));
                }
            }

            return items;
        }

        private static decimal ProcessBracketAndCalculate(IList<Item> items)
        {
            var itemStack = new Stack<List<Item>>();
            var currentItems = new List<Item>();

            for (int index = 0; index < items.Count; index++)
            {
                var item = items[index];

                switch (item.Type)
                {
                    case ItemType.OpenBracket:
                        itemStack.Push(currentItems);
                        currentItems = new List<Item>();
                        break;
                    case ItemType.CloseBracket:
                        var numberItem = CalculateWithPrecedence(currentItems);
                        if (itemStack.Count > 0)
                        {
                            currentItems = itemStack.Pop();
                            currentItems.Add(numberItem);
                        }
                        else
                        {
                            currentItems = new List<Item> { numberItem };
                        }
                        break;
                    default:
                        currentItems.Add(item);
                        break;
                }
            }

            // Finishing calculation
            if (itemStack.Count == 0)
            {
                if (currentItems.Count > 1)
                {
                    return CalculateWithPrecedence(currentItems).Number;
                }
                else if (currentItems.Count == 1)
                {
                    return currentItems[0].Number;
                }
            }
            throw new Exception("Unknown exception caught! Fail to finish calculate!");
        }

        private static Item CalculateWithPrecedence(IList<Item> items)
        {
            var newItems = CalculateSelectedPrecedenceOperators(items, precedences.Where(x => x.Rank == 1));
            newItems = CalculateSelectedPrecedenceOperators(newItems, precedences.Where(x => x.Rank == 2));

            if (newItems.Count == 1)
            {
                return newItems[0];
            }
            else
            {
                throw new Exception($"Calculation fail to complete! Still haing {string.Join(", ", newItems)}.");
            }
        }

        private static IList<Item> CalculateSelectedPrecedenceOperators(IList<Item> items, IEnumerable<Precedence> precedenceOperators)
        {
            var itemStack = new Stack<Item>();
            Precedence latestPrecedence = null;

            foreach (var item in items)
            {
                var precedence = precedenceOperators.FirstOrDefault(x => x.ItemType == item.Type);

                if (precedence != null)
                {
                    latestPrecedence = precedence;
                }
                else if (item.Type == ItemType.Number)
                {
                    if (latestPrecedence == null)
                    {
                        itemStack.Push(item);
                    }
                    else
                    {
                        var left = itemStack.Pop().Number;
                        var right = item.Number;
                        var tempNumber = latestPrecedence.Compute(left, right);
                        latestPrecedence = null;
                        itemStack.Push(new Item(ItemType.Number, tempNumber));
                    }
                }
                else
                {
                    itemStack.Push(item);
                }
            }

            var newItems = itemStack.ToList();
            newItems.Reverse();
            return newItems;
        }
    }
}
