using System;

namespace Calculator.AST
{
    class Parser
    {
        readonly Lexer _lexer;
        Token _curToken;

        public string Text
        {
            get
            {
                return _lexer.Text;
            }
            set
            {
                _lexer.Text = value;
            }
        }

        public Parser(string text)
        {
            _lexer = new Lexer(text);
            _curToken = _lexer.GetNextToken();
        }
        void Advance(Token.TOKEN_TYPE tokenType)
        {
            if (_curToken.Type == tokenType)
                _curToken = _lexer.GetNextToken();
            else throw new Exception("Wrong syntax!");
        }
        Operand ParseBrackets()
        {
            Operand node;
            if (_curToken.Type == Token.TOKEN_TYPE.VAR || _curToken.Type == Token.TOKEN_TYPE.NUM)
            {
                node = new Operand(_curToken);
                Advance(_curToken.Type);
            } else if (_curToken.Type == Token.TOKEN_TYPE.BRC_L)
            {
                Advance(Token.TOKEN_TYPE.BRC_L);
                node = ParsePlusMin();
                Advance(Token.TOKEN_TYPE.BRC_R);
            } else
            {
                throw new Exception("Wrong syntax!");
            }

            return node;
        }
        Operand ParsePow()
        {
            Operand node = ParseBrackets();

            while (_curToken.Type == Token.TOKEN_TYPE.POW)
            {
                var token = _curToken;
                Advance(Token.TOKEN_TYPE.POW);

                node = new BinaryOperator(node, ParseBrackets(), token);
            }

            return node;
        }
        Operand ParseMulDiv()
        {
            Operand node = ParsePow();

            while (_curToken.Type == Token.TOKEN_TYPE.MUL || _curToken.Type == Token.TOKEN_TYPE.DIV)
            {
                var token = _curToken;
                if (_curToken.Type == Token.TOKEN_TYPE.MUL)
                    Advance(Token.TOKEN_TYPE.MUL);
                else if (_curToken.Type == Token.TOKEN_TYPE.DIV)
                    Advance(Token.TOKEN_TYPE.DIV);

                node = new BinaryOperator(node, ParsePow(), token);
            }

            return node;
        }
        Operand ParsePlusMin()
        {
            var node = ParseMulDiv();

            while (_curToken.Type == Token.TOKEN_TYPE.PLUS || _curToken.Type == Token.TOKEN_TYPE.MINUS)
            {
                var token = _curToken;
                if (_curToken.Type == Token.TOKEN_TYPE.PLUS)
                    Advance(Token.TOKEN_TYPE.PLUS);
                else if (_curToken.Type == Token.TOKEN_TYPE.MINUS)
                    Advance(Token.TOKEN_TYPE.MINUS);

                node = new BinaryOperator(node, ParseMulDiv(), token);
            }

            return node;
        }

        public Operand Parse()
        {
            return ParsePlusMin();
        }
    }
}
