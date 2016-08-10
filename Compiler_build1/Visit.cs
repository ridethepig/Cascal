using System;
using System.Collections.Generic;

namespace Compiler_build1
{
    class Visit
    {
        public Stack<int> op = new Stack<int>();
        public string cur_res_str = "";
        public int type = 0;
        public long cur_res_int = 0;
        public Visit() {; }
        public void ExprSolution(AST root)
        {
            bool ac_left = false, ac_right = false;
            ac_left = root.lch.token.type != (int)tok_names.Id || root.lch.token.type != (int)tok_names.Num;
            ac_right = root.rch.token.type != (int)tok_names.Id || root.rch.token.type != (int)tok_names.Num;
            if (!(ac_left && ac_right))
            {
                if (!ac_left)
                {
                    op.Push(root.lch.token.type);
                    ExprSolution(root.lch);
                }
                if (!ac_right)
                {
                    op.Push(root.rch.token.type);
                    ExprSolution(root.rch);
                }
            }                        
            switch (root.lch.token.type)
            {
                case (int)tok_names.Id:type = Parser.Dict_id_type[root.lch.token.text];break;
                case (int)tok_names.Num:if (root.lch.token.isstr) { type = 2; }else { type = 1; }break;
            }
            if (type == 1)
            {
                long lch = 0, rch = 0;
                if (root.lch.token.type == (int)tok_names.Id)
                {
                    lch = Parser.Dict_main[root.lch.token.text];
                }
                else
                {
                    lch = Convert.ToInt64(root.lch.token.text);
                }
                if (root.rch.token.type == (int)tok_names.Id)
                {
                    rch = Parser.Dict_main[root.rch.token.text];
                }
                else
                {
                    rch = Convert.ToInt64(root.rch.token.text);
                }
                switch (op.Pop())
                {
                    case (int)tok_names.Add : cur_res_int = lch + rch; break;
                    case (int)tok_names.Sub : cur_res_int = lch - rch; break;
                    case (int)tok_names.Mul : cur_res_int = lch * rch; break;
                    case (int)tok_names.Mod : cur_res_int = lch / rch; break;
                    case (int)tok_names.Div : cur_res_int = lch % rch; break;
                }
                root = new AST(new Token((int)tok_names.Num, Convert.ToString(cur_res_int)));
            }
            else
            {
                switch (op.Pop())
                {
                    case (int)tok_names.Add : cur_res_str = root.lch.token.text + root.rch.token.text;break;
                    case (int)tok_names.Sub : throw new Exception("no such operator for string");
                    case (int)tok_names.Mul : throw new Exception("no such operator for string");
                    case (int)tok_names.Mod : throw new Exception("no such operator for string");
                    case (int)tok_names.Div : throw new Exception("no such operator for string");
                }
                root = new AST(new Token((int)tok_names.Num, cur_res_str));
            }                                 
        }
        public void walk(AST root)
        {
            Parser.Dict_str["CH1"] = "I'M ";
            if (root.children == null || root.children.Count == 0)
            {
                if (root.lch != null && root.token.text == "=")
                {
                    ExprSolution(root.rch);
                    type = 0;
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