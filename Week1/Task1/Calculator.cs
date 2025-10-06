using Task1.Operations;

namespace Task1;

public class Calculator
{
    private Dictionary<string, IOperation> _operations;

    public Calculator()
    {
        _operations = new Dictionary<string, IOperation>()
        {
            { "+", new Addition() },
            { "-", new Subtraction() },
            { "*", new Multiplication() },
            { "/", new Division() }
        };
    }

    public double ExecuteOperation(string operation, double number1, double number2)
    {
        return _operations[operation].Execute(number1, number2);
    }

    public bool IsOperationValid(string operation)
    {
        return _operations.ContainsKey(operation);
    }
}