using System.Text.RegularExpressions;

namespace Calculator
{
    public class ExpressionDecoder
    {
        public IList<Item> Decode(string expression)
        {
            var regex = new Regex(@"([0-9]+[\.0-9]*)|(\+)|(-)|(\*)|(\/)|(\()|(\))");
            var matches = regex.Matches(expression);
            var items = new List<Item>();

            foreach (Match match in matches)
            {
                var groups = match.Groups;
                if (match.Success)
                {
                    if (groups[1].Success) items.Add(new Item { Type = Item.ItemType.Number, Number = decimal.Parse(groups[1].Value) });
                    else if (groups[2].Success) items.Add(new Item { Type = Item.ItemType.Sum });
                    else if (groups[3].Success) items.Add(new Item { Type = Item.ItemType.Minus });
                    else if (groups[4].Success) items.Add(new Item { Type = Item.ItemType.Multiply });
                    else if (groups[5].Success) items.Add(new Item { Type = Item.ItemType.Divide });
                    else if (groups[6].Success) items.Add(new Item { Type = Item.ItemType.OpenBracket });
                    else if (groups[7].Success) items.Add(new Item { Type = Item.ItemType.CloseBracket });
                }
            }

            return items;
        }
    }
}
