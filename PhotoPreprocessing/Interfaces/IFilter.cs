using OpenCvSharp;

namespace PhotoPreprocessing.Interfaces
{
    internal interface IFilter
    {
        Mat Filter(Mat image, int kernelSize);
    }

    internal interface ISafeFilter
    {
        SafeTransformer Filter(int kernelSize);
    }
}
