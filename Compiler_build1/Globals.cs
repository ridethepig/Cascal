namespace Compiler_build1
{
    public enum tok_names
    {
        Num = 2, Fun,Begin, Sys, Glo, Loc, Id,
        Char, Else, If, Int, Return, Sizeof, While,
        Assign, Lor, Lan , Lno, Or, Xor, And, Eq, Ne, Lt, Gt, Le, Ge , Add, Sub, Mul, Div, Mod, Inc, Dec, Brak , Stmt , Var
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
        public static string[] keyIds = { "BREAK", "CONTINUE", "PRINT", "PRINTLN", "READLN","EXIT" };
        public static string[] tokenNames = { "Number" };
    }
}