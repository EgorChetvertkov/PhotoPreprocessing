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

    internal interface ISelfFilter
    {
        SelfTransformer Filter(int kernelSize);
        SelfTransformer ContrastFilter();
        SelfTransformer SobelFilter();
        SelfTransformer GammaCorrectionFilter();
    }
}
