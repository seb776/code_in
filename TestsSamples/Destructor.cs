using System;

public class DtorTest
{
    String foo;
    int bar;

	public DtorTest()
	{
        foo = new String("lalalalala test lalala");
        bar = 42;
	}

    public ~DtorTest()
    {
        bar = 30;
        foo = null;
    }
}
