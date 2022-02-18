namespace Calculator.AST
{
    class Operand
    {
        string _value;
        Token _token;

        public string Value { get { return _value; } set { _value = value; } }

        public Token Token { get { return _token; } }

        public Operand(Token token)
        {
            _token = token; _value = _token.Value;
        }
    }
}
