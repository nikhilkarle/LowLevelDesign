namespace AbstractFactoryDesignPattern.Products
{
    public class MacButton : IButton
    {
        public void Render() => Console.WriteLine("Rendered Mac Button");
    }
}