using AbstractFactoryDesignPattern.Products;

namespace AbstractFactoryDesignPattern.Factories
{
    public class MacUIFactory: IUIFactory
    {
        public IButton CreateButton() => new MacButton();
        public ICheckbox CreateCheckbox() => new MacCheckbox();
    }
}