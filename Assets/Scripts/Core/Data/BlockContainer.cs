using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Data
{
    [CreateAssetMenu(fileName = "BlockContainer", menuName = "BlockContainer", order = 0)]

    public class BlockContainer : ScriptableObject
    {
        [SerializeField]
        private List<BlockData> m_blockData;

        public List<BlockData> Data => m_blockData;

        public void SetData(List<BlockData> data)
        {
            m_blockData = data;
        }
    }

}