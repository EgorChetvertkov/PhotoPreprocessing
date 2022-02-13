using OpenCvSharp;

namespace PhotoPreprocessing.Interfaces
{
    internal interface IEqualizer
    {
        Mat EqualizeHist(Mat image);
    }

    internal interface ISafeEqualizer
    {
        SafeTransformer EqualizeHist();
    }
}
