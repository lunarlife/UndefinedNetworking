using UndefinedServer.Commands;

namespace UndefinedServer
{
    public static class ServerExtentions
    {

        public static string[] ToStringArray(this CommandParameter[] arr)
        {
            var a = new string[arr.Length];
            for (var i = 0; i < arr.Length; i++)
            {
                a[i] = arr[i];
            }

            return a;
        }
    }
}