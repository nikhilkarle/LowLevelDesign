using VisitorPattern.Shapes;

namespace VisitorPattern.Visitors
{
    public class AreaCalculator : IShapeVisitor
    {
        public double TotalArea { get; private set; }

        public AreaCalculator()
        {
            TotalArea = 0.0;
        }

        public void VisitCircle(Circle circle)
        {
            double area = Math.PI * circle.Radius * circle.Radius;
            TotalArea = TotalArea + area;
        }

        public void VisitRectangle(Rectangle rectangle)
        {
            double area = rectangle.Width * rectangle.Height;
            TotalArea = TotalArea + area;
        }

        public void VisitTriangle(Triangle triangle)
        {
            double area = 0.5 * triangle.Base * triangle.Height;
            TotalArea = TotalArea + area;
        }
    }
}