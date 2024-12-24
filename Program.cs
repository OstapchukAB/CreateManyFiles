using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        // Укажите путь, куда хотите записывать файлы
        // Получаем полный путь к самому .exe
        string exePath = Assembly.GetExecutingAssembly().Location;

        // Извлекаем из него путь к каталогу
        string exeDirectory = Path.GetDirectoryName(exePath);
        string directoryPath = Path.Combine(exeDirectory,"Files");

        // Создаем директорию, если не существует
        Directory.CreateDirectory(directoryPath);

        // Для оценки скорости работы
        Stopwatch sw = new Stopwatch();
        sw.Start();

        // Запускаем параллельное создание 50 000 файлов
        Parallel.For(0, 50000, i =>
        {
            // Формируем имя файла: метка времени + порядковый номер
            // Пример: 20241221_104530_123_0.txt
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
            string fileName = $"{i:00000}_{timestamp}.txt";
            string fullPath = Path.Combine(directoryPath, fileName);

            // Создаем файл и записываем внутрь порядковый номер
            File.WriteAllText(fullPath, i.ToString("00000"));
        });

        sw.Stop();

        Console.WriteLine($"Создано 50 000 файлов за {sw.Elapsed.TotalSeconds} секунд.");
    }
}
