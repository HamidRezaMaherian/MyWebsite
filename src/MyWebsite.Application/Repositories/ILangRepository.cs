using MyWebsite.Domain.Base;

namespace MyWebsite.Application.Repositories.Contract
{
   public interface ILangRepository<T> : IRepository<T> where T : BaseLanguage
   {
   }
}
