namespace StrategyPattern.Models
{
    public class CartItem
    {
        public string Name {get;}
        public decimal UnitPrice {get;}
        public int Quantity {get;}

        public CartItem(string name, decimal unitprice, int quantity)
        {
            Name = name;
            UnitPrice = unitprice;
            Quantity = quantity;
        }

        public decimal PerItemTotal => UnitPrice*Quantity;
    }

}