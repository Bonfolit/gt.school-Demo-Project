using BonLib.Events;

namespace Core.Events.Initialization
{

    public struct ConstructStacksEvent : IEvent
    {
        public bool IsConsumed { get; set; }
    }

}