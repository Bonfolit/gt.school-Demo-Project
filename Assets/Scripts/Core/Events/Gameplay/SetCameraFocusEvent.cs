using BonLib.Events;
using UnityEngine;

namespace Core.Events.Gameplay
{

    public struct SetCameraFocusEvent : IEvent
    {
        public bool IsConsumed { get; set; }

        public Transform Target;

        public SetCameraFocusEvent(Transform target) : this()
        {
            Target = target;
        }
    }

}