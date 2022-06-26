using OpenCvSharp;

namespace PhotoPreprocessing.Interfaces
{
    internal interface ISaver
    {
        void Save(Mat image, string pathToSave, string fileName);
    }

    internal interface ISalfSaver
    {
        SelfTransformer Save(string pathToSave, string fileName);
    }
}
