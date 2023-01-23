using MyWebsite.Application.Repositories.Contract;
using MyWebsite.Domain.Entities.Info;
using System.Linq.Expressions;

namespace MyWebsite.Application.Repositories
{
   public interface IAboutMeKeyValueRepo : ILangRepository<AboutMeKeyValue>
   {
   }
}
