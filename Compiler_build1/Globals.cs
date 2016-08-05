using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Compiler_build1
{
    public enum tok_names
    {
        Num = 2, Fun, Sys, Glo, Loc, Id,
        Char, Else, If, Int, Return, Sizeof, While,
        Assign, Lor, Lan , Lno, Or, Xor, And, Eq, Ne, Lt, Gt, Le, Ge , Add, Sub, Mul, Div, Mod, Inc, Dec, Brak , Block , Var
    };
    public struct identifier{
        public int tok;
        public int has;
        public string nam;
        public int cla;
        public int typ;
        public int val;
        public int Glo_cla;
        public int Glo_typ;
        public int Glo_val;
    }
    public class globals
    {
        public static int line = 1;
        public static string[] keyIds = { "break", "if", "else", "int", "print", "println" };
        public static string[] tokenNames = { "Number" };
    }
}