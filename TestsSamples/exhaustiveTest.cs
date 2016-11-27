using System;

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
            case 42:
                MessageBox.Show("Excellent !");
                break;
            case  12:
                MessageBox.Show("12...");
                break;
            default:
                MessageBox.Show("Not 42 and not 12");
                break;
        }
    }

    void funcLoop() {
        int coundWhile = 42;
        while (coundWhile > 0) {
            MessageBox.Show("While loop");
            MessageBox.Show("Still not negative: value is " + coundWhile.ToString());
            --coundWhile;
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

    void funcTryCatch()
    {
        try
        {
            MessageBox.Show("this is try part");
        }
        catch (Exception e)
        {
            MessageBox.Show("this is catch part");
        }
    }
}
