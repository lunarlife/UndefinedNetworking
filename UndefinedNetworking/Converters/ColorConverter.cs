using System;
using Networking.DataConvert.Datas;
using Utils;

namespace UndefinedNetworking.Converters
{
    public sealed class ColorConverter : Converter
    {
        public override Type Type => typeof(Color);
        public override int Length => 4;
        public override byte[]? Serialize(object o)
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
        public override object? Deserialize(byte[] data, Type currenType) =>
            new Color(data[0], data[1], data[2], data[3]);
    }
}