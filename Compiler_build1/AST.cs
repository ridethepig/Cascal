using System.Collections.Generic;
namespace Compiler_build1
{
    unsafe public class AST
    {
        public string parameter_str = null;
        public string parameter_tok = null;
        public Token token;
        public List<AST> children;
        public AST lch;
        public AST rch;
        public AST() {; }
        public AST(Token t) { token = t; }
        public AST(int tokenType) { this.token = new Token(tokenType); }

        public int getNodeType() { return token.type; }

        public void addChild(AST t)
        {
            if (children == null)
            {
                children = new List<AST>();
            }
            children.Add(t);
        }
        public bool isNil() { return token == null; }
        public void addChild(int chtype,AST ch)
        {
            if (chtype == 1)
            {
                lch = ch;            
            }
            else if(chtype == 2)
            {
                rch = ch;
            }
        }
        /*public string toString() { return token.text; }
        public string toStringTree()
        {
            if (children == null || children.Count() == 0)
            {
                return this.toString();
            }
            string local = "";
            if (!isNil())
            {
                local += "(";
                local = this.toString() + local;
                local += " ";
            }
            for (int i = 0; children != null && i < children.Count(); i++)
            {
                AST t = children[i];
                if (i > 0)
                {
                    local += " ";
                }
                local = local + t.toStringTree();
            }
            if (!isNil())
            {
                local += ")";
            }
            return local;
        }*/
    }
    public class CodeGen
    {
        public Token token;
        public List<AST> children;

        public CodeGen() {; }

        public void addChild(AST t)
        {
            if (children == null)
            {
                children = new List<AST>();
            }
            children.Add(t);
        }
    }
}