namespace Calculator.AST
{
    class BinaryOperator : Operand
    {
        Operand _right;
        Operand _left;
        public Operand Left { get { return _left; } set { _left = value; } }
        public Operand Right { get { return _right; } set { _right = value; } }

        public BinaryOperator(Operand leftOperand, Operand rightOperand, Token token) : base(token)
        {
            _left = leftOperand;
            _right = rightOperand;
        }
        public BinaryOperator(Token token) : base(token) { }
    }
}
