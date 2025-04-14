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

            //string filePath = "C:\\Users\\vladk\\Desktop\\Text.txt";
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //string filePath = Path.Combine("C:", "Users", "vladk", "Desktop", "Text.txt");
            string filePath = Path.Combine(desktopPath, "Text.txt");
            if (File.Exists(filePath))
            {
                try
                {
                    string text = File.ReadAllText(filePath);
                    var noPunctuationText = new string(text.Where(c => !char.IsPunctuation(c)).ToArray());

                    // Используем LinkedList вместо List
                    LinkedList<string> words = new LinkedList<string>(
                        noPunctuationText.Split(new[] { ' ', '\n', '\r', '\t' },
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
//Время обработки: 69 мс
//---------------------- 

//несколько рекомендаций, которые помогут вам в дальнейшем при решении задач, а также улучшит вашу работу:
//1.Если использовать Path.Combine теперь код будет работать под любой ОС!
//2.На производительность списков сильно влияет его размерность capacity более подробно тут
//https://learn.microsoft.com/ru-ru/dotnet/api/system.collections.generic.list-1.capacity?view=net-7.0.
//3.Внутри коллекций бывает используются массивы как в обычном списке (list) или hashset как в словарике (dictionary)
//более подробно когда кого выбирать можно прочить тут https://learn.microsoft.com/ru-ru/dotnet/standard/collections/. 

