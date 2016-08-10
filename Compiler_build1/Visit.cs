using System;
using System.Collections.Generic;

namespace Compiler_build1
{
    class Visit
    {
        public Visit() {; }
        public void ExprWalk(AST root)
        {

        }
        public void walk(AST root)
        {
            Parser.Dict_str["CH1"] = "I'M ";
            if (root.children == null || root.children.Count == 0)
            {
                if (root.lch != null)
                {
                    ExprWalk(root);
                }
                else
                {
                    switch (root.token.text)
                    {
                        case "READLN":
                            {
                                string key = root.parameter_tok;
                                if (!Parser.Dict_id_type.ContainsKey(key))
                                {
                                    throw new Exception("undeclared identifier: " + key);
                                }
                                if (Parser.Dict_id_type[key] == 1)
                                {
                                    Parser.Dict_main[key] = Convert.ToInt64(Console.ReadLine());
                                }
                                else if (Parser.Dict_id_type[key] == 2)
                                {
                                    Parser.Dict_str[key] = Convert.ToString(Console.ReadLine());
                                }
                                break;
                            }
                        case "PRINT":
                            {
                                if (root.parameter_tok != null)
                                {
                                    string key = root.parameter_tok;
                                    if (Parser.Dict_id_type[key] == 1)
                                    {
                                        Console.Write(Parser.Dict_main[key]);
                                    }
                                    else
                                    {
                                        Console.Write(Parser.Dict_str[key]);
                                    }
                                }
                                else
                                {
                                    Console.Write(root.parameter_str);
                                }
                                break;
                            }
                        case "PRINTLN":
                            {
                                if (root.parameter_tok != null)
                                {
                                    string key = root.parameter_tok;
                                    if (Parser.Dict_id_type[key] == 1)
                                    {
                                        Console.Write(Parser.Dict_main[key]);
                                    }
                                    else
                                    {
                                        Console.Write(Parser.Dict_str[key]);
                                    }
                                }
                                else if (root.parameter_str != "")
                                {
                                    Console.WriteLine(root.parameter_str);
                                }
                                else
                                {
                                    Console.WriteLine();
                                }
                                break;
                            }
                        case "EXIT":
                            {
                                if (root.parameter_tok != null)
                                {
                                    string key = root.parameter_tok;
                                    Console.WriteLine("PROGRAM EXIT WITH ({0})", Parser.Dict_main[key]);
                                }
                                else if (root.parameter_str != "")
                                {
                                    Console.WriteLine("PROGRAM EXIT WITH ({0})", root.parameter_str);
                                }
                                Environment.Exit(0);
                                break;
                            }
                    }
                    return;
                }
            }
            foreach (AST x in root.children)
            {
                walk(x);
            }
        }
    }
}