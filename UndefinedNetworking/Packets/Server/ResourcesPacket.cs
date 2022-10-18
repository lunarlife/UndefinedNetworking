using Networking.DataConvert;

namespace UndefinedNetworking.Packets.Server
{
    [DataObject]
    public sealed class ResourcesPacket
    {
        [DataProperty]
        public int FileLength { get; private set; }
        [DataProperty]
        public byte[] Bytes { get; private set; }

        public ResourcesPacket(int fileLength, byte[] bytes)
        {
            FileLength = fileLength;
            Bytes = bytes;
        }

        private ResourcesPacket()
        {
            
        }
    }
}