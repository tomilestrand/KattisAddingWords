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
            Dictionary<string,int> definitions = new Dictionary<string, int>();
            string input;
            string[] inputs;
            int value;
            List<int> values = new List<int>();

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
                        for (int j = 1; j < inputs.Length; j += 2)
                        {
                            if (definitions.ContainsKey(inputs[j]))
                            {
                                values.Add(definitions[inputs[j]]);
                            }
                            else
                            {
                                values.Add(-1001);
                            }
                        }
                        Console.WriteLine(MakeOutputString(values, inputs, definitions));
                        values.Clear();
                    }
                }
            }
        }

        private static string MakeOutputString(List<int> values, string[] inputs, Dictionary<string, int> definitions)
        {
            StringBuilder builder = new StringBuilder();
            string returnKey = "unknown";

            if (!values.Any(q => q == -1001))
            {
                int returnValue = values[0];
                for (int i = 1; i < values.Count; i++)
                {
                    if (inputs[2*i] == "+")
                    {
                        returnValue += values[i];
                    }
                    else if (inputs[2*i] == "-")
                    {
                        returnValue -= values[i];
                    }
                }
                if (definitions.ContainsValue(returnValue))
                {
                    returnKey = definitions.Single(q => q.Value == returnValue).Key;
                }
            }

            for (int i = 1; i < inputs.Length; i++)
            {
                builder.Append(inputs[i] + " ");
            }
            builder.Append(returnKey);
            return builder.ToString();
        }
    }
}
