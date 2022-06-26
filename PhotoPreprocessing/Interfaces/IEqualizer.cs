using OpenCvSharp;

namespace PhotoPreprocessing.Interfaces
{
    internal interface IEqualizer
    {
        Mat EqualizeHist(Mat image);
    }

    internal interface ISalfEqualizer
    {
        SelfTransformer EqualizeHist();
    }
}
