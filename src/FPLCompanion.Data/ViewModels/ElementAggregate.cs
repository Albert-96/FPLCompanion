using FPLCompanion.Data.Entities;

namespace FPLCompanion.Data.ViewModels
{
    public class ElementAggregate : Element
    {
        public Team teamInfo { get; set; }

        public ElementType positionInfo { get; set; }
    }
}
