using OpenCvSharp;

using PhotoPreprocessing;

namespace Simple
{
    internal class Program
    {
        public static readonly List<string> ImageExtensions = new List<string>() { ".JPG", ".JPEG", ".BMP", ".GIF", ".PNG" };
        static void Main()
        {
            PrepareImageDir();
        }

        private static void PrepareImageDir()
        {
            Console.WriteLine("Set path to dir with images:");
            string path = Console.ReadLine() ?? throw new ArgumentNullException("Путь должен существовать!");
            int size = 224;

            DirectoryInfo topDir = new DirectoryInfo(path);
            DirectoryInfo[] paths = topDir.GetDirectories();

            Console.WriteLine($"Число классов: {paths.Length}");

            foreach (DirectoryInfo dirInfo in paths)
            {
                Console.WriteLine($"Название каталога: {dirInfo.Name}");
                Console.WriteLine($"Полное название каталога: {dirInfo.FullName}");
                Console.WriteLine($"Корневой каталог: {dirInfo.Root}");
                Console.WriteLine($"Размер изображения приводится к {size}");

                if (dirInfo.Exists)
                {
                    FileInfo[] oldFiles = dirInfo.GetFiles();

                    _ = Parallel.ForEach(oldFiles, file =>
                    {
                        if (file.Exists && ImageExtensions.Contains(file.Extension.ToUpperInvariant()))
                        {
                            try
                            {
                                _ = new SelfTransformer(new Mat(file.FullName)).
                                Filter(5).
                                EqualizeHist().
                                GammaCorrectionFilter().
                                Scale(size).
                                Save(dirInfo.FullName, file.Name);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"{ex.Message}\n{ex.StackTrace}");
                            }
                        }
                    });

                    Console.WriteLine($"Изменение изображений в каталоге {dirInfo.Name} закончено.");
                }
            }

            Console.WriteLine($"Изменение изображений закончено.");
            Console.WriteLine("Хотите сменить каталог? [y/n]");
            string answer = Console.ReadLine() ?? "n";
            if (answer.ToLower().StartsWith("y"))
            {
                PrepareImageDir();
            }
            else
            {
                Console.WriteLine("Press any button for exit.");
                _ = Console.ReadLine();
            }
        }
    }
}