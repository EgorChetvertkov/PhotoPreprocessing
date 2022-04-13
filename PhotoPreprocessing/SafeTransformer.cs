using OpenCvSharp;

using PhotoPreprocessing.Interfaces;

using System.Text;

namespace PhotoPreprocessing
{
    public class SafeTransformer : ISafeScalator, ISafeSaver, ISafeEqualizer, ISafeFilter
    {
        private Mat _image;

        public Mat Image { set { _image = value ?? throw new ArgumentNullException(nameof(value), "Изображение должно существовать!"); } }

        public SafeTransformer(Mat image)
        {
            _image = image ?? throw new ArgumentNullException(nameof(image), "Изображение должно существовать!");
        }

        public SafeTransformer EqualizeHist()
        {
            if (_image == null)
            {
                throw new ArgumentNullException(nameof(_image), "Изображение должно существовать!");
            }

            Mat src = _image;
            Mat[] channels = Cv2.Split(src);

            Cv2.EqualizeHist(channels[0], channels[0]);
            Cv2.EqualizeHist(channels[1], channels[1]);
            Cv2.EqualizeHist(channels[2], channels[2]);

            Cv2.Merge(channels, _image);

            return this;
        }

        public SafeTransformer Save(string pathToSave, string fileName)
        {
            if (_image == null)
            {
                throw new ArgumentNullException(nameof(_image), "Изображение должно существовать!");
            }

            string path = new StringBuilder(pathToSave)
                .Append("\\")
                .Append(fileName)
                .ToString();

            if (_image.SaveImage(path))
            {
                _image.Dispose();
            }
            else
            {
                throw new IOException(nameof(path));
            }

            return this;            
        }

        public SafeTransformer Scale(int newShape)
        {
            if (_image == null)
            {
                throw new ArgumentNullException(nameof(_image), "Изображение должно существовать!");
            }

            Mat src = _image;
            Cv2.Resize(src, _image, new Size(newShape, newShape), 0, 0, InterpolationFlags.Linear);
            return this;
        }

        public SafeTransformer Scale(Size newShape)
        {
            if (_image == null)
            {
                throw new ArgumentNullException(nameof(_image), "Изображение должно существовать!");
            }

            Mat src = _image;
            Cv2.Resize(src, _image, newShape, 0, 0, InterpolationFlags.Linear);
            return this;
        }

        public SafeTransformer Filter(int kernelSize)
        {
            if (_image == null)
            {
                throw new ArgumentNullException(nameof(_image), "Изображение должно существовать!");
            }

            Mat src = _image;
            Cv2.MedianBlur(src, _image, kernelSize);

            return this;
        }

        public SafeTransformer ContrastFilter()
        {
            if (_image == null)
            {
                throw new ArgumentNullException(nameof(_image), "Изображение должно существовать!");
            }

            Mat scr = _image;
            int[,] kernel = new int[3, 3] { { -1, -1, -1}, { -1, 9, -1}, {-1, -1, -1 } };
            Cv2.Filter2D(scr, _image, -1, InputArray.Create(kernel));

            return this;
        }

        public SafeTransformer SobelFilter()
        {
            if (_image == null)
            {
                throw new ArgumentNullException(nameof(_image), "Изображение должно существовать!");
            }

            Mat src = _image;
            Mat X = new();
            Mat Y = new();

            Cv2.GaussianBlur(_image, src, new Size(3, 3), 0);
            Cv2.CvtColor(src, src, ColorConversionCodes.BGR2GRAY);
            Cv2.Sobel(src, X, MatType.CV_16S, 1, 0);
            Cv2.Sobel(src, Y, MatType.CV_16S, 0, 1);
            Cv2.ConvertScaleAbs(X, X);
            Cv2.ConvertScaleAbs(Y, Y);
            Cv2.AddWeighted(X, 0.5, Y, 0.5, 0, _image);

            return this;
        }

        public SafeTransformer GammaCorrectionFilter()
        {
            if (_image == null)
            {
                throw new ArgumentNullException(nameof(_image), "Изображение должно существовать!");
            }

            double Gamma = 0.5;
            Mat src = _image;
            Mat[] channels = Cv2.Split(src);

            byte[] lut = new byte[256];

            for (int i = 0; i < lut.Length; i++)
            {
                lut[i] = (byte)(Math.Pow(i / 255.0, 1.0 / Gamma) * 255.0);
            }

            for (int i = 0; i < channels.Length; i++)
            {
                Cv2.LUT(channels[i], lut, channels[i]);
            }

            Cv2.Merge(channels, _image);

            return this;
        }
    }
}
