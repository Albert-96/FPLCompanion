using FPLCompanion.Data.Entities;
namespace FPLCompanion.DataService.Abstractions
{
    public interface IDreamTeamRepository : IEntityRepository<DreamTeam>
    {
        Task UpdateMany(List<DreamTeam> elements);
    }
}
