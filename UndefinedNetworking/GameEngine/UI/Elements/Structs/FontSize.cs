using UndefinedNetworking.Exceptions;
using Utils;

namespace UndefinedNetworking.GameEngine.UI.Elements.Structs
{
    public struct FontSize
    {
        private float _minSize = 24;
        private float _maxSize;
        private float _characterWidthAdjustment;
        private float _characterSpacing;
        private float _wordSpacing;
        private float _lineSpacing;

        public float MinSize
        {
            readonly get => _minSize;
            set
            {
                if (!MathUtils.Clamp(value, 0, _maxSize))
                    throw new TextException($"{nameof(MinSize)} value: ({value}) size must be between zero and {nameof(MaxSize)}({_maxSize})");
                _minSize = value;
            }
        }

        public float MaxSize
        {
            readonly get => _maxSize;
            set
            {
                if (_minSize > value)
                    throw new TextException($"{nameof(MaxSize)} value: ({value}) size must be more than {nameof(MinSize)}({_minSize}))");
                _maxSize = value;
            }
        }

        public float CharacterWidthAdjustment
        {
            readonly get => _characterWidthAdjustment;
            set
            {
                if (!MathUtils.Clamp(value, 0, 50))
                    throw new TextException($"{nameof(CharacterWidthAdjustment)} value: ({value}) must be between zero and 50");
                _characterWidthAdjustment = value;
            }
        }

        public float LineSpacing
        {
            readonly get => _lineSpacing;
            set
            {
                if (!MathUtils.Clamp(value, -500, 500))
                    throw new TextException($"{nameof(LineSpacing)} value: ({value}) must be between -500 and 500");
                _lineSpacing = value;
            }
        }

        public float CharacterSpacing
        {
            readonly get => _characterSpacing;
            set
            {
                if (!MathUtils.Clamp(value, -500, 500))
                    throw new TextException($"{nameof(CharacterSpacing)} value: ({value}) must be between -500 and 500");
                _characterSpacing = value;
            }
        }

        public float WordSpacing
        {
            readonly get => _wordSpacing;
            set
            {
                _wordSpacing = value;
                if (!MathUtils.Clamp(value, -1000, 1000))
                    throw new TextException($"{nameof(WordSpacing)} value: ({value}) must be -1000 and 1000");
            }
        }
        public bool AutoSize { get; set; }

        public FontSize(float size)
        {
            _maxSize = size;
            _characterSpacing = 0;
            _minSize = 0;
            _wordSpacing = 0;
            _characterWidthAdjustment = 0;
            _lineSpacing = 0;
            AutoSize = false;
        }

        public FontSize() : this(24)
        {
            
        }
    }
}