namespace Interfaces
{
	public interface ICustomCollection<T>
	{
		T this[int index] { get; set; }	
		T Current();					
		T RemoveCurrent();				

		void Reset();					
		void MoveNext();					
		void Add(T item);				
		void Remove(T item);			

		int Count { get; }				
	}
}