using System;
using Networking.DataConvert;
using Utils;

namespace UndefinedNetworking.Converters
{
    public sealed class ColorConverter : IStaticDataConverter
    {
        public Type Type => typeof(Color);
        public ushort Length => 4;
        public byte[] Serialize(object o)
        {
            var color = (Color)o;
            var buffer = new[]
            {
                color.R,
                color.G,
                color.B,
                color.A,
            };
            return buffer; 
        }
        public object Deserialize(byte[] data, Type currenType) =>
            new Color(data[0], data[1], data[2], data[3]);
    }
}