﻿using System;

public class DescriptionAttribute : Attribute 
{
    public string _description {get; set;}
    
    DescriptionAttribute(string description) {
        _description = description;
    }
}

public interface IexhaustiveTest
{
    private string _name;

    public void getName() {
        return _name;
    }

    public void setName(string name)
    {
        _name = name;
    }
}

public class exhaustiveTest : heritage1, heritage2
{
    enum Months
    {
        Jan, Feb, Mar, Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec
    }

    public int _publicInt;
    private int _privateInt;
    protected int _protectedInt;
    static int _staticInt;


    public exhaustiveTest() 
	{
        MessageBox.Show("here is a constructor with no parameter");
    }
    
    public exhaustiveTest(string name)
    {
        MessageBox.Show("Here is a constructor with a name. Name is : " + name);
    }

    public exhaustiveTest(String name, int value) 
    {
    }

    [DescriptionAttribute("This method is only assigning values")]
    void funcAssign() {
        var assignNull = null;
        int assignInt = 42;
        string assignString = "quarante deux";
        int[] assignArray = new Int[42];
        int assignAdd = assignInt + 42;
        int assignMinus = assignInt - 42;
        int assignMult = assignInt * 42;
        float assignDivision = assignInt / 42.0;
        float assignModulo = assignInt % 42.0;
        int assignIncrement = ++asignInt;
        int assignDecrement = --assignInt;
        var assignBoolEqual = (assignInt == 42);
        var assignBoolDif = (assignInt != 42);
        var assignBoolInf = (assignInt < 42);
        var assignBoolSup = (assignInt > 42);
        var assignBoolSupEqual = (assignInt >= 42);
        var assignBoolInfEqual = (assignInt <= 42);
        var assignBoolAnd = (true && assignBoolDif);
        var assignBoolOr = (false || assignBoolDif);
        var assignBoolNot = !assignBoolDif;
        GenericList<int> assignGenericListInt = new GenericList<int>();
	// int *pointer = &assignArray[42];
	this.privateInt = 42;
    }

    void funcCondition(int x, int y) {
        if ((x == y) || (x < y)) {
            funcAssign();
            printDay(x);
        }
        else {
            MessageBox.Show("let's do a recursive");
            funcCondition(x - 1, y);
        }

        switch (x) {
            case 42 + 12:
                MessageBox.Show("Excellent !");
                break;
            case 12:
                MessageBox.Show("12...");
                break;
            default:
                MessageBox.Show("Not 42 and not 12");
                break;
        }
    }

    void funcLoop() {
        int countWhile = 42;
        while (countWhile > 0) {
            MessageBox.Show("While loop");
            MessageBox.Show("Still not negative: value is " + coundWhile.ToString());
            --coundWhile;
	   continue;
        }

        for (int countFor = 42; i > 0; --countFor) {
            MessageBox.Show("For loop");
            MessageBox.Show("Still not negative: value is " + countFor.ToString());
            --countFor;
        }

        int countDoWhile = 42;
        do {
            MessageBox.Show("Do while loop");
            MessageBox.Show("Still not negative: value is " + x.ToString());
            --countDoWhile;
        } while (countDoWhile > 0);

        int total = 0;
        var arrayInt = new int[] { 42, 12, 7, 92 };
        foreach (var val in arrayInt)
        {
            if (val is int)
                total += val as int;
        }
    }

    void DepthExpressions()
    {
        int variable = 5 + 3 + (2 - 9) + 10 + 9 + (8 * 9);
    }

    void printDay(int x) {
        var day = new String[7] {"Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"};
        if (x > 7)
        {
            MessageBox.Show("Cannot print this day");
            return;
        }
        for (int i = 0; i < day.Length; ++i)
        {
            if (i == x) {
                MessageBox.Show(day[i]);
                break;
            }
        }
    }

    int getNumberMonth()
    {
        int month = (int)Months.Jul;
        return month;
    }

    private void funcPrivate()
    {
        MessageBox.Show("this function is to test private method");
    }

    public void funcPublic()
    {
        MessageBox.Show("this function is to test public method");
    }

    protected void funcProtected()
    {
        MessageBox.Show("this function is to test protected method");
    }

    void funcThrow()
    {
        throw new NotImplementedException();
    }

    void funcTryCatchWitFinally()
    {
        try
        {
            MessageBox.Show("this is try part");
        }
        catch (Exception e)
        {
            MessageBox.Show("this is catch part");
        }
        catch (Exception e)
        {
            MessageBox.Show("other catch");
        }
        finally
        {
            MessageBox.Show("finally part");
        }
    }

    void funcTryCatchWithoutFinally()
    {
        try
        {
            MessageBox.Show("this is try part");
        }
        catch (Exception e)
        {
            MessageBox.Show("this is catch part");
        }
        catch (Exception e)
        {
            MessageBox.Show("other catch");
        }
    }

    void funcGoto()
    {
        goto testGoto;

	testGoto:
	    MessageBox.Show("This is a test for goto");
    }

    void funcLambdaExpr() 
    {
	var squareFunc = x => x * x;
	var val = squareFunc(9);
    }

    void funcNamedArgumentExpression() 
    {
	namedArgumentExpression(height: 42, width: 42);
    }

    void funcQueryExpression() 
    {
	int[] scores = new int[] { 97, 92, 81, 60 };

        IEnumerable<int> scoreQuery =
            from score in scores
            where score > 80
            select score;

        foreach (int i in scoreQuery)
        {
            Console.Write(i + " ");
        }            
    }

    void functhis() {
	this.toto = "tata";
    }

    void funcYield()
    {
	    yield break;
    	yield return "test";
    }

    void CheckedFunc()
    {
        int x = 42;
        checked
        {
            MessageBox.Show("check the values of the int");
        }

        try
        {
            checked(x + 484151515);
        }
        catch (System.OverflowException e)
        {
            MessageBox.Show("there's an overflow in the block try");
        }
    }

    void UncheckedFunc()
    {
        int x = 42;
        unchecked
        {
            MessageBox.Show("uncheck the verification of the int in case of overflow");
        }

        unchecked(x + 484151515);
    }

    unsafe static void fixedFunc()
    {
        Point pt = new Point();

        fixed (int* p = &pt.x)
        {
            *p = 1;
        }        
    }
}
