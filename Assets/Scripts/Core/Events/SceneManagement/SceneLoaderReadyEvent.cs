using BonLib.Events;

namespace Core.Events.SceneManagement
{

    public struct SceneLoaderReadyEvent : IEvent
    {
        public bool IsConsumed { get; set; }
    }

}