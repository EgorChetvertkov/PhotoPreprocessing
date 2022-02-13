using OpenCvSharp;

namespace PhotoPreprocessing.Interfaces
{
    internal interface IScalator
    {
        Mat Scale(Mat image, int newShape);
        Mat Scale(Mat image, Size newShape);
    }

    internal interface ISafeScalator
    {
        SafeTransformer Scale(int newShape);
        SafeTransformer Scale(Size newShape);
    }
}
