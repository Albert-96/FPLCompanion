using FPLCompanion.Data.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLCompanion.DataService.Abstractions
{
    public interface ITeamDataService
    {
        IMongoCollection<Team> _teamsCollection { get; set; }

        Task UpdateAsync(int id, Team player);

        Task UpdateMany(List<Team> elements);

        Task CreateAsync(Team player);

        Task InsertMany(List<Team> players);

        Task RemoveAsync(string id);
    }
}
