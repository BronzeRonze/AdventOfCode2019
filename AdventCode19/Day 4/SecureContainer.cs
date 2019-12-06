using System;
using System.Collections.Generic;
using System.Text;

namespace AdventCode19
{
    class SecureContainer
    {
        public static void StartSecuredContainer()
        {
            // Rules
            // Given rules (Rules that are already enforced by the range.)
            // 1) It is a six-digit number.
            // 2) The value is within the range given in your puzzle input.
            // Rules to implement (Rules that need to be enforced.)
            // 3) Two adjacent digits are the same (like 22 in 122345).
            // 4) Going from left to right, the digits never decrease; they only ever increase or stay the same (like 111123 or 135679).

            int rangeStart = 372037;
            int rangeEnd = 905157;

            List<int> validPasswords = new List<int>();

            for(int i = rangeStart; i <= rangeEnd; i++)
            {
                
                //if(CheckAdjacent(i)) // Puzzle 1 method
                if(CheckLargestAdjacent(i))
                {
                    if(CheckForDecrease(i))
                    {
                        validPasswords.Add(i);
                    }
                }
            }
            Console.WriteLine(validPasswords.Count);
        }

        static bool CheckAdjacent(int value)
        {
            bool result = false;

            string valueStr = value.ToString();

            char tempChar = 'Z';
            foreach(char c in valueStr)
            {
                if(tempChar == c)
                {
                    result = true;
                    break;
                }
                tempChar = c;
            }

            return result;
        }

        static bool CheckLargestAdjacent(int value)
        {
            bool result = false;

            string valueStr = value.ToString();

            char tempChar = 'Z';
            char matchedChar = 'Z';
            foreach (char c in valueStr)
            {
                if (tempChar == c)
                {
                    if(matchedChar == c)
                    {
                        result = false;
                    }
                    else
                    {
                        result = true;
                    }
                    matchedChar = c;
                }
                else
                {
                    if(result)
                    {
                        break;
                    }
                }
                tempChar = c;
            }

            return result;
        }

        static bool CheckForDecrease(int value)
        {
            bool result = true;

            string valueStr = value.ToString();

            char tempChar = '/';
            foreach(char c in valueStr)
            {
                if(tempChar > c)
                {
                    result = false;
                    break;
                }
                tempChar = c;
            }

            return result;
        }
    }
}
