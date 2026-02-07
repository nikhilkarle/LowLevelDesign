namespace AbstractFactoryDesignPattern.Products
{
    public class MacCheckbox: ICheckbox
    {
        public void Render() => Console.WriteLine("Rendered Mac Checkbox");
    }
}