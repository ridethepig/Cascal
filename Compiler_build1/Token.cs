using System;
namespace Compiler_build1{
    public class Token{
        public int type;
        public string text;
        public bool isstr = false;
        public Token(int type,string text){
            this.type = type;
            this.text = text;
        }
        public Token(string text) {this.text = text; }
        public Token(int type)
        {
            this.type = type;
        }
        public Token(int type,char ch)
        {
            this.type = type;
            this.text = "";
            this.text += ch;
        }        
    }
}