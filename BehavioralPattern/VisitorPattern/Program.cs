using VisitorPattern.Shapes;
using VisitorPattern.Visitors;

namespace VisitorPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<IShape> shapes = new List<IShape>();
            shapes.Add(new Circle(2));
            shapes.Add(new Rectangle(3, 4));
            shapes.Add(new Triangle(4, 3));

            RenderShape renderer = new RenderShape();
            for (int i = 0; i < shapes.Count; i++)
            {
                shapes[i].Accept(renderer);
            }

            Console.WriteLine();

            AreaCalculator area = new AreaCalculator();
            for (int i = 0; i < shapes.Count; i++)
            {
                shapes[i].Accept(area);
            }

            Console.WriteLine("Total Area: {0}", area.TotalArea);

        }
    }
}