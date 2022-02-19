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
            
    }
}
