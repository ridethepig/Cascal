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
            string filepath = "";
            if (args.Length == 0)
            {
                filepath = Convert.ToString(Console.ReadLine());
            }
            else
            {
                filepath = args[0];
            }
            string input = File.ReadAllText(filepath);
            listlexer lex = new listlexer(input);
            Parser par = new Parser(lex);
            par.GlobDaclare();
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
