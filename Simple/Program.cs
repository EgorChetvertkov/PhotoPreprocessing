using OpenCvSharp;

using PhotoPreprocessing;

namespace Simple
{
    internal class Program
    {
        public static readonly List<string> ImageExtensions = new List<string>() { ".JPG", ".JPEG", ".BMP", ".GIF", ".PNG" };
        static void Main()
        {
            Console.WriteLine("Set path to dir with images:");
            string path = Console.ReadLine() ?? throw new ArgumentNullException("Путь должен существовать!");
            int size = 224;

            DirectoryInfo dirInfo = new(path);

            Console.WriteLine($"Название каталога: {dirInfo.Name}");
            Console.WriteLine($"Полное название каталога: {dirInfo.FullName}");
            Console.WriteLine($"Корневой каталог: {dirInfo.Root}");
            Console.WriteLine($"Размер изображения приводится к {size}");

            DirectoryInfo dirInfoScaled = new(dirInfo.FullName + "_scaled");

            if (!dirInfoScaled.Exists)
            {
                dirInfoScaled.Create();
            }

            if (dirInfo.Exists)
            {
                FileInfo[] oldFiles = dirInfo.GetFiles();

                _ = Parallel.ForEach(oldFiles, file =>
                  {
                      if (file.Exists && ImageExtensions.Contains(file.Extension.ToUpperInvariant()))
                      {
                          try
                          {
                              _ = new SafeTransformer(new Mat(file.FullName)).
                              Filter(5).
                              Scale(size).
                              EqualizeHist().
                              Filter(3).
                              Save(dirInfoScaled.FullName, file.GetHashCode().ToString() + ".png");
                              Console.WriteLine($"Save image {file.GetHashCode().ToString() + ".png"}");
                          }
                          catch (Exception ex)
                          {
                              Console.WriteLine($"{ex.Message}\n{ex.StackTrace}");
                          }
                      }
                  });
                _ = Console.ReadLine();
            }
        }
    }
}