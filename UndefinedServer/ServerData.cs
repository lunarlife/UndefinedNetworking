using System.IO;
using System.IO.Compression;
using UndefinedServer.Types;
using Utils.Enums;

namespace UndefinedServer
{
     public static class ServerData
     {
         public static Enum<ServerHumanType> HumanTypes { get; } = new();
         public static bool IsLoaded { get; private set; }
 
         public static void LoadData()
         {
             if (!Directory.Exists(Paths.TempData))
                 Directory.CreateDirectory(Paths.TempData).Attributes =
                     FileAttributes.Directory | FileAttributes.Hidden;
             if (!Directory.Exists(Paths.ResourcesFolder)) Directory.CreateDirectory(Paths.ResourcesFolder);
             var zip = Path.Combine(Paths.TempData, "resources.zip");
             if(File.Exists(zip)) File.Delete(zip); 
             ZipFile.CreateFromDirectory(Paths.ResourcesFolder, zip, CompressionLevel.NoCompression, false);
             IsLoaded = true;
         }
     }
 }