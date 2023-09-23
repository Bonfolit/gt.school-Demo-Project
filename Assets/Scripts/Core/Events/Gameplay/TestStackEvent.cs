using BonLib.Events;

namespace Core.Events.Gameplay
{

    public struct TestStackEvent : IEvent
    {
        public bool IsConsumed { get; set; }
    }

}