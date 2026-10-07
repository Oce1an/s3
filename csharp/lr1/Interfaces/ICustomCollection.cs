namespace Interfaces
{
	public interface ICustomCollection<T>
	{
		T this[int index] { get; set; }		// индексатор коллекции
		T Current();						// метод, возвращает элемент текущего положения курсора
		T RemoveCurrent();					// метод, удаляет элемент текущего положения курсора

		void Reset();						 // метод, устанавливает курсор в начало коллекции 
		void MoveNext();					 // метод, перемещает курсор на следующий элемент коллекции
		void Add(T item);					 // метод, добавляет объект item в конец коллекции
		void Remove(T item);				 // метод, удаляет объект item из коллекции

		int Count { get; }					 // свойство, возвращает количество элементов в коллекции
	}
}