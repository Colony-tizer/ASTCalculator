namespace Calculator.AST
{
    partial class Token
    {
        public enum TOKEN_TYPE
        {
            EMPTY = -1,
            VAR = 0,
            NUM,
            PLUS, MINUS,
            DIV, MUL,
            POW,
            BRC_L, BRC_R,
            VOID
        };

    }
}
