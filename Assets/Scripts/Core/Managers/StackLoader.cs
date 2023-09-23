using System.Collections;
using System.Collections.Generic;
using BonLib.Events;
using BonLib.Managers;
using Core.Data;
using Core.Events.Initialization;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

namespace Core.Managers
{

    public class StackLoader : Manager<StackLoader>, 
        IEventHandler<LoadStackDataEvent>
    {
        [FormerlySerializedAs("m_stackContainer")] [SerializeField]
        private BlockContainer m_blockContainer;
        public BlockContainer Container => m_blockContainer ??= Resources.Load<BlockContainer>("BlockContainer");
        
        private const string API_URL = "https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack";

        public override void SubscribeToEvents()
        {
            base.SubscribeToEvents();
            
            EventManager.AddListener<LoadStackDataEvent>(this, Priority.Critical);
        }

        public void OnEventReceived(ref LoadStackDataEvent evt)
        {
            StartCoroutine(GetDataFromApi());
        }
        
        private IEnumerator GetDataFromApi()
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(API_URL))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    var stackJson = webRequest.downloadHandler.text;
                    
                    var stackData = JsonConvert.DeserializeObject<List<BlockData>>(stackJson);
                    
                    Container.SetData(stackData);

                    var evt = new StackDataLoadedEvent();
                    EventManager.SendEvent(ref evt);
                }
                else
                {
                    Debug.LogError($"Error: {webRequest.error}");
                }
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            
            EventManager.RemoveListener<LoadStackDataEvent>(this);
        }
    }

}