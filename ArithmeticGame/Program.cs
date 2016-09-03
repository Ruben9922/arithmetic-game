using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ruben9922.Utilities.ConsoleUtilities;

namespace ArithmeticGame
{
    class Program
    {
        static void Main(string[] args)
        {
            const int MaxQuestionCount = 100;
            const string Separator = "----------";

            Random random = new Random();
            int correctAnswerCount;
            int score;

            do
            {
                correctAnswerCount = 0;
                score = 0;

                Console.Clear();
                Console.WriteLine("Welcome to Ruben9922's Arithmetic Game!");
                Console.WriteLine();

                int questionCount = ConsoleReadUtilities.ReadInt("Number of questions: ", 1, MaxQuestionCount + 1, string.Format("Integers between 1 and {0} inclusive only!", MaxQuestionCount));
                Console.WriteLine();

                // Input min and max operands, ensuring max is greater than or equal to min
                // Both min and max operands are inclusive
                int minOperand;
                int maxOperand;
                bool extremaValid;
                do
                {
                    minOperand = ConsoleReadUtilities.ReadInt("Lowest number: ", 1, 1001, "Integers between 1 and 1000 inclusive only!");
                    Console.WriteLine();
                    maxOperand = ConsoleReadUtilities.ReadInt("Highest number: ", null, 1001, "Integers less than or equal to 1000 only!");
                    Console.WriteLine();
                    extremaValid = minOperand <= maxOperand;
                    if (!extremaValid)
                    {
                        Console.WriteLine("Highest number must be greater than or equal to lowest number!");
                    }
                } while (!extremaValid);
                int range = maxOperand - minOperand;
                Console.WriteLine("Choosing numbers between {0} and {1} inclusive", minOperand, maxOperand);
                Console.WriteLine();

                Console.WriteLine(Separator);
                Console.WriteLine();

                for (int i = 0; i < questionCount; i++)
                {
                    // Randomly choose 2 numbers for operands
                    int[] operands = new int[2];
                    for (int j = 0; j < operands.Length; j++)
                    {
                        operands[j] = random.Next(0, maxOperand + 1 - minOperand) + minOperand;
                    }

                    // Choose operation
                    int operationCount = Enum.GetNames(typeof(Operation)).Length; // From StackOverflow answer at http://stackoverflow.com/a/856165/3806231 by Kasper Holdum (http://stackoverflow.com/users/71515/kasper-holdum)
                    Operation operation = (Operation)random.Next(0, operationCount);

                    // Create new Expression object using chosen operands and operation, then display expression and input answer from user
                    Expression expression = new Expression(operation, operands);
                    int enteredAnswer = ConsoleReadUtilities.ReadInt(expression.ToString() + " = ");

                    // Check whether entered answer is correct, then display message and update score accordingly
                    int actualAnswer = expression.Evaluate();
                    if (actualAnswer == enteredAnswer)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Correct!");
                        Console.ResetColor();
                        Console.WriteLine(" +1");
                        correctAnswerCount++;
                        score += range;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Incorrect!");
                        Console.ResetColor();
                        Console.WriteLine(" Answer is " + actualAnswer);
                    }

                    // Display current score
                    Console.WriteLine();
                    Console.Write("Score so far: ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(score);
                    Console.Write(correctAnswerCount);
                    Console.ResetColor();
                    Console.WriteLine(" out of {0} correct so far", i + 1);
                    Console.WriteLine();

                    Console.WriteLine(Separator);
                    Console.WriteLine();
                }

                Console.WriteLine("Game finished!");

                Console.Write("Score: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(score);
                Console.Write(correctAnswerCount);
                Console.ResetColor();
                Console.WriteLine(" out of {0} correct", questionCount);

                Console.WriteLine();
            } while (ConsoleReadUtilities.ReadYOrN("Play again? (y/n): "));
            Console.WriteLine();

            Console.Write("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
