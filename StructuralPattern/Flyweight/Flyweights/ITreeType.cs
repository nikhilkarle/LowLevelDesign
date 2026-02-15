namespace FlyweightPattern.Flyweights
{
    public interface ITreeType
    {
        string Name {get; }
        string TextureFile {get; }

        void Draw(int x, int y, int scale);
    }
}