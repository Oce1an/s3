using System;

namespace Exceptions
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException()
            : base("Элемент не найден в коллекции.") { }

        public ItemNotFoundException(string message)
            : base(message) { }

        public ItemNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}