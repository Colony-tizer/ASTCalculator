using System;

namespace Calculator.AST
{
    /* 
     * Tokenizes the expression
     * Spits out Token by invoking GetNextToken() function
    */
    class Lexer
    {
        string _text;
        int _pos;
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = FixString(value); 
                _pos = 0;
            }
        }

        public Lexer (string text)
        {
            _text = FixString(text); 
            _pos = 0;
        }

        public Token GetNextToken()
        {
            Token token;

            SkipSpaces();

            int startIdx = _pos;
            if (char.IsDigit(_text[startIdx]))
            {
                token = ProcessNum(startIdx);
            }
            else
            {
                token = ProcessOperand(startIdx);
            }

            return token;
        }

        Token ProcessNum(int startIdx)
        {
            while (char.IsLetterOrDigit(_text[_pos]))
            {
                if (char.IsLetter(_text[_pos]))
                    throw new Exception("Invalid variable indentifier!");

                Advance();
            }
            var token = new Token(Token.TOKEN_TYPE.NUM, _text[startIdx.._pos]);
            
            return token;
        }

        Token ProcessOperand(int startIdx)
        {
            char prevChar = _text[_pos];
            while (!IsCharLikeSpace(_text[_pos]) && (char.IsLetterOrDigit(prevChar) && IsNextCharLetterOrDigit() || IsCharOperator(_text[_pos])))
            {
                prevChar = _text[_pos];
                if (IsCharOperator(_text[_pos]))
                    break;
                Advance();
            }
            var token = new Token(_text.Substring(startIdx, Math.Max(1, _pos - startIdx + 1)));

            Advance();

            return token;
        }

        void Advance()
        {
            if (_pos < _text.Length - 1) 
                ++_pos;
        }

        string CleanString(string text)
        {
            string cleanedStr = "";
            foreach (char ch in text)
            {
                if (!IsCharLikeSpace(ch)) cleanedStr += ch;
            }
            return cleanedStr;
        }
        string FixString(string text)
        {
            const int START_IDX = 0;

            var copyText = CleanString(text);

            char prevChar = copyText[START_IDX];
            char curChar = copyText[START_IDX];

            for (int i = 0; i < copyText.Length; ++i)
            {
                curChar = copyText[i];

                if (i > 0 && !IsCharOperator(prevChar) && 
                    ((char.IsLetterOrDigit(prevChar) && curChar == '(') || 
                     (char.IsDigit(prevChar) && char.IsLetter(curChar))))
                {
                    copyText = copyText[0..i] + "*" + copyText[i..copyText.Length];
                    ++i;
                }
                prevChar = curChar;
            }
            return copyText;
        }
       
        void SkipSpaces()
        {
            while (IsCharLikeSpace(_text[_pos])) Advance();
        }

        bool IsCharLikeSpace(char ch)
        {
            return (char.IsWhiteSpace(ch) || ch == '\t' || ch == '\n');
        }

        bool IsCharOperator(char ch)
        {
            return (ch == '+' || ch == '-' ||
                ch == '*' || ch == '\\' || ch == '/' ||
                ch == '^');
        }

        bool IsNextCharLetterOrDigit()
        {
            var isInBounds = _pos + 1 < _text.Length;
            if (!isInBounds)
                return isInBounds;

            var isNextBrace = _text[_pos + 1] == '(' || _text[_pos + 1] == ')';

            return !isNextBrace && !IsCharOperator(_text[_pos + 1]);
        }

        bool IsBraces(char ch)
        {
            return ch == '(' || ch == ')';
        }
    }
}
