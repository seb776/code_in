namespace LoopsExample
{
    class Example
    {
        void Function()
        {
            int a;
            
            while (a < 42)
            {
                Console.WriteLine("Hello While !");
            }
            
            do
            {
                Console.WriteLine("Hello DoWhile !");
            } while (a < 42);
            
            for (int b = (a + 5); b > a; --b)
            {
                Console.WriteLine("Hello For !");
            }
            
            foreach (int c in Collection)
            {
                 Console.WriteLine("Hello ForEach !");
            }
        }
    }
}