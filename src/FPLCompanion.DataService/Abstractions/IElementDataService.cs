using FPLCompanion.Data.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPLCompanion.DataService.Abstractions
{
    public interface IElementDataService
    {
        IMongoCollection<Element> _elementsCollection { get; set; }

        Task UpdateAsync(int id, Element player);

        Task CreateAsync(Element player);

        Task InsertMany(List<Element> players);

        Task RemoveAsync(string id);
    }
}
