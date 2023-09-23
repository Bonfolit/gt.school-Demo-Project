using BonLib.Events;

namespace Core.Events.SceneManagement
{

    public struct SceneLoadedEvent : IEvent
    {
        public bool IsConsumed { get; set; }

        public int SceneIndex;

        public SceneLoadedEvent(int sceneIndex) : this()
        {
            SceneIndex = sceneIndex;
        }
    }

}