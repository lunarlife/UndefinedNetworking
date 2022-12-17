using System;
using Networking.DataConvert;
using Utils;

namespace UndefinedNetworking.Converters
{
    public sealed class ColorConverter : IStaticDataConverter
    {
        public ushort Length => 4;
        public bool IsValidConvertor(Type type) => typeof(Color) == type;

        public byte[] Serialize(object o)
        {
            var color = (Color)o;
            return new[]
            {
                color.R,
                color.G,
                color.B,
                color.A
            };; 
        }
        public object Deserialize(byte[] data, Type currenType) =>
            new Color(data[0], data[1], data[2], data[3]);
    }
}