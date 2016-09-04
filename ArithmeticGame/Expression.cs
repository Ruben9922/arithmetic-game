using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArithmeticGame
{
    class Expression
    {
        private Operation operation; // Each question can only consist of a single operation for simplicity
        public Operation Operation
        {
            get
            {
                return operation;
            }
            set
            {
                operation = value;
            }
        }

        private int[] operands;
        public int[] Operands
        {
            get
            {
                return operands;
            }
            set
            {
                if (value.Length >= 1)
                {
                    operands = value;
                }
                else if (value == null)
                {
                    operands = new int[1];
                }
            }
        }

        public Expression(Operation operation, int[] operands)
        {
            Operation = operation;
            Operands = operands;
        }

        public Expression(Operation operation, int operandCount)
            : this(operation, new int[operandCount])
        {

        }

        public int Evaluate()
        {
            int answer = Operands[0];
            Func<int, int, int> calculation = GetCalculation();
            for (int i = 1; i < Operands.Length; i++)
            {
                answer = calculation(answer, Operands[1]);
            }
            return answer;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder(); // Using StringBuilder as operand count is not fixed
            for (int i = 0; i < Operands.Length; i++)
            {
                stringBuilder.Append(Operands[i]);
                if (i != Operands.Length - 1)
                {
                    stringBuilder.Append(" ");
                    stringBuilder.Append(GetOperationChar());
                    stringBuilder.Append(" ");
                }
            }
            return stringBuilder.ToString();
        }

        private Func<int, int, int> GetCalculation()
        {
            switch (operation)
            {
                case Operation.Add:
                    return (int x, int y) => x + y;
                case Operation.Subtract:
                    return (int x, int y) => x - y;
                case Operation.Multiply:
                    return (int x, int y) => x * y;
                case Operation.Divide:
                    return (int x, int y) => (int)Math.Round((double)x / y, MidpointRounding.AwayFromZero);
                default: // Should never be run
                    return (int x, int y) => 0;
            }
        }

        private char GetOperationChar()
        {
            switch (operation)
            {
                case Operation.Add:
                    return '+';
                case Operation.Subtract:
                    return '-';
                case Operation.Multiply:
                    return '*';
                case Operation.Divide:
                    return '/';
                default: // Should never be run
                    return '?';
            }
        }
    }
}
