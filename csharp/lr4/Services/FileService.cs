using System;
using System.Collections.Generic;
using System.IO;
using Lab4.Entities;
using Lab4.Interfaces;

namespace Lab4.Services
{
	public class FileService : IFileService<Client>
	{
		public void SaveData(IEnumerable<Client> data, string fileName)
		{
			if (File.Exists(fileName))
			{
				File.Delete(fileName);
			}

			try
			{
				using (var fs = new FileStream(fileName, FileMode.CreateNew))
				using (var writer = new BinaryWriter(fs))
				{
					foreach (var client in data)
					{
						writer.Write(client.Name);
						writer.Write(client.Age);
						writer.Write(client.Discount);
						writer.Write(client.IsCorporate);
					}
				}
			}
			catch (IOException ex)
			{
				Console.WriteLine($"Ошибка записи IO: {ex.Message}");
			}
		}

		// Публичный метод остается именованным итератором (yield return)
		public IEnumerable<Client> ReadFile(string fileName)
		{
			foreach (var client in ReadClientsInternal(fileName))
			{
				yield return client;
			}
		}

		// Вспомогательный метод для обработки исключений
		private List<Client> ReadClientsInternal(string fileName)
		{
			var result = new List<Client>();
			try
			{
				using (var fs = new FileStream(fileName, FileMode.Open))
				using (var reader = new BinaryReader(fs))
				{
					while (reader.BaseStream.Position < reader.BaseStream.Length)
					{
						string name = reader.ReadString();
						int age = reader.ReadInt32();
						int discount = reader.ReadInt32();
						bool isCorp = reader.ReadBoolean();

						result.Add(new Client
						{
							Name = name,
							Age = age,
							Discount = discount,
							IsCorporate = isCorp
						});
					}
				}
			}
			catch (EndOfStreamException ex)
			{
				Console.WriteLine($"Предупреждение чтения: {ex.Message}");
			}
			catch (IOException ex)
			{
				Console.WriteLine($"Ошибка чтения IO: {ex.Message}");
			}

			return result;
		}
	}
}