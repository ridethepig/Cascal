using System;

namespace Compiler_build1
{
    public abstract class lexer
    {
        protected bool ws = true;
        public static int EOF_T = 1;
        public static char EOF = (char) 255;
        protected string input;
        protected int p = 0;
        protected char c;
        public  lexer(string input)
        {
            this.input = input;
            try
            {
                c = input[p];                
            }
            catch
            {
                Console.WriteLine("F**k,where is your code?");
                System.Environment.Exit(-1);
            }
        }
        public void consume()
        {
            advance();
            if (ws)
            {
                WS();
            }
        }
        public void advance()
        {
            p++;
            if (p >= input.Length)
            {
                c = EOF;
            }
            else
            {
                c = input[p];
            }
        }
        public void match(char x)
        {
            if ( c == x ){consume();}
            else throw new Exception("expecting "+x+"; found "+c);
        }
        public abstract Token nextToken();
        public abstract void WS();
        //public abstract string getTokenName(int tokType);
    }
    public class listlexer : lexer
    {
        public static int NOTHING = 255;
        public listlexer(string input) : base(input) {; }
        bool isIdentifier_1()
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c == '_');
        }
        bool isIdentifier_2()
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9') || (c == '_');
        }
        bool isDigit()
        {
            return c >= '0' && c <= '9';
        }
        public override Token nextToken()
        {
            while (c != EOF)
            {
                switch (c)
                {
                    case ' ': case '\t': case '\n': case '\r': WS(); continue;
                    case ',': case ';': case '{': case '}': case '(': case ')':
                    case ']': case ':' : { char t = c; consume(); return new Token(t); }
                    case '/': consume(); if (c == '?') { consume(); Comment(); continue; } return new Token((int)(tok_names.Div), "/");
                    case '*': consume(); return new Token((int)(tok_names.Mul), "*");
                    case '=': consume(); if (c == '=') { consume(); return new Token((int)(tok_names.Eq), "=="); } return new Token((int)(tok_names.Assign), "=");
                    case '+': consume(); return new Token((int)(tok_names.Add), "+");
                    case '-': consume(); return new Token((int)(tok_names.Sub), "-");
                    case '!': consume(); if (c == '=') { consume(); return new Token((int)(tok_names.Ne), "!= "); } return new Token((int)(tok_names.Lno), "!");
                    case '>': consume(); if (c == '=') { consume(); return new Token((int)(tok_names.Ge), ">= "); } return new Token((int)(tok_names.Gt), ">");
                    case '<': consume(); if (c == '=') { consume(); return new Token((int)(tok_names.Le), "<= "); } return new Token((int)(tok_names.Lt), "<");
                    case '|': consume(); if (c == '|') { consume(); return new Token((int)(tok_names.Lor), "||"); } throw new Exception("Sorry,we don't support Bit Operator \"|\"");
                    case '&': consume(); if (c == '&') { consume(); return new Token((int)(tok_names.Lor), "&&"); } throw new Exception("Sorry,we don't support Bit Operator \"&\"");
                    case '%': consume(); return new Token((int)(tok_names.Mod), "%");
                    case '[': consume(); return new Token((int)(tok_names.Brak), "[");
                    case '"': case '\'': return StrSolution();
                    default:
                        {
                            if (isIdentifier_1()) return NAME();
                            if (isDigit()) return Digit();
                            throw new Exception("Invalid character: " + c);
                        }
                }
            }
            return new Token(EOF_T, "<EOF>");
        }
        public override void WS()
        {
            if (c == '\n') { globals.line++; }  
            while (c == ' ' || c == '\t' || c == '\n' || c == '\r') consume();
        }
        public Token NAME()
        {
            string local = "";
            do
            {
                local += c;
                consume();
            }
            while (isIdentifier_2());
            if (isKeyId(local))
            {
                return new Token((int)(tok_names.Sys),local);
            }
            if (local == "SIZEOF")
            {
                return new Token((int)(tok_names.Sizeof), local);
            }
            if (local == "IF")
            {
                return new Token((int)(tok_names.If), local);

            }
            if(local == "WHILE")
            {
                return new Token((int)(tok_names.While), local);

            }
            return new Token((int)(tok_names.Id),local);
        }
        public Token Digit()
        {
            string local = "";
            do
            {
                local += c;
                consume();
            }
            while (isDigit());
            return new Token((int)(tok_names.Num), local);
        }
        public Token StrSolution()
        {
            char temp = c;
            string local = "";
            consume();
            ws = false;
            while (c != temp)
            {
                local += c;
                consume();
            }
            ws = true;
            consume();
            return new Token((int)(tok_names.Num), local);
        }
        public void Comment()
        {
            while (true)
            {
                consume();
                if (c == '?')
                {
                    consume();
                    if (c == '/')
                    {
                        consume();
                        break;
                    }
                }
            }
        }
        public bool isKeyId(string id)
        {
            for (int i = 0;i < globals.keyIds.Length; i++)
            {
                if (id == globals.keyIds[i])
                {
                    return true;
                }
            }
            return false;
        }
    }
}