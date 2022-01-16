using FPLCompanion.Data.Entities;

namespace FPLCompanion.Data.ViewModels
{
    public class ElementAggregate : Element
    {
        public List<Team> teamsInfo { get; set; }

        public List<ElementType> positionsInfo { get; set; }

        public Team teamInfo 
        {
            get
            {
                return teamsInfo.FirstOrDefault();
            }
        }

        public ElementType positionInfo
        {
            get
            {
                return positionsInfo.FirstOrDefault();
            }
        }
    }
}
