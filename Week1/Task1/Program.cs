namespace Task1;

internal class Program
{
    public static void Main(string[] args)
    {
        var calculator = new Calculator();

        while (true) {
            double number1 = GetNumber("Enter the first number: ");
            string operation = GetOperation("Select an operation (+, -, *, /): ", calculator);
            double number2 = GetNumber("Enter the second number: ");

            try {
                double result = calculator.ExecuteOperation(operation, number1, number2);
                Console.WriteLine($"The result is: {result}");
            }
            catch (DivideByZeroException ex) {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("\nPress Enter to perform a new operation, or enter 'q' to quit.");
            if (Console.ReadLine().ToLower() == "q") {
                break;
            }
        }
    }

    private static double GetNumber(string message)
    {
        double number;
        Console.Write(message);
        while (!double.TryParse(Console.ReadLine(), out number)) {
            Console.WriteLine("Error: Enter a valid number.");
            Console.Write(message);
        }

        return number;
    }

    private static string GetOperation(string message, Calculator calculator)
    {
        while (true) {
            Console.Write(message);
            string operation = Console.ReadLine();

            if (calculator.IsOperationValid(operation)) {
                return operation;
            }

            Console.WriteLine($"Error: '{operation}' is not a valid operation. Please try again.");
        }
    }
}