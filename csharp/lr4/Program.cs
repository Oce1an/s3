using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lab4.Builders;
using Lab4.Comparers;
using Lab4.Entities;
using Lab4.Models;
using Lab4.Services;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            string dirName = "Roman_Lab4";

            // h) Создание/очистка папки
            if (Directory.Exists(dirName))
            {
                Console.WriteLine("Папка уже существует. Удаление содержимого...");
                foreach (var file in Directory.GetFiles(dirName)) File.Delete(file);
                foreach (var dir in Directory.GetDirectories(dirName)) Directory.Delete(dir, true);
            }
            else
            {
                Directory.CreateDirectory(dirName);
            }

            // h) Генерация 10 пустых файлов со случайными расширениями
            string[] exts = { ".txt", ".rtf", ".dat", ".inf" };
            Random rnd = new Random();
            for (int i = 0; i < 10; i++)
            {
                string randomName = Path.GetRandomFileName();
                string ext = exts[rnd.Next(exts.Length)];
                string baseName = Path.GetFileNameWithoutExtension(randomName);
                string filePath = Path.Combine(dirName, baseName + ext);
                File.Create(filePath).Close();
            }

            // h) Вывод списка файлов
            Console.WriteLine("\n--- Содержимое папки ---");
            foreach (var file in Directory.GetFiles(dirName))
            {
                Console.WriteLine($"Файл:{Path.GetFileName(file)} имеет расширение{Path.GetExtension(file)}");
            }

            // h) Создание коллекции из 50 объектов
            List<Client> clients = new List<Client>();
            string[] names = { "Ivanov", "Petrov", "Sidorov", "Smirnov", "Kuznetsov" };
            for (int i = 0; i < 50; i++)
            {
                clients.Add(new Client
                {
                    Name = names[rnd.Next(names.Length)] + "_" + i,
                    Age = rnd.Next(18, 65),
                    Discount = rnd.Next(0, 30),
                    IsCorporate = rnd.Next(2) == 1
                });
            }

            // h) Запись, переименование и чтение
            var fileService = new FileService();
            string dataFile = Path.Combine(dirName, "clients.dat");
            string newFile = Path.Combine(dirName, "clients_renamed.dat");

            fileService.SaveData(clients, dataFile);
            File.Move(dataFile, newFile);

            List<Client> readClients = fileService.ReadFile(newFile).ToList();

            // h) Сортировка с помощью MyCustomComparer
            var sortedByComparer = readClients.OrderBy(c => c, new MyCustomComparer<Client>()).ToList();

            Console.WriteLine("\n--- Исходная коллекция (первые 5) ---");
            readClients.Take(5).ToList().ForEach(Console.WriteLine);

            Console.WriteLine("\n--- Отсортированная по Name (MyCustomComparer) (первые 5) ---");
            sortedByComparer.Take(5).ToList().ForEach(Console.WriteLine);

            // h) Сортировка по другому свойству (Age) через lambda
            var sortedByAge = readClients.OrderBy(c => c.Age).ToList();
            Console.WriteLine("\n--- Отсортированная по Age (Lambda) (первые 5) ---");
            sortedByAge.Take(5).ToList().ForEach(Console.WriteLine);

            // h) Фильтрация с помощью QueryBuilder
            Console.WriteLine("\n--- Фильтрация ---");

            var filter1 = new FilterData { MinAge = 40 };
            var expr1 = QueryBuilder.Build(filter1);
            var res1 = readClients.Where(expr1.Compile()).ToList();
            Console.WriteLine($"Условие: Возраст >= 40. Найдено: {res1.Count}");
            res1.Take(3).ToList().ForEach(c => Console.WriteLine(c));

            var filter2 = new FilterData { MinDiscount = 15, IsCorporate = true };
            var expr2 = QueryBuilder.Build(filter2);
            var res2 = readClients.Where(expr2.Compile()).ToList();
            Console.WriteLine($"\nУсловие: Скидка >= 15% И Юр. лицо = true. Найдено: {res2.Count}");
            res2.Take(3).ToList().ForEach(c => Console.WriteLine(c));
        }
    }
}