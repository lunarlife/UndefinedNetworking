using Networking.Packets;

namespace UndefinedNetworking.Packets.Server.Resources;

public class ResourceDownloadStartPacket : RequestPacket<ResourceDownloadCompletePacket>
{
    public string FilePathInResourcesFolder { get; private set; }
    public long TotalLength { get; private set; }
    public int Id { get; private set; }
    
    public ResourceDownloadStartPacket(string filePathInResourcesFolder, int id, long totalLength)
    {
        FilePathInResourcesFolder = filePathInResourcesFolder;
        Id = id;
        TotalLength = totalLength;
    }
}