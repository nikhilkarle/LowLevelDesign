using AbstractFactoryDesignPattern.Products;

namespace AbstractFactoryDesignPattern.Factories
{
    public class WindowsUIFactory: IUIFactory
    {
        public IButton CreateButton() => new WindowsButton();
        public ICheckbox CreateCheckbox() => new WindowsCheckbox();
    }
}