using VisitorPattern.Shapes;

namespace VisitorPattern.Visitors
{
    public interface IShapeVisitor
    {
        void VisitCircle(Circle circle);
        void VisitRectangle(Rectangle rectangle);
        void VisitTriangle(Triangle triangle);
    }
}