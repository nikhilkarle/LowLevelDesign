namespace AbstractFactoryDesignPattern.Products
{
    public class WindowsButton: IButton
    {
        public void Render() => Console.WriteLine("Rendered Windows Button");
    }
}