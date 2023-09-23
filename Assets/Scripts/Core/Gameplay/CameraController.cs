using BonLib.Events;
using BonLib.Managers;
using Core.Events.Gameplay;
using UnityEngine;

namespace Core.Gameplay
{
    public class CameraController : Manager<CameraController>,
        IEventHandler<SetCameraFocusEvent>
    {
        [SerializeField]
        private Transform m_Target;
        
        [SerializeField]
        private float m_rotationSpeed = 5f;

        [SerializeField]
        private float m_zoomSpeed = 50f;
        
        private Vector3 m_offset;

        private const string AXIS_NAME = "Mouse X";

        public override void SubscribeToEvents()
        {
            base.SubscribeToEvents();
            
            EventManager.AddListener<SetCameraFocusEvent>(this);
        }

        public void Start()
        {
            SetOffset();
        }

        private void LateUpdate()
        {
            if (Input.GetMouseButton(0))
            {
                float horizontalInput = Input.GetAxis(AXIS_NAME);
                transform.RotateAround(m_Target.position, Vector3.up, horizontalInput * m_rotationSpeed);
                
                SetOffset();
            }

            float scroll = Input.mouseScrollDelta.y;
            if (Mathf.Abs(scroll) > 0.1f)
            {
                var zoomAmount = scroll * m_zoomSpeed * Time.deltaTime;
                transform.Translate(0, 0, zoomAmount, Space.Self);
            }
        }

        private void SetOffset()
        {
            m_offset = transform.position - m_Target.position;
        }

        public void OnEventReceived(ref SetCameraFocusEvent evt)
        {
            m_Target = evt.Target;

            transform.position = m_Target.position + m_offset;
        }

        public override void Dispose()
        {
            EventManager.RemoveListener<SetCameraFocusEvent>(this);
        }
    }
}