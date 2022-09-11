using System;

namespace Decimal_to_Floating_Point_Converter 
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //Console.WriteLine(FractionToFixedPoint(.1f, 20));
            //Console.WriteLine(IntegerToFixedPoint(255));
            //Console.WriteLine(FlippedAddedOne("0101"));

            while (true) //added loop to be able to test more numbers
            {
                Console.WriteLine("This program ouputs a floating point binary number from a decimal\ntype the decimal below");
                float decimalInput = Single.Parse(Console.ReadLine());
                string[] twosAndPoint = DecimalToFixedPoint(decimalInput);
                Console.WriteLine(FloatingPoint(twosAndPoint[0], twosAndPoint[1]) + "\n");
            }
            


        }

        static string FractionToFixedPoint(float dFraction, int maxBits = 100) //multiply by 2 method
        {
            if (dFraction >= 1) {return "invalid argument";} //if "fraction" is greater or equal to one stop function

            string binary = "";

            float currF = dFraction;
            float doubledF;
            char firstV;

            int iters = 0;

            do
            {
                iters += 1;

                doubledF = currF * 2; //double fraction

                firstV = doubledF.ToString()[0];
                binary += firstV; //append digit before decimal point to final string

                currF = firstV == '1' ? doubledF - 1 : doubledF;  
                //make the current fraction equal to 1 less than the doubled if it is >= 1, else just the same as the doubled fraction                        

            } while (currF != 0 && currF != dFraction && iters < maxBits);
            //continue whilst within chosen bit length and current fraction is not same as original fraction and is not 0


            return binary;
        
        }

        static string IntegerToFixedPoint(int integer) //halve by 2 method
        {
            string binary = "";
            int remainder;
            float curNum = integer;

            while(curNum != 0)
            {

                remainder =  (int)(curNum % 2); //convert float modulus result to integer
                binary += remainder.ToString();
                curNum = (float)Math.Floor(curNum / 2); //floor result to result with integer
            }

            return Reverse(binary); //reverse order of binary digits
        }

        static string[] DecimalToFixedPoint(float oDecimal)
        {
            string binary = "";

            string binaryInt;
            string binaryFrac;
            string printedV = "";

            float absDecimal = Math.Abs(oDecimal);

            int integerP = (int)(absDecimal - (absDecimal % 1));
            float fractionalP = absDecimal % 1;

            binaryInt = IntegerToFixedPoint(integerP);
            binaryFrac = FractionToFixedPoint(fractionalP);
            
            string decimalPos = binaryInt.Length.ToString();


            if (oDecimal >= 0) // positive case
            {
                
                printedV = binaryFrac != "0" ? binaryInt + binaryFrac : binaryInt; //fixing displayed (would show 0 on end when fraction is 0)
                printedV = printedV[0] == '1' ? Reverse(Reverse(printedV) + '0') : printedV; // leading 0 for 2s compl, is there ever not a leading 1? - decimal pos
                
            }

            else if (oDecimal < 0) // negative cae
            {

                string combinedS = binaryFrac != "0" ? binaryInt + binaryFrac : binaryInt;             
                string twosComplement = FlippedAddedOne(combinedS);
                printedV = twosComplement;
               
            }
                      
            return new string[] {printedV, decimalPos};
        }

        static string FlippedAddedOne(string bits)
        {

            bits = bits[0] == '1' ? Reverse(Reverse(bits) + '0') : bits; // add leading 0 for 2's complement if first digit is 1

            bool reachedOne = false;
            string flippedStringRL = "";

            for (int i = bits.Length-1; i >= 0; i--) //from right to left
            {

                flippedStringRL += bits[i] == '1' && reachedOne == false ? '1' : ""; //cases of whether 1 has been met yet and what to do if so (or not)
                flippedStringRL += bits[i] == '0' && reachedOne == false ? '0' : "";
                flippedStringRL += bits[i] == '1' && reachedOne == true ? '0' : "";
                flippedStringRL += bits[i] == '0' && reachedOne == true ? '1' : "";

                
                if (bits[i] == '1') { reachedOne = true; }
            }

            
            return Reverse(flippedStringRL);
        }

        static string FloatingPoint(string twosCompl, string decimalP)
        {
            string floatingPoint = "";

            string binaryDec = DecimalToFixedPoint(Int32.Parse(decimalP))[0];

            floatingPoint = twosCompl + " " +  binaryDec;
            //Console.WriteLine($"exponent is after {decimalP + 1} digit(s)");

            return floatingPoint;
        }

        static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }





    }
}