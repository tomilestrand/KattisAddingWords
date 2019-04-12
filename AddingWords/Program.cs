using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kattis.IO;

namespace AddingWords
{
    class Program
    {
        static void Main(string[] args)
        {
            var definitions = new List<Definition>();
            var scanner = new Scanner();

            string action;
            string key;
            int value;
            List<string> calc = new List<string>();
            List<int?> values = new List<int?>();
            List<char> operands = new List<char>();
            Definition temp;

            while (scanner.HasNext())
            {
                action = scanner.Next();
                if (action == "clear")
                {
                    definitions.Clear();
                }
                else
                {
                    if (action == "def")
                    {
                        key = scanner.Next();
                        value = scanner.NextInt();
                        temp = definitions.SingleOrDefault(q => q.Key == key);
                        if (temp == null)
                        {
                            definitions.Add(new Definition(value, key));
                        }
                        else
                        {
                            temp.Value = value;
                        }

                    }
                    else
                    {
                        do
                        {
                            calc.Add(scanner.Next());

                        } while (calc.Last() != "=");
                        for (int i = 0; i < calc.Count; i++)
                        {
                            if (i % 2 == 0)
                            {
                                temp = definitions.SingleOrDefault(q => q.Key == calc[i]);
                                if (temp != null)
                                {
                                    values.Add(temp.Value);
                                }
                                else
                                {
                                    values.Add(null);
                                }
                            }
                            else
                            {
                                operands.Add(calc[i][0]);
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

        private static string MakeOutputString(List<int?> values, List<char> operands, List<Definition> definitions, List<string> calc)
        {
            StringBuilder builder = new StringBuilder();
            string returnKey = null;
            Definition temp;
            if (!values.Any(q => q == null))
            {
                int? returnValue = values[0];
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
                temp = definitions.SingleOrDefault(q => q.Value == returnValue);
                if (temp != null)
                {
                    returnKey = temp.Key;
                }
            }

            if (returnKey == null)
            {
                returnKey = "unknown";
            }
            for (int i = 0; i < values.Count; i++)
            {
                builder.Append(calc[2 * i] + " ");
                builder.Append(operands[i] + " ");
                if (i == values.Count - 1)
                {
                    builder.Append(returnKey);
                }
            }
            return builder.ToString();
        }
    }

    class Definition
    {
        public Definition(int value, string key)
        {
            Value = value;
            Key = key;
        }

        public int Value { get; set; }
        public string Key { get; set; }
    }
}
