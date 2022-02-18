# ASTCalculator
An implementation of a calculator using Abstract Syntax Tree

# Instructions:
1. Follow the displayed text guide and type the algebraic expression which is needed to be evaluated
2. Press Enter
   - If your expression includes some variables the program will ask you to type values for them
3. Inspect the result of the evaluation
4. The program will ask you "Do you want to repeat calculation?"
   - If you want to recalculate written expression, for example, with different variable values, press 'Y' key
     - If the written expression includes any variables program will display them with its' values
       - The program will ask you "Do you want to change variable value?"
         - If you want to change a value press 'Y'
         - Type desired numeric value of the variable
       - In other case, press 'N'
   - Otherwise press 'N'. The program will be terminated

# Features:
- [X] Supports expressions like: __10x-12x+y(50+3x)__
  - That means that the program will interpret the combination of characters __10x__ as a multiplying expression
- [X] Support binary _Power ('^')_ operator
  - That means that the program will successfully iterpret the combination of characters __x^2__ as a power operation

# Limitations:
- [ ] Doesn't support _unary Minus ('-')_ operator
  - That means that program decline any expressions which begins with '-'
   - To bypass this limitation, use a variable at the beggining of the expression
- [ ] Doesn't support decimal numbers as input
- [ ] Doesn't support retyping expression *without relaunching* the program

# Syntax:
- **12312** -- *number*
- **var1234_** -- *variable*; later you will be asked to enter the value of the variable
- **( *%expression* )** -- bracketed expression
# Operators:
- 1 **+** 1 -- *plus*
- 1 **-** 1 -- *minus*
- 1 __*__ 1 -- *multiplier*
   - 2x or x(2+1) or 1(1+1) will work too 
- 1 **/** 1 or 1 **\** 1 -- *division*
- 1 **^** 1 -- *power*

# Feedback
- I will be glad any feedback concerning this implementation!

# Resources
- Thanks a lot for the explanation of Abstract Syntax Tree the person who mantains this site: https://ruslanspivak.com/lsbasi-part1/!
