using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Compiler_build1
{
    public class Symbol_Tab
    {
        public int type;
        public string scope;
        public List<string> children;
        public Symbol_Tab(int type,string scope)
        {
            this.type = type;
            this.scope = scope;
            this.children = new List<string>();
        }
        public Symbol_Tab(string scope)
        {
            this.scope = scope;
            this.children = new List<string>();

        }
    }
    public class Parser_Base
    {
        lexer input;
        protected Token[] lookahead;
        int k;
        int p = 0;
        public Parser_Base(lexer input,int k)
        {
            this.input = input;
            this.k = k;
            lookahead = new Token[k];
            for (int i = 1;i <= k; i++) { consume(); }
        }
        public void consume()
        {
            lookahead[p] = input.nextToken();
            p = (p + 1) % k;
        }
        public Token LT(int i) { return lookahead[(p + i - 1) % k]; }
        public int LA(int i) { return LT(i).type; }
        public void match(int x)
        {
            if (LA(1) == x) { consume(); }
            else throw new Exception("unexpected char: " + LT(1).text);
        }
    }
    public class Parser : Parser_Base
    {
        public Stack<Token> stk1 = new Stack<Token>(), stk2 = new Stack<Token>();
        public static CodeGen gen = new CodeGen();
        public static List<Symbol_Tab> Global_Symbol_Tab = new List<Symbol_Tab>();
        public static List<string> Occured = new List<string>();
        public Parser(lexer input,int k):base(input,k){; }
        public void GlobDaclare()
        {
            //consume();
            if (LT(1).text == "VAR")
            {
                consume();
                match('{');
                Glob_Syb();      
                match('}');
                match((int)tok_names.Begin);
                Statement();
                match('}');
            }
            else if(LT(1).text == "MAIN")
            {
                consume();
                match('{');
                Statement();
                match('}');
            }
            else
            {
                throw new Exception("parse err: "+LT(1).text);
            }
        }
        public bool Occur(string text)
        {
            for(int i = 0;i < Occured.Count; i++)
            {
                if (Occured[i] == text)
                {
                    throw new Exception("Name '" + text + "' were declared.");
                }
            }
            return false;
        }
        public bool IN_SYB_TAB(string text)
        {
            for (int i = 0; i < Occured.Count; i++)
            {
                if (Occured[i] == text)
                {
                    return true;
                }
            }
            throw new Exception("Name '" + text + "' were not declared.");
        }
        public void Glob_Syb()
        {
            //bool occured_ch, occured_int = false;
            while (LA(1)==((int)tok_names.Id) )
            {
                Symbol_Tab loc = new Symbol_Tab("Global");
                while (LA(1) != ':')
                {
                    if (LA(1) == (int)tok_names.Id )
                    {
                        Occur(LT(1).text);
                        loc.children.Add(LT(1).text);
                        Occured.Add(LT(1).text);
                        consume();
                        
                    }
                    else
                        consume();
                }
                consume();
                switch (LT(1).text)
                {
                    case "INTEGER": loc.type = 1; consume(); break;
                    case "CHAR":loc.type = 2; consume(); break;
                    default:
                        throw new Exception("No Such Type" + LT(1).text);
                }
                Global_Symbol_Tab.Add(loc);
                match(';');
            }
        }
        public int level( Token t)
        {
            if (t.type == (int)tok_names.Add || t.type == (int)tok_names.Sub)
            {
                return 1;
            }
            if (t.type == (int)tok_names.Mul || t.type == (int)tok_names.Div || t.type == (int)tok_names.Mod)
            {
                return 2;
            }
            if (t.type == '(')
            {
                return 0;
            }
            if (t.type == ')')
            {
                return 4;
            }
            throw new Exception("level err:" + t.text);
        }
        public void Statement()
        {
            int ptr_chd = 0;            
            gen.addChild(new AST(new Token((int)tok_names.Begin, "MAIN")));
            while(true)
            {
                switch (LA(1))
                {
                    case (int)tok_names.Id:
                        if (LA(2) == (int)tok_names.Assign)
                        {
                            IN_SYB_TAB(LT(1).text);
                            gen.children[0].addChild(new AST(new Token((int)tok_names.Stmt, gen.children[0].token.text)));
                            gen.children[0].children[ptr_chd].addChild(new AST(new Token((int)tok_names.Assign, "=")));
                            gen.children[0].children[ptr_chd].children[0].addChild(1,new AST(new Token((int)tok_names.Id, LT(1).text)));
                            consume();
                            gen.children[0].children[ptr_chd].children[0].addChild(Expression());
                            break;
                        }
                        else
                            throw new Exception("expecting '=';get " + LT(2).text);
                    case (int)tok_names.If:break;
                    case (int)tok_names.While:break;
                    case (int)tok_names.Sys: switch (LT(1).text)
                        {
                            case "print":break;
                            case "println":break;
                            case "readln":break;
                            case "break":break;
                            case "continue":break;
                            case "exit":break;                            
                        }
                        break;
                }
            }
        }
        public bool isOp(Token t)
        {
            int tp = t.type;
            return (tp == (int)tok_names.Add || tp == (int)tok_names.Sub || tp == (int)tok_names.Mul || tp == (int)tok_names.Div || tp == (int)tok_names.Mod);
        }
        public AST Expression()
        {            
            while (LT(1).text != ";")
            {
                consume();
                if (LT(1).type == (int)tok_names.Id)
                {
                    IN_SYB_TAB(LT(1).text);
                    stk1.Push(LT(1));
                }
                else if (LT(1).type == (int)tok_names.Id)
                {
                    stk1.Push(LT(1));
                }
                else if (LT(1).type == ')')
                {
                    while (stk2.Peek().type != '(')
                    {
                        stk1.Push(stk2.Pop());
                    }
                    stk2.Pop();
                }
                else if (LT(1).type == '(')
                {
                    stk2.Push(LT(1));
                }
                else if (isOp(LT(1)))
                {
                    if (stk2.Count == 0)
                    {
                        stk2.Push(LT(1));
                    }
                    else if (level(LT(1)) >= level(stk2.Peek()))
                    {
                        stk2.Push(LT(1));
                    }
                    else if (level(LT(1)) < level(stk2.Peek()))
                    {
                        while (stk2.Count > 0)
                        {
                            if (level(stk2.Peek()) <= level(LT(1))) { break; }
                            stk1.Push(stk2.Pop());
                            stk2.Push(LT(1));
                        }
                    }
                }
            }
            if (stk2.Count > 0)
            {
                while (stk2.Count > 0)
                {
                    stk1.Push(stk2.Pop());
                }
            }
            Token[] arr = stk1.ToArray();
            consume();            
            return root;
        }
    }
}