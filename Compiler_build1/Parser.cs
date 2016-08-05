using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Compiler_build1
{
    public class Parser
    {
        public lexer input;
        Token lookahead;
        public static List<List<string>> Local_Symbol_Tab = new List<List<string>>();
        public static List<string> Global_Symbol_Tab = new List<string>();
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
                             "; found " + lookahead.text*/"unexpected character");
        }
        public void GlobDaclare()
        {
            consume();
            if (lookahead.text == "VAR")
            {
                consume();
                match('{');
                Glob_Syb();
                consume();
                match('}');
            }
            else if(lookahead.text == "{")
            {
                Statement();
                match('}');
            }
        }
        public void Glob_Syb()
        {
            
        }
        public void Statement()
        {

        }
    }
}