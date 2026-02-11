using VisitorPattern.Visitors;

namespace VisitorPattern.Shapes
{
    public class Circle : IShape
    {
        public int Radius{get; set;}

        public Circle(int radius)
        {
            Radius = radius;
        }

        public void Accept(IShapeVisitor visitor)
        {
            visitor.VisitCircle(this);
        }
    }
}