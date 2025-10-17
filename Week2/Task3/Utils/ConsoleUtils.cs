namespace Task3.Utils;

public static class ConsoleUtils
{
    public static string GetRequiredString(string prompt, int maxLength)
    {
        string? input;
        while (true)
        {
            Console.Write(prompt);
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input) && input.Length <= maxLength)
            {
                return input;
            }
            Console.WriteLine($"Input cannot be empty and must be less than {maxLength} characters.");
        }
    }
    
    public static string? GetOptionalString(string prompt, int maxLength)
    {
        string? input;
        while (true)
        {
            Console.Write(prompt);
            input = Console.ReadLine();
            if (input == null || input.Length <= maxLength)
            {
                return string.IsNullOrWhiteSpace(input) ? null : input;
            }
            Console.WriteLine($"Input must be less than {maxLength} characters.");
        }
    }
    
    public static int GetPositiveInt(string prompt)
    {
        int number;
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out number) && number > 0)
            {
                return number;
            }
            Console.WriteLine("Invalid number format. Please, enter a positive number.");
        }
    }
    
    public static bool GetYesNoChoice(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? choice = Console.ReadLine()?.ToLower();

            if (choice == "y") return true;
            if (choice == "n") return false;
            
            Console.WriteLine("Error: enter 'y' or 'n'.");
        }
    }
}