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
            if (root.children == null || root.children.Count == 0)
            {
                if (root.lch != null)
                {
                    ExprWalk(root);
                }
                else
                {
                    return;
                }
            }
            else
            {
                switch (root.token.text)
                {
                    //case "READLN":
                }
            }
            foreach (AST x in root.children)
            {
                walk(x);
            }
        }
    }
}