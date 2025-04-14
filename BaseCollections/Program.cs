
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
                    if (string.IsNullOrEmpty(text))
                    {
                        Console.WriteLine("Файл пуст или содержит только пробелы.");
                        return;
                    }

                    var noPunctuationText = new string(text.Where(c => !char.IsPunctuation(c)).ToArray());
                    //var words = noPunctuationText
                    //     .Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                    //     .Where(word => !string.IsNullOrEmpty(word) && word.Length > 2)      // Фильтрация  IsNullOrWhiteSpace - менее строгая проверка
                    //     .ToList();
                    var words = noPunctuationText
                         .Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                         .Where(word => !string.IsNullOrWhiteSpace(word) && word.Length > 2)  // Фильтрация - отсеивает строки, состоящие только из пробелов
                         .ToList();

                    //List<string> words = new List<string>(noPunctuationText.Split(new[] { ' ', '\n', '\r', '\t' },
                    //                             StringSplitOptions.RemoveEmptyEntries));

                    // Создаем словарь для подсчета частоты слов
                    var wordFrequency = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase); // Без учета регистра
                    
                    //Dictionary<string, int> wordFrequency = new Dictionary<string, int>();
                    foreach (var word in words)
                    {
                        //if (word.Length <= 2) continue; // Пропускаем короткие слова

                        if (wordFrequency.ContainsKey(word))
                        {
                            wordFrequency[word]++;
                        }
                        else
                        {
                            wordFrequency[word] = 1;
                        }
                    }

                    // Преобразуем словарь в список для сортировки
                    List<KeyValuePair<string, int>> sortedWords = new List<KeyValuePair<string, int>>(wordFrequency);

                    // Сортируем по убыванию частоты
                    sortedWords.Sort((a, b) => b.Value.CompareTo(a.Value));

                    // Выводим топ-10 слов
                    Console.WriteLine("Топ-10 самых частых слов:");
                    Console.WriteLine("--------------------------");
                    Console.WriteLine("Слово\t\tКоличество");
                    Console.WriteLine("--------------------------");

                    int count = 0;
                    foreach (var pair in sortedWords)
                    {
                        if (count++ >= 10) break;
                        Console.WriteLine($"{pair.Key}\t\t{pair.Value}");
                    }

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


//Топ - 10 самых частых слов:
//--------------------------
//Слово           Количество
//--------------------------
//что             2412
//она             1634
//Как             1536
//все             965
//Обломов         911
//его             906
//Это             899
//так             813
//только          585
//сказал          552
//Найдено 111244 слов:
//Время обработки: 162 мс
//---------------------- 


//Топ - 10 самых частых слов:
//--------------------------
//Слово           Количество
//--------------------------
//что             1948
//как             1304
//она             1244
//Обломов         911
//его             886
//все             869
//это             735
//так             715
//сказал          548
//только          546

//Найдено 147853 слов:
//Время обработки: 78 мс
//---------------------- 

//Есть несколько рекомендаций, которые помогут сделать вашу работу ещё лучше:
//1.Используйте Path.Combine для составления путей к файлам. Что позволит создать универсальный путь к файлу в разных ОС (в разных разный слеш),
//тут более подробно можно прочитать https://learn.microsoft.com/ru-ru/dotnet/api/system.io.path.combine?view=net-7.0. 
//2.Можно сократить запись получения первых 10 записей из словарика примерно так var newDic = oldDic.OrderBy(g => g.Key).Take(10);.
//3.Для проверки строки на пустоту и null есть специальное расширение для этого String.IsNullOrEmpty(String) более подробно можно прочитать
//тут https://learn.microsoft.com/ru-ru/dotnet/api/system.string.isnullorempty?view=net-7.0.
