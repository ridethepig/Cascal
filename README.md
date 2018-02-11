# Cascal
### This is my Toy Compiler.
It has many disabilities.I specially wrote a file to show what things it can't do.As for why it is named 'Cascal',because I used Pascal-like variable declaration and C-like grammar :-) .The proudest thing is that I will finish it by hand.I will also put my refrence code here.  

***
## Details
·LL(k) Parser;  
·Uses AST;  
·So far,it is only a Interpreter(next update,I will add a VM support and IR). :-(

## Warning: There Still May Be Some Bugs... 
***
##		  License:
License? What is License? I don't care about it. Since I don't care,use MIT or BSD.//I don't think anyone would like to use my code...^_^
***
# Cascal
### 编译器实验
我的编译器（语言）有很多不能干的事情。我还特地写了一个文件来告诉你们它不能干什么。^_^
至于为什么它叫做Cascal，是因为我采用了Pascal的变量声明方式和C语言语法。：-）
它是纯手工的。
我将奉上惨不忍睹的代码以及良好的参考代码（主要来自于Language Implementation Patterns的示例代码）
### 实现细节
·LL(k)的语法分析  
·好不容易写了个AST  
·暂时只支持解释运行（下一次更新时将支持虚拟机和IR）。 :-(  
## 许可协议：
License?我不管什么协议，随便吧，MIT 或 BSD。//我估计没人想要
#####抱歉，我的网络很不好，没办法上传可执行文件，只好放在zip里p一起放源码里了
## 使用方法
>[可执行文件名] [代码文件名]

## 示例
>VAR{  
	A ,B : INTEGER;  
	CH1 : CHAR;  
}  
MAIN{  
	READLN(A);  
	B = A + 1;  
	PRINTLN(B);  
	PRINT("HELLO,WORLD.");  
	EXIT(0);  
	/?THE REST WILL NOT BE EXECUTED,BUT IT IS LEGAL?/  
	PRINTLN();  
	PRINT();  
}

I have planned 6~7 times' updates.The next update will be released in at least one year. :-( Just because I am very busy.  
Update 1: add control flow 'if' statement  
Update 2: add control flow 'while' statement  
Update 3: add pointer  
Update 4: add array  
Update 5: add self-define function  
Update 6: bitcode and VM//exciting  
Update 7: use llvm to compile to binary file//may not be released
