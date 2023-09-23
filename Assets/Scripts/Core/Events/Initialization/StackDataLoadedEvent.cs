using BonLib.Events;

namespace Core.Events.Initialization
{

    public struct StackDataLoadedEvent : IEvent
    {
        public bool IsConsumed { get; set; }
    }

}