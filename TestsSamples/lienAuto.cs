﻿class LienAuto
{
	void EmptyFunc()
	{
		
	}
    int returnBiggest(int x, int y)
    {
        int valRet;

        if (x < y) 
	{
		test;
		titi;
		42;
	}
        else
	{
            
	}
	valret = valret + 42;
        return valRet;
    }

    int returnParamA(int a)
    {
        return a;
    }
	
	void DepthExpressions()
	{
		int variable;
		variable = 5 + 3 + (2 - 9) + 10 + 9 + (8 * 9);
	}

    int returnDeclParam()
    {
        int a;

        a = 42;
        return a;
    }

    void multipleDeclParam()
    {
		if (true)
		{
		int a;
		int b;
		char c, d, e;
		}
    }

    void ifStmt(char c) {
	if (c == 'c')	    
            Console.WriteLine("Test");
return;
    }

    void ifStmtBlock(char c) {
	if (c == 'c')
	{
	    Console.WriteLine("Test block if1");
	    Console.WriteLine("Test block if2");
	    Console.WriteLine("Test block if3");
	}
    }

    void testSwitch(char c) {
	switch (c) {
	   case 'a':
	      Console.writeLine("case a");
	      break;
	   case 'b':
	      Console.writeLine("case b");
	      break;
	   default:
	      Console.writeLine("other");
	      break;	
	}
    }

   void ifElseStmt(char c) {
	if (c == 'c')
	{
	    Console.WriteLine("Test block if1");
	    Console.WriteLine("Test block if2");
	    Console.WriteLine("Test block if3");
	}
        else 
	{
	    Console.WriteLine("Test block else1");
	    Console.WriteLine("Test block else2");
	    Console.WriteLine("Test block else3");
	}
	Console.writeLine("END IF/ELSE");
   }

    void loopWhile() {
	int i;

	i = 0;
	while (i < 10)
	    Console.writeLine("test while");
    }

    void loopWhileBlock() {
	int i;

	i = 0;
	while (i < 10)
	{
	    Console.writeLine("test while 1");
	    Console.writeLine("test while 2");
	}
     }

    void loopDoWhile() {
	int x = 0;
        do 
        {
            Console.WriteLine(x);
            x++;
        } while (x < 42);
    }

    void loopFor() {
	for (int x = 0; x < 42; x++) 
    	{
	    Console.writeLine(x);
	    Console.writeLine("test for");
	}
	Console.writeLine("fin du for");
    }

    void loopForEach() {
	foreach (int x in truc)
	{
	    Console.writeLine(x);
	    Console.writeLine("test foreach");
	}
	Console.writeLine("fin du foreach");
    }



    
}