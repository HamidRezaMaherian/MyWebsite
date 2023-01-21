using MyWebsite.Domain.Entities;

namespace MyWebsite.Application.Repositories.Contract
{
   public interface ISiteVisitRepo : IRepository<SiteVisit>
   {
      public IEnumerable<object> GetAll(ushort year, byte month, string endpoint);

      public IEnumerable<object> GetThisMonthDays(DateTime date, string endpoint);

      public IEnumerable<object> GetThisWeek(DateTime date, string endpoint);

      public IEnumerable<object> GetDayHour(DateTime date, string endpoint);
   }
}
