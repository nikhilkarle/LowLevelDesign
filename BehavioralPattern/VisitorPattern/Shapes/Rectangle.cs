using VisitorPattern.Visitors;

namespace VisitorPattern.Shapes
{
    public class Rectangle : IShape
    {
        public int Width{get; set;}
        public int Height{get;set;}

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void Accept(IShapeVisitor visitor)
        {
            visitor.VisitRectangle(this);
        }
    }
}