using System.Text;
using BonLib.Events;
using BonLib.Managers;
using Core.Events.Gameplay;
using TMPro;
using UnityEngine;

namespace Core.Managers
{

    public class UIManager : Manager<UIManager>,
        IEventHandler<GetDataFromBlockEvent>
    {
        [SerializeField]
        private TextMeshProUGUI m_infoText;

        public override void SubscribeToEvents()
        {
            base.SubscribeToEvents();
            
            EventManager.AddListener<GetDataFromBlockEvent>(this);
        }

        public override void Dispose()
        {
            base.Dispose();
            
            EventManager.RemoveListener<GetDataFromBlockEvent>(this);
        }

        public void RemoveGlassBlocks()
        {
            var evt = new TestStackEvent();
            EventManager.SendEvent(ref evt);
        }

        public void OnEventReceived(ref GetDataFromBlockEvent evt)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(evt.BlockData.grade);
            sb.Append(": ");
            sb.Append(evt.BlockData.domain);
            sb.Append("<br><br>");
            sb.Append(evt.BlockData.cluster);
            sb.Append("<br><br>");
            sb.Append(evt.BlockData.standardid);
            sb.Append(": ");
            sb.Append(evt.BlockData.standarddescription);
            
            m_infoText.SetText(sb);

            sb.Clear();
        }
    }

}