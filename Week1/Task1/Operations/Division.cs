namespace Task1.Operations;

public class Division : IOperation
{
    public double Execute(double number1, double number2)
    {
        if (number2 == 0) {
            throw new DivideByZeroException("Error: Division by zero is not allowed.");
        }
        return number1 / number2;
    }
}