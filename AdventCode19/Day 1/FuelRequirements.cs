using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;

namespace AdventCode19
{
    class FuelRequirements
    {
        public static void CalculateFuelReqDay1()
        {
            int sum = 0;
            int mass = 0;
            using (StreamReader reader = File.OpenText("F:\\Programming\\CodeAdvent\\2019\\Day1Input.txt"))
            {
                while (!reader.EndOfStream)
                {
                    mass = int.Parse(reader.ReadLine());
                    sum += CalculateTotalFuel(mass);
                }
            }
            Console.WriteLine(sum);
        }

        static int CalculateTotalFuel(int mass)
        {
            int reqFuel = 0;
            while (mass > 8)
            {
                mass = (mass / 3) - 2;
                reqFuel += mass;
            }
            return reqFuel;
        }

    }
}
