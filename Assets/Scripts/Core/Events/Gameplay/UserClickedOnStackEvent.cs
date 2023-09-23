using BonLib.Events;

namespace Core.Events.Gameplay
{

    public struct UserClickedOnStackEvent : IEvent
    {
        public bool IsConsumed { get; set; }

        public int StackId;

        public UserClickedOnStackEvent(int stackId) : this()
        {
            StackId = stackId;
        }
    }

}