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

                    List<string> words = new List<string>(noPunctuationText.Split(new[] { ' ', '\n', '\r', '\t' },
                                                 StringSplitOptions.RemoveEmptyEntries));

                    // Создаем словарь для подсчета частоты слов
                    Dictionary<string, int> wordFrequency = new Dictionary<string, int>();
                    foreach (var word in words)
                    {
                        if (word.Length <= 2) continue; // Пропускаем короткие слова

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
//---------------------- -
