using MediatR;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Data;
using StargateAPI.Business.Dtos;
using StargateAPI.Controllers;

namespace StargateAPI.Business.Queries
{
    public class GetPeople : IRequest<GetPeopleResult>
    {

    }

    public class GetPeopleHandler : IRequestHandler<GetPeople, GetPeopleResult>
    {
        public readonly StargateContext _context;
        public GetPeopleHandler(StargateContext context)
        {
            _context = context;
        }
        public async Task<GetPeopleResult> Handle(GetPeople request, CancellationToken cancellationToken)
        {
            var result = new GetPeopleResult();

            result.People = await _context.People.GroupJoin(
                        _context.AstronautDetails,
                        p => p.Id,
                        ad => ad.PersonId,
                        (person, astronautDetail) => new { person, astronautDetail })
                        .SelectMany(
                            joinedSet => joinedSet.astronautDetail.DefaultIfEmpty(),
                            (person, astronautDetail) => new PersonAstronaut()
                            {
                                PersonId = person.person.Id,
                                Name = person.person.Name,
                                CurrentRank = astronautDetail == null ? string.Empty : astronautDetail.CurrentRank,
                                CurrentDutyTitle = astronautDetail == null ? string.Empty : astronautDetail.CurrentDutyTitle,
                                CareerStartDate = astronautDetail == null ? null : astronautDetail.CareerStartDate,
                                CareerEndDate = astronautDetail == null ? null : astronautDetail.CareerEndDate
                            }
                        ).ToListAsync(cancellationToken);

            return result;
        }
    }

    public class GetPeopleResult : BaseResponse
    {
        public List<PersonAstronaut> People { get; set; } = new List<PersonAstronaut> { };

    }
}
