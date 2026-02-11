using VisitorPattern.Visitors;

namespace VisitorPattern.Shapes
{
    public interface IShape
    {
        void Accept(IShapeVisitor visitor);
    }
}