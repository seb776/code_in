class QueryMethod
{
    static void Main()
    {
        int[] numbers = { 5, 10, 8, 3, 6, 12};

        //Query syntax:
        IEnumerable<int> numQuery1 = 
            from num in numbers
            where num % 2 == 0
            orderby num
            select num;
    }
}