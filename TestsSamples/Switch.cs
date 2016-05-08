private class SwitchTest {
	
	void func()
	{
		switch (toto(titi(), true, 5) * 42)
		{
			case 0:
			case 1:
			case 2:
			break;
			case 3:
				toto();
				break;
			default:
		}
	}
}