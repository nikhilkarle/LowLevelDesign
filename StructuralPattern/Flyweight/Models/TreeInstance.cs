using FlyweightPattern.Flyweights;

namespace FlyweightPattern.Models
{
    public class TreeInstance
    {
        private readonly ITreeType _type; 

        private readonly int _x;
        private readonly int _y;
        private readonly int _scale;

        public TreeInstance(ITreeType type, int x, int y, int scale)
        {
            _type = type;
            _x = x;
            _y = y;
            _scale = scale;
        }

        public void Draw()
        {
            _type.Draw(_x, _y, _scale);
        }
    }
}
