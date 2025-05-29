using System.Threading.Tasks.Dataflow;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Data;
using StargateAPI.Business.Dtos;

namespace StargateAPI.Business.Queries
{
    public static class PeopleQueries
    {
        public static async Task<PersonAstronaut?> GetPersonByNameAsync(StargateContext dbContext, string name, CancellationToken cancellationToken)
        {
            var people = from p in dbContext.People
                         join ad in dbContext.AstronautDetails
                         on p.Id equals ad.PersonId into grp
                         from pa in grp.DefaultIfEmpty()
                         where p.Name == name
                         select new PersonAstronaut()
                         {
                             PersonId = p.Id,
                             Name = p.Name,
                             CurrentRank = pa == null ? string.Empty : pa.CurrentRank,
                             CurrentDutyTitle = pa == null ? string.Empty : pa.CurrentDutyTitle,
                             CareerStartDate = pa == null ? null : pa.CareerStartDate,
                             CareerEndDate = pa == null ? null : pa.CareerEndDate
                         };
            return await people.FirstOrDefaultAsync(cancellationToken);
        }
    }
}