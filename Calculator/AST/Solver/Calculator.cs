using System;
using System.Collections.Generic;

namespace Calculator.AST.Solver
{
    class TreeSolver
    {
        readonly Operand _node;
        readonly Dictionary<string, VariableInfo> _varsCache;

        bool _isFirstCalculating;

        public TreeSolver(Operand node = null)
        {
            _node = node;
            _varsCache = new Dictionary<string, VariableInfo>();
            _isFirstCalculating = true;
        }

        public long InitCalcualtion()
        {
            foreach (var key in _varsCache.Keys)
                _varsCache[key].ResetImmutability();

            long result = Resolve(_node);
            _isFirstCalculating = false;

            return result;
        }

        public override string ToString()
        {
            return ToString(_node);
        }

        #region Resolvers 
        // functions in the region calculate numeric value of the expression

        long ResolveVar(Operand node)
        {
            string varName = node.Token.Value;
            long num = 0;

            bool isVarCached = _varsCache.ContainsKey(varName);
            bool isVarImmutable = false;
            bool isVarShouldBeChanged = true;

            if (isVarCached)
            {
                num = _varsCache[varName].Value;
                isVarImmutable = _varsCache[varName].IsImmutable;
            }

            if (!_isFirstCalculating && !isVarImmutable)
            {
                Console.Write(string.Format("{0} = {1}\n", varName, num));
                Console.WriteLine("Do you wish to change variable value? (Y/N): ");

                var pressedKey = Console.ReadKey();
                while (pressedKey.Key != ConsoleKey.Y && pressedKey.Key != ConsoleKey.N) { 
                    Console.WriteLine("\nPlease, press either Y or N key.");

                    pressedKey = Console.ReadKey(); 
                }
                isVarShouldBeChanged = pressedKey.Key == ConsoleKey.Y;
            }
            if (!isVarCached || (isVarShouldBeChanged && !isVarImmutable))
            {
                if (!isVarCached)
                    _varsCache[varName] = new VariableInfo();

                Console.WriteLine("\nType the value of variable");
                Console.Write(string.Format("\n{0} = ", varName));

                var varValue = Console.ReadLine();
                while (!long.TryParse(varValue, out num))
                {
                    Console.WriteLine("Incorrect input numeric value! Please fix the issue and try again.");

                    varValue = Console.ReadLine();
                }
            }
            // setter won't change value if it's immmutable
            _varsCache[varName].Value = num;

            return num;
        }

        long ResolveNum(Operand node)
        {
            string varValue = node.Value;

            long num = Convert.ToInt64(varValue);

            return num;
        }

        long ResolveBinaryOperator(BinaryOperator node)
        {
            var left = Resolve(node.Left);
            var right = Resolve(node.Right);

            long result = 0;

            if (node.Token.Type == Token.TOKEN_TYPE.PLUS)
                result = left + right;
            else if (node.Token.Type == Token.TOKEN_TYPE.MINUS)
                result = left - right;
            else if (node.Token.Type == Token.TOKEN_TYPE.MUL)
                result = left * right;
            else if (node.Token.Type == Token.TOKEN_TYPE.DIV)
                result = left / right;
            else if (node.Token.Type == Token.TOKEN_TYPE.POW)
                result = (long)Math.Pow(left, right);

            return result;
        }

        long Resolve(Operand node)
        {
            long result = 0;

            if (node.Token.IsOperator())
                result += ResolveBinaryOperator((BinaryOperator)node);
            else if (node.Token.Type == Token.TOKEN_TYPE.VAR)
                result += ResolveVar(node);
            else if (node.Token.Type == Token.TOKEN_TYPE.NUM)
                result += ResolveNum(node);
            else result += Resolve(node);

            return result;
        }

        #endregion

        #region ToString Utilities

        string EmbraceString(string str)

        {
            return "(" + str + ")";
        }

        string ToString(BinaryOperator node)
        {
            var operatorStr = node.Token.ToString();

            var isLeftBinary = node.Left is BinaryOperator;
            var isRightBinary = node.Right is BinaryOperator;

            var leftRes = ToString(node.Left);
            var rightRes = ToString(node.Right);

            if (isLeftBinary)
                leftRes = EmbraceString(leftRes);

            if (isRightBinary)
                rightRes = EmbraceString(rightRes);

            return leftRes + operatorStr + rightRes;
        }

        string ToString(Operand node)
        {
            if (node.Token.IsOperator())
                return ToString((BinaryOperator)node);
            else if (node.Token.Type == Token.TOKEN_TYPE.VAR)
                return node.Token.Value;
            else if (node.Token.Type == Token.TOKEN_TYPE.NUM)
                return node.Value;
            return "";
        }

        #endregion
    }
}
