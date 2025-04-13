using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace BaseCollections
{
    class Program
    {
        static void Main(string[] args)
        {

            // Создаем и запускаем таймер
            Stopwatch stopwatch = Stopwatch.StartNew();

            string filePath = "C:\\Users\\vladk\\Desktop\\Text.txt";
            if (File.Exists(filePath))
            {
                try
                {
                    string text = File.ReadAllText(filePath);
                    var noPunctuationText = new string(text.Where(c => !char.IsPunctuation(c)).ToArray());

                    List<string> words = new List<string>(noPunctuationText.Split(new[] { ' ', ',', '.', '!', '?', '\n', '\r', '\t' },
                                                 StringSplitOptions.RemoveEmptyEntries));

                    // Останавливаем таймер
                    stopwatch.Stop();

                    // Вывод результатов
                    Console.WriteLine($"Найдено {words.Count} слов:");
                    Console.WriteLine($"Время обработки: {stopwatch.ElapsedMilliseconds} мс");
                    Console.WriteLine("-----------------------");

                    //foreach (var word in words)
                    //{
                    //    Console.WriteLine(word);
                    //}

                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine("Нет прав на чтение файла.");
                }
            }
            else
            {
                Console.WriteLine("Файл не найден.");
            }
        }

    }
}

//Найдено 147853 слов:
//Время обработки: 52 мс
//---------------------- -