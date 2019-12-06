using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;

namespace AdventCode19
{
    class CrossedWires
    {
        public static void StartCrossedWires()
        {
            int[] numbers = { };
            string[] direction = { };
            int[] numbers2 = { };
            string[] direction2 = { };
            // Read in two wire inputs.
            using (StreamReader reader = File.OpenText("F:\\Programming\\CodeAdvent\\2019\\Day3InputBoth.txt"))
            {
                bool secondWire = false;
                string line = null;
                string[] values = { };

                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    if (secondWire)
                    {
                        values = line.Split(',');
                        numbers2 = new int[values.Length];
                        direction2 = new string[values.Length];
                    }
                    else
                    {
                        values = line.Split(',');
                        numbers = new int[values.Length];
                        direction = new string[values.Length];
                    }
                    for (int i = 0; i < values.Length; i++)
                    {
                        // Separate the direction from the distance.
                        if (secondWire)
                        {
                            numbers2[i] = int.Parse(values[i].Substring(1));
                            direction2[i] = values[i].Substring(0, 1);
                        }
                        else
                        {
                            numbers[i] = int.Parse(values[i].Substring(1));
                            direction[i] = values[i].Substring(0, 1);
                        }
                    }
                    secondWire = true;
                }
            }
            // Plot out wire paths in a 2d array using direction and distance.
            // Fill in 2d array with 0s. 1 = wire, 2 = cross.
            // Calculate the shortest distance from origin point.
            int[,] grid = new int[25000, 25000];
            SetupGrid(grid, numbers, direction);
            SetupGrid(grid, numbers2, direction2);
        }

        static void SetupGrid(int[,] myGrid, int[] distance, string[] direction)
        {
            int size = distance.Length;
            int xPos = 12500;
            int yPos = 12500;
            int tempClosest = 999999;
            int closest = 999999;
            for(int i = 0; i < size; i++)
            {
                // Grid[Horizontal][Vertical]
                switch(direction[i])
                {
                    case "R":
                        for(int j = xPos; j <= (xPos + distance[i]); j++)
                        {
                            if (myGrid[j, yPos] == 1)
                            {
                                myGrid[j, yPos] = 2;
                                if (j != 12500 && yPos != 12500)
                                {
                                    tempClosest = CalcPathToOrigin(j, yPos);
                                }
                            }
                            else
                            {
                                myGrid[j, yPos] = 1;
                            }
                        }
                        xPos += distance[i];
                        break;
                    case "D":
                        for (int j = yPos; j >= (yPos - distance[i]); j--)
                        {
                            if (myGrid[xPos, j] == 1)
                            {
                                myGrid[xPos, j] = 2;
                                if (j != 12500 && xPos != 12500)
                                {
                                    tempClosest = CalcPathToOrigin(xPos, j);
                                }
                            }
                            else
                            {
                                myGrid[xPos, j] = 1;
                            }
                        }
                        yPos -= distance[i];
                        break;
                    case "L":
                        for (int j = xPos; j >= (xPos - distance[i]); j--)
                        {
                            if (myGrid[j, yPos] == 1)
                            {
                                myGrid[j, yPos] = 2;
                                if (j != 12500 && yPos != 12500)
                                {
                                    tempClosest = CalcPathToOrigin(j, yPos);
                                }
                            }
                            else
                            {
                                myGrid[j, yPos] = 1;
                            }
                        }
                        xPos -= distance[i];
                        break;
                    case "U":
                        for (int j = yPos; j <= (yPos + distance[i]); j++)
                        {
                            if (myGrid[xPos, j] == 1)
                            {
                                myGrid[xPos, j] = 2;
                                if (j != 12500 && xPos != 12500)
                                {
                                    tempClosest = CalcPathToOrigin(xPos, j);
                                }
                            }
                            else
                            {
                                myGrid[xPos, j] = 1;
                            }
                        }
                        yPos += distance[i];
                        break;
                    default:
                        break;
                }
                if (tempClosest < closest)
                {
                    closest = tempClosest;
                }
            }
            Console.WriteLine(closest);
        }

        static int CalcPathToOrigin(int xPos, int yPos)
        {
            return (Math.Abs(12500 - xPos) + Math.Abs(12500 - yPos));
        }
    }
}
