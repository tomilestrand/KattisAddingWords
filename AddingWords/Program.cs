using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddingWords
{
    class Program
    {
        static void Main(string[] args)
        {
            var definitions = new Dictionary<string, int>();

            string input;
            string[] inputs;
            int value, i;
            List<string> calc = new List<string>();
            List<int> values = new List<int>();
            List<char> operands = new List<char>();

            while ((input = Console.ReadLine()) != null)
            {
                inputs = input.Split(' ');
                if (inputs[0] == "clear")
                {
                    definitions.Clear();
                }
                else
                {
                    if (inputs[0] == "def")
                    {
                        value = int.Parse(inputs[2]);
                        if (!definitions.ContainsKey(inputs[1]))
                        {
                            definitions.Add(inputs[1], value);
                        }
                        else
                        {
                            definitions[inputs[1]] = value;
                        }

                    }
                    else
                    {
                        i = 1;
                        do
                        {
                            calc.Add(inputs[i++]);

                        } while (calc.Last() != "=");
                        for (int j = 0; j < calc.Count; j++)
                        {
                            if (j % 2 == 0)
                            {
                                if (definitions.ContainsKey(calc[j]))
                                {
                                    values.Add(definitions[calc[j]]);
                                }
                                else
                                {
                                    values.Add(-1001);
                                }
                            }
                            else
                            {
                                operands.Add(calc[j][0]);
                            }
                        }
                        Console.WriteLine(MakeOutputString(values, operands, definitions, calc));
                        calc.Clear();
                        values.Clear();
                        operands.Clear();
                    }
                }
            }
        }

        private static string MakeOutputString(List<int> values, List<char> operands, Dictionary<string, int> definitions, List<string> calc)
        {
            StringBuilder builder = new StringBuilder();
            string returnKey = "unknown";

            if (!values.Any(q => q == -1001))
            {
                int returnValue = values[0];
                for (int i = 1; i < values.Count; i++)
                {
                    if (operands[i - 1] == '+')
                    {
                        returnValue += values[i];
                    }
                    else if (operands[i - 1] == '-')
                    {
                        returnValue -= values[i];
                    }
                }
                if (definitions.ContainsValue(returnValue))
                {
                    returnKey = definitions.Single(q => q.Value == returnValue).Key;
                }
            }

            for (int i = 0; i < values.Count; i++)
            {
                builder.Append(calc[2 * i] + " ");
                builder.Append(operands[i] + " ");
            }
            builder.Append(returnKey);
            return builder.ToString();
        }
    }
}
