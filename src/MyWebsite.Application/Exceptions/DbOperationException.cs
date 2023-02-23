using System.Runtime.Serialization;

namespace MyWebsite.Application.Exceptions
{
	public class DbOperationException : Exception
	{
		public DbOperationException()
		{
		}

		public DbOperationException(string message) : base(message)
		{
		}

		public DbOperationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected DbOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
