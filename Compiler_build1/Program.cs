using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Compiler_build1
{
    class Program
    {
        static void Main(string[] args)
        {
            //            FileStream sFile = new FileStream("hello.c", FileMode.Open);
            string input = File.ReadAllText(args[0]);
            listlexer lex = new listlexer(input);
            /*Token t = lex.nextToken();
            while (t.type != listlexer.EOF_T)
            {
                Console.WriteLine("<"+t.type+","+t.text+">");
                t = lex.nextToken();
            }
            Console.WriteLine("<" + t.type + "," + t.text + ">");
            Console.WriteLine(globals.line);*/
        }
    }
}
