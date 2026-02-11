using VisitorPattern.Visitors;

namespace VisitorPattern.Shapes
{
    public class Triangle : IShape
    {
        public int Base{get; set;}
        public int Height{get;set;}

        public Triangle(int basee, int height)
        {
            Base = basee;
            Height = height;
        }

        public void Accept(IShapeVisitor visitor)
        {
            visitor.VisitTriangle(this);
        }
    }
}