namespace Calculator.AST.Solver
{
    internal class VariableInfo
    {
        double _value;
        bool _isImmutable;

        public double Value
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

        public VariableInfo(double value = 0, bool isImmutable = false)
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
