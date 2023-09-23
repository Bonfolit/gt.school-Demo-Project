using BonLib.Events;
using Core.Data;

namespace Core.Events.Gameplay
{

    public struct GetDataFromBlockEvent : IEvent
    {
        public bool IsConsumed { get; set; }

        public BlockData BlockData;

        public GetDataFromBlockEvent(BlockData blockData) : this()
        {
            BlockData = blockData;
        }
    }

}