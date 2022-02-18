namespace Calculator.AST.Solver
{
    internal class VariableInfo
    {
        long _value;
        bool _isImmutable;

        public long Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (!_isImmutable)
                {
                    _isImmutable = true;
                    _value = value;
                }
            }
        }
        public bool IsImmutable
        {
            get
            {
                return _isImmutable;
            }
        }

        public VariableInfo(long value = 0, bool isImmutable = false)
        {
            _value = value;
            _isImmutable = isImmutable;
        }

        public void ResetImmutability()
        {
            _isImmutable = false;
        }
    }
}
