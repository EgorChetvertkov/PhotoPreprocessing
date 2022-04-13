using OpenCvSharp;

namespace PhotoPreprocessing.Interfaces
{
    internal interface IFilter
    {
        Mat Filter(Mat image, int kernelSize);
        Mat ContrastFilter();
        Mat SobelFilter(Mat image);

        Mat GammaCorrection(Mat image);
    }

    internal interface ISafeFilter
    {
        SafeTransformer Filter(int kernelSize);
        SafeTransformer ContrastFilter();
        SafeTransformer SobelFilter();
        SafeTransformer GammaCorrectionFilter();
    }
}
