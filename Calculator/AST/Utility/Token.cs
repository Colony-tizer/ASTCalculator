using System;

namespace Calculator.AST
{
    partial class Token
    {

        TOKEN_TYPE _type;
        string _value;

        public TOKEN_TYPE Type { get { return _type; } set { _type = value; } }
        public string Value { get { return _value; } }

        public Token(TOKEN_TYPE type, string value)
        {
            _type = type; _value = value;
        }

        public Token()
        {
            _type = TOKEN_TYPE.EMPTY; _value = "";
        }

        public Token(string str)
        {
            FormTokenFromString(str);
        }

        public bool IsOperator()
        {
            return (_type > TOKEN_TYPE.NUM && _type < TOKEN_TYPE.BRC_L);
        }

        void FormTokenFromString(string str)
        {
            _value = str.Replace("\0", "");

            if (_value.Equals("+"))
                _type = TOKEN_TYPE.PLUS;
            else if (_value.Equals("-"))
                _type = TOKEN_TYPE.MINUS;
            else if (_value.Equals( "*"))
                _type = TOKEN_TYPE.MUL;
            else if (_value.Equals("\\") || _value.Equals("/"))
                _type = TOKEN_TYPE.DIV;
            else if (_value.Equals("^"))
                _type = TOKEN_TYPE.POW;
            else if (_value.Equals("("))
                _type = TOKEN_TYPE.BRC_L;
            else if (_value.Equals(")"))
                _type = TOKEN_TYPE.BRC_R;
            else if ((_value.Length > 0) && Int32.TryParse(str, out _))
                _type = TOKEN_TYPE.NUM;
            else if (_value.Length > 0 && char.IsLetter(_value[0]))
                _type = TOKEN_TYPE.VAR;
            else _type = TOKEN_TYPE.VOID;
        }

        public override string ToString()
        {
            string str = ""; 
            if (_type == Token.TOKEN_TYPE.PLUS)
                str = "+";
            else if (_type == Token.TOKEN_TYPE.MINUS)
                str = "-";
            else if (_type == Token.TOKEN_TYPE.MUL)
                str = "*";
            else if (_type == Token.TOKEN_TYPE.DIV)
                str = "/";
            else if (_type == Token.TOKEN_TYPE.POW)
                str = "^";
            
            return str;
        }
    }
}
