class LienAuto
{

    int returnBiggest(int x, int y)
    {
        int valRet;

        if (x < y) 
	{
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

    int returnDeclParam()
    {
        int a;

        a = 42;
        return a;
    }

    void multipleDeclParam()
    {
	int a;
	int b;
	char c, d, e;
    }

    void loopWhile() 
    {
	int a;
	
	a = 0;
	while (a < 42) 
	{
	}
    }

    void ifStmt(char c) {
	if (c == 'c')	    
            Console.WriteLine("Test");
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



    
}