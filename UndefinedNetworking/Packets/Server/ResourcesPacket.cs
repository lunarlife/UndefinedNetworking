namespace UndefinedNetworking.Packets.Server
{
    public sealed class ResourcesPacket
    {
        public int FileLength { get; private set; }
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