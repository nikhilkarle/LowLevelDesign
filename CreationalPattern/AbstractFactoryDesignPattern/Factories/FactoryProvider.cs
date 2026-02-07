using System;

namespace AbstractFactoryDesignPattern.Factories
{
    public static class FactoryProvider
    {
        public static IUIFactory GetFactory(string theme)
        {
            if (theme == "Windows") return new WindowsUIFactory();
            if (theme == "Mac") return new MacUIFactory();

            throw new ArgumentException($"Unknown these {theme}");
        }
    }
}