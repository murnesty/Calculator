namespace Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var number1 = Calculator.Calculate("1 * 1");
            var number2 = Calculator.Calculate("1 + 1");
            var number3 = Calculator.Calculate("1 + 2 + 3");
            var number4 = Calculator.Calculate("6 / 2");
            var number5 = Calculator.Calculate("11 + 23");
            var number6 = Calculator.Calculate("11.1 + 23");
            var number7 = Calculator.Calculate("( 11.5 + 15.4 ) + 10.1");
            var number8 = Calculator.Calculate("23 - ( 29.3 + 12.5 )");
            var number9 = Calculator.Calculate("( 1 / 2 ) - 1 + 1");
            var number10 = Calculator.Calculate("(1+1)*3");
        }
    }
}