namespace TestAlignExpr
{
	class TestAlignExpr
	{
		void MyFunc()
		{
			a + b + 5 + 42 + 9 - 8 * 32 / 16;
			(a + (b == 2 ? 10 : 21) + 5 + 42 + 9 - 8 * 32 / 16) - 2 * a + b + 5 + 42 + 9 - 8 * 32 / 16 / (a + b + 5 + 42 + 9 - 8 * 32 / 16) * 42;
		}
	}
}