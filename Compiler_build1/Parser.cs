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
                /*match((int)tok_names.Begin);
                Statement();
                match('}');*/
            }
            else if(LT(1).text == "main")
            {
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
                    return true;
                }
            }
            return false;
        }
        public void Glob_Syb()
        {
            //bool occured_ch, occured_int = false;
            while (LA(1)==((int)tok_names.Id) )
            {
                Symbol_Tab loc = new Symbol_Tab("Global");
                while (LA(1) != ':')
                {
                    if (LA(1) == (int)tok_names.Id && !Occur(LT(1).text))
                    { 
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
        public void Statement()
        {
            /*CodeGen gen = new CodeGen();
            gen.addChild(new FuncNode(new Token((int)tok_names.Begin, "MAIN")));
            while(lookahead.type != '}')
            {
                switch (lookahead.type)
                {
                    case (int)tok_names.Id:
                        {
                            Token temp = lookahead;
                            consume();
                            match('=');

                            break;
                        }
                }
            }*/
        }
    }
}