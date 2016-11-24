using System;

public class exhaustiveTest
{
    public exhaustiveTest() : heritage1, heritage2
	{
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
        }

        void funcCondition(int x, int y) {
            if ((x == y) || (x < y)) {
                funcAssign();
                MessageBox.Show("This function assign does not return anything");
                printDay(x);
            }
            else {
                MessageBox.Show("let's do a recursive");
                funcCondition(x - 1, y);
            }
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
	}
}
