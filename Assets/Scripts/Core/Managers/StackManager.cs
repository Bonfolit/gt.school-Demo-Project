using System.Collections.Generic;
using System.Linq;
using BonLib.Events;
using BonLib.Managers;
using Core.Data;
using Core.Events.Initialization;
using Core.Gameplay;
using UnityEngine;

namespace Core.Managers
{

    public class StackManager : Manager<StackManager>,
        IEventHandler<StackDataLoadedEvent>
    {
        [SerializeField]
        private BlockContainer m_blockContainer;
        public BlockContainer Container => m_blockContainer ??= Resources.Load<BlockContainer>("BlockContainer");

        [SerializeField]
        private List<Stack> m_stacks;
        
        public override void SubscribeToEvents()
        {
            base.SubscribeToEvents();
            
            EventManager.AddListener<StackDataLoadedEvent>(this);
        }

        public override void Initialize()
        {
            base.Initialize();
            
            foreach (var stack in m_stacks)
            {
                stack.Initialize();
            }

            var evt = new LoadStackDataEvent();
            EventManager.SendEvent(ref evt);
        }

        public void OnEventReceived(ref StackDataLoadedEvent evt)
        {
            foreach (var stack in m_stacks)
            {
                var data = Container.Data.Where(x => x.grade == stack.Grade).ToList();
                stack.SetData(data);
            }

            var constructEvt = new ConstructStacksEvent();
            EventManager.SendEvent(ref constructEvt);
        }

        public override void Dispose()
        {
            base.Dispose();
            
            EventManager.RemoveListener<StackDataLoadedEvent>(this);
            
            foreach (var stack in m_stacks)
            {
                stack.Dispose();
            }
        }
    }

}