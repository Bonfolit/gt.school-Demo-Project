using BonLib.Events;

namespace Core.Events.Initialization
{

    public struct LoadStackDataEvent : IEvent
    {
        public bool IsConsumed { get; set; }
    }

}