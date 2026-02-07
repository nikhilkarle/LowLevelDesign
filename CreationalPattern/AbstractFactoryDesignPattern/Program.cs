using AbstractFactoryDesignPattern.Factories;

class Program
{
    static void Main()
    {
        string theme = "Mac";

        var factory = FactoryProvider.GetFactory(theme);

        var button = factory.CreateButton();
        var checkbox = factory.CreateCheckbox();

        button.Render();
        checkbox.Render();

    }

}