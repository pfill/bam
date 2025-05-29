using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Data;

namespace StargateAPI.Business.Queries
{
    public static class AstronautDutyQueries
    {
        public static async Task<List<AstronautDuty>> GetAstronautDutiesByPersonIdAsync(StargateContext dbContext, int personId, CancellationToken cancellationToken)
        {
            return await dbContext.AstronautDuties
                                        .Where(ad => ad.PersonId == personId)
                                        .OrderByDescending(ad => ad.DutyStartDate).ToListAsync(cancellationToken);
        }
    }
}