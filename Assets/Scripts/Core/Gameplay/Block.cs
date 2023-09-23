using BonLib.Events;
using Core.Data;
using Core.Events.Gameplay;
using UnityEngine;

namespace Core.Gameplay
{

    public class Block : MonoBehaviour
    {
        private Stack m_stack;
        private EventManager m_eventManager;
        
        [SerializeField]
        private Rigidbody m_rigidbody;

        [SerializeField]
        private MeshRenderer m_renderer;

        [SerializeField]
        private BlockData m_data;

        public void Initialize(Stack stack, EventManager eventManager)
        {
            m_stack = stack;
            m_eventManager = eventManager;
        }

        public void SetData(ref BlockData data)
        {
            m_data = data;
        }

        public void SetRigidbodyActive(bool active)
        {
            m_rigidbody.useGravity = active;
        }

        private void OnMouseDown()
        {
            var evt = new UserClickedOnStackEvent(m_stack.GetInstanceID());
            m_eventManager.SendEvent(ref evt);
        }

        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(1))
            {
                var evt = new GetDataFromBlockEvent(m_data);
                m_eventManager.SendEvent(ref evt);
            }
        }

        public void SetMaterial(Material mat)
        {
            m_renderer.material = mat;
        }
    }

}