namespace AbstractFactoryDesignPattern.Products
{
    public class WindowsCheckbox : ICheckbox
    {
        public void Render() => Console.WriteLine("Rendered Windows Checbox");
    }
}