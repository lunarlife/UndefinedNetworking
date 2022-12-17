using System;
using Networking.DataConvert;
using Utils;
using Utils.Dots;

namespace UndefinedNetworking.Converters
{
    public sealed class RectConverter : IStaticDataConverter
    {
        public ushort Length => 16;
        public bool IsValidConvertor(Type type) => typeof(Rect) == type;

        public byte[] Serialize(object o)
        {
            var rect = (Rect)o;
            return DataConverter.Combine(DataConverter.Serialize(rect.Position), DataConverter.Serialize(rect.Width),
                DataConverter.Serialize(rect.Height));
        }

        public object Deserialize(byte[] data, Type currenType)
        {
            ushort index = 0;
            return new Rect(DataConverter.Deserialize<Dot2Int>(data, ref index), DataConverter.Deserialize<int>(data, ref index), DataConverter.Deserialize<int>(data, ref index));
        }
    }
}