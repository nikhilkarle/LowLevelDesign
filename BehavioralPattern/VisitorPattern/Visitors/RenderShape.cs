using VisitorPattern.Shapes;

namespace VisitorPattern.Visitors
{
    public class RenderShape : IShapeVisitor
    {
        public void VisitCircle(Circle circle)
        {
            Console.WriteLine("Render: Circle (radius=" + circle.Radius + ")");
        }

        public void VisitRectangle(Rectangle rectangle)
        {
            Console.WriteLine("Render: Rectangle (width=" + rectangle.Width + ", height=" + rectangle.Height + ")");
        }

        public void VisitTriangle(Triangle triangle)
        {
            Console.WriteLine("Render: Triangle (base=" + triangle.Base + ", height=" + triangle.Height + ")");
        }
    }
}