using System;
using System.Collections.Generic;
using System.Linq;
using BonLib.DependencyInjection;
using BonLib.Events;
using BonLib.Pooling;
using Core.Config;
using Core.Data;
using Core.Events.Gameplay;
using Core.Events.Initialization;
using TMPro;
using UnityEngine;

namespace Core.Gameplay
{
    public class Stack : MonoBehaviour,
        IEventHandler<ConstructStacksEvent>,
        IEventHandler<UserClickedOnStackEvent>,
        IEventHandler<TestStackEvent>
    {
        private EventManager m_eventManager;
        
        private MaterialConfig m_materialConfig;

        public MaterialConfig MaterialConfig =>
            m_materialConfig ??= Resources.Load<MaterialConfig>("Config/MaterialConfig");
        
        [SerializeField]
        private string m_grade;

        [SerializeField]
        private TextMeshProUGUI m_text;

        [SerializeField]
        private float m_horizontalOffset;
        [SerializeField]
        private float m_verticalOffset;

        [SerializeField]
        private PoolObject m_blockPoolObject;

        public string Grade => m_grade;
        
        [SerializeField]
        private List<BlockData> m_stackData;

        private List<PoolObject> m_blocks;

        private List<PoolObject> m_glassBlocks;

        private void Start()
        {
            m_text.SetText(m_grade);
        }

        public void Initialize()
        {
            m_eventManager = DI.Resolve<EventManager>();
            
            m_eventManager.AddListener<ConstructStacksEvent>(this);
            m_eventManager.AddListener<UserClickedOnStackEvent>(this);
            m_eventManager.AddListener<TestStackEvent>(this);
        }

        public void Dispose()
        {
            m_eventManager.RemoveListener<ConstructStacksEvent>(this);
            m_eventManager.RemoveListener<UserClickedOnStackEvent>(this);
            m_eventManager.RemoveListener<TestStackEvent>(this);
            
            for (var i = 0; i < m_blocks.Count; i++)
            {
                if (m_blocks[i] == null || m_blocks[i].gameObject.activeSelf == false)
                    continue;

                PrefabPool.Return(m_blocks[i]);
            }
        }

        public void SetData(List<BlockData> stackData)
        {
            m_stackData = stackData.OrderBy(x => x.domain)
                .ThenBy(x => x.cluster)
                .ThenBy(x => x.standardid)
                .ToList();
        }

        private void Construct()
        {
            m_blocks = new List<PoolObject>();
            m_glassBlocks = new List<PoolObject>();

            for (var i = 0; i < m_stackData.Count; i++)
            {
                var data = m_stackData[i];
                
                var blockPoolObject = PrefabPool.Rent(m_blockPoolObject);
                var block = (Block)blockPoolObject.CustomReference;
                var blockTransform = blockPoolObject.transform;
                
                block.SetData(ref data);
                block.Initialize(this, m_eventManager);

                var mat = MaterialConfig.GetMaterial(data.mastery);
                block.SetMaterial(mat);
                
                m_blocks.Add(blockPoolObject);

                if (data.mastery == 0)
                {
                    m_glassBlocks.Add(blockPoolObject);
                }
                
                var row = i % 3;
                var col = i / 3;
                var turn = col % 2 == 0;

                var horizontal = (row - 1) * m_horizontalOffset;
                var vertical = col * m_verticalOffset;

                var localPos = new Vector3(turn ? 0f : horizontal, vertical, turn ? horizontal : 0f);
                
                var pos = transform.TransformPoint(localPos);
                var rot = Quaternion.Euler(0f, turn ? 90f : 0f, 0f);

                blockTransform.SetPositionAndRotation(pos, rot);
            }
        }

        public void OnEventReceived(ref ConstructStacksEvent evt)
        {
            Construct();
        }

        public void OnEventReceived(ref UserClickedOnStackEvent evt)
        {
            if (evt.StackId != GetInstanceID())
                return;

            var focusEvt = new SetCameraFocusEvent(transform);
            m_eventManager.SendEvent(ref focusEvt);
        }

        public void OnEventReceived(ref TestStackEvent evt)
        {
            RemoveGlassBlocks();
            ActivatePhysics();
        }

        private void RemoveGlassBlocks()
        {
            foreach (var poolObject in m_glassBlocks)
            {
                m_blocks.Remove(poolObject);
                PrefabPool.Return(poolObject);
            }
        }

        private void ActivatePhysics()
        {
            foreach (var poolObject in m_blocks)
            {
                ((Block)poolObject.CustomReference).SetRigidbodyActive(true);
            }
        }
    }

}