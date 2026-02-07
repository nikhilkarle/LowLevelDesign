using AbstractFactoryDesignPattern.Products;

namespace AbstractFactoryDesignPattern.Factories
{
    public interface IUIFactory
    {
        IButton CreateButton();
        ICheckbox CreateCheckbox();
    }
}