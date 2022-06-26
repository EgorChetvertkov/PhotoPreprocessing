using OpenCvSharp;

namespace PhotoPreprocessing.Interfaces
{
    internal interface IScalator
    {
        Mat Scale(Mat image, int newShape);
        Mat Scale(Mat image, Size newShape);
    }

    internal interface ISelfScalator
    {
        SelfTransformer Scale(int newShape);
        SelfTransformer Scale(Size newShape);
    }
}
