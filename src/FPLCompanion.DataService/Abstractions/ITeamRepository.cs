using FPLCompanion.Data.Entities;

namespace FPLCompanion.DataService.Abstractions
{
    public interface ITeamRepository : IEntityRepository<Team>
    {
        Task UpdateMany(List<Team> teams);
    }
}
