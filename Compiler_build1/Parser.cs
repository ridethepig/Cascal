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
    public class Parser
    {
        public lexer input;
        Token lookahead;
        public static List<Symbol_Tab> Global_Symbol_Tab = new List<Symbol_Tab>();
        public static List<string> Occured = new List<string>();
        public Parser(lexer input)
        {
            this.input = input;
        }
        public void consume()
        {
            lookahead = input.nextToken();
        }
        public void match(int x)
        {
            if (lookahead.type == x)
            {
                consume();
            }
            else throw new Exception(/*"expecting " + input.getTokenName(x) +
                             "; found " + lookahead.text*/"unexpected character:"+lookahead.text+":"+lookahead.type);
        }
        public void GlobDaclare()
        {
            consume();
            if (lookahead.text == "VAR")
            {
                consume();
                match('{');
                Glob_Syb();      
                match('}');
                /*match((int)tok_names.Begin);
                Statement();
                match('}');*/
            }
            else if(lookahead.text == "main")
            {
                Statement();
                match('}');
            }
            else
            {
                throw new Exception("F**k!Where is your code?");
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
            while (lookahead.type==((int)tok_names.Id) )
            {
                Symbol_Tab loc = new Symbol_Tab("Global");
                while (lookahead.type != ':')
                {
                    if (lookahead.type == (int)tok_names.Id && !Occur(lookahead.text))
                    { 
                        loc.children.Add(lookahead.text);
                        Occured.Add(lookahead.text);
                        consume();
                        
                    }
                    else
                        consume();
                }
                consume();
                switch (lookahead.text)
                {
                    case "INTEGER": loc.type = 1; consume(); break;
                    case "CHAR":loc.type = 2; consume(); break;
                    default:
                        throw new Exception("No Such Type" + lookahead.text);
                }
                Global_Symbol_Tab.Add(loc);
                match(';');
            }
        }
        public void Statement()
        {

        }//´¦ÀíÓï¾ä¿é
    }
}