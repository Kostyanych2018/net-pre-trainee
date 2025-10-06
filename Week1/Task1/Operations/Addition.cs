namespace Task1.Operations;

public class Addition: IOperation
{
    public double Execute(double number1, double number2)
    {
        return number1 + number2;
    }
}