namespace MyWebsite.Infrastructure
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class RepositoryConcreteAttribute : Attribute
	{
		private Type repositoryType;

		public RepositoryConcreteAttribute(Type repositoryType)
		{
			RepositoryType = repositoryType;
		}

		public Type RepositoryType { get => repositoryType; private set => repositoryType = value; }
	}
}
