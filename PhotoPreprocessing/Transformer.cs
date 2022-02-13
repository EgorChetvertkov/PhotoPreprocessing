using OpenCvSharp;

using PhotoPreprocessing.Interfaces;

using System.Text;

namespace PhotoPreprocessing
{
    public class Transformer : IScalator, ISaver, IEqualizer
    {
        public Transformer()
        {

        }

        public Mat EqualizeHist(Mat image)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image), "Изображение должно существовать!");
            }

            Mat src = image;
            Mat dst = image;

            Mat[] channels = Cv2.Split(src);

            Cv2.EqualizeHist(channels[0], channels[0]);
            Cv2.EqualizeHist(channels[1], channels[1]);
            Cv2.EqualizeHist(channels[2], channels[2]);

            Cv2.Merge(channels, dst);

            return dst;
        }

        public void Save(Mat image, string pathToSave, string fileName)
        {
            string path = new StringBuilder(pathToSave)
                .Append('\\')
                .Append(fileName)
                .ToString();

            if (!image.SaveImage(path))
            {
                throw new Exception("Изображение не удалось сохранить!");
            }
        }

        public Mat Scale(Mat image, int newShape = 224)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image), "Изображение должно существовать!");
            }
            
            if (newShape <= 0)
            {
                throw new ArgumentException("Новая размерность должна быть положителньой!", nameof(newShape));
            }

            Mat src = image;
            Mat dst = image;
            Cv2.Resize(src, dst, new Size(newShape, newShape), 0, 0, InterpolationFlags.Linear);
            return dst;
        }

        public Mat Scale(Mat image, Size newShape)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image), "Изображение должно существовать!");
            }

            if (newShape.Height <= 0 || newShape.Width <= 0)
            {
                throw new ArgumentException("Новая размерность должна быть положителньой!", nameof(newShape));
            }

            Mat src = image;
            Mat dst = image;
            Cv2.Resize(src, dst, newShape, 0, 0, InterpolationFlags.Linear);
            return dst;
        }
    }
}
