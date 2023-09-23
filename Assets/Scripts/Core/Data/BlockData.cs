using System.Runtime.InteropServices;

namespace Core.Data
{
    [System.Serializable]
    [StructLayout(LayoutKind.Sequential, Pack=0)]
    public struct BlockData
    {
        public int id;
        public string subject;
        public string grade;
        public int mastery;
        public string domainid;
        public string domain;
        public string cluster;
        public string standardid;
        public string standarddescription;
    }
}