namespace FlyweightPattern.Flyweights
{
    public class TreeType : ITreeType
    {
        public string Name {get;}
        public string TextureFile {get;}

        private readonly int _height;
        private readonly int _width;

        public TreeType(string name, string textureFile, int height, int width)
        {
            Name = name;
            TextureFile = textureFile;
            _height = height;
            _width = width;
        }

        public void Draw(int x, int y, int scale)
        {
            int finalWidth = (_width * scale);
            int finalHeight = (_height * scale);

            Console.WriteLine(
                "Draw " + Name +
                " at (" + x + "," + y + ")" +
                " texture=" + TextureFile +
                " size=" + finalWidth + "x" + finalHeight
            );
        }
    }
}