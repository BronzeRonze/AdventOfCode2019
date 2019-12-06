using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Drawing;

namespace AdventCode19
{
    class WiresCrossed
    {
        public static void StartCrossedWires()
        {
            List<String> wirePath = new List<string>();
            List<String> wirePath2 = new List<string>();
            // Read wire paths to list
            using (StreamReader reader = File.OpenText("F:\\Programming\\CodeAdvent\\2019\\Day3InputBoth.txt"))
            {
                bool secondWire = false;
                string line = null;
                string[] values = { };

                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    values = line.Split(',');

                    for (int i = 0; i < values.Length; i++)
                    {
                        // Separate the direction from the distance.
                        if (secondWire)
                        {
                            wirePath2.Add(values[i]);
                        }
                        else
                        {
                            wirePath.Add(values[i]);
                        }
                    }
                    secondWire = true;
                }
            }
            List<Point> firstGrid = SetupGrid(wirePath);
            List<Point> secondGrid = SetupGrid(wirePath2);

            List<Point> cross = firstGrid.Intersect(secondGrid).ToList();

            List<int> distance = new List<int>();

            foreach(Point p in cross)
            {
                distance.Add(CalcPathToOrigin(0, p.X, 0, p.Y));
            }
            Console.WriteLine(distance.Min().ToString());

            List<int> totalSteps = new List<int>();

            int steps1 = 0;
            int steps2 = 0;
            int total = 0;
            foreach(Point p in cross)
            {
                steps1 = firstGrid.IndexOf(p) + 1;
                steps2 = secondGrid.IndexOf(p) + 1;
                total = steps1 + steps2;
                totalSteps.Add(total);
            }

            Console.WriteLine(totalSteps.Min().ToString());
        }
        
        static List<Point> SetupGrid(List<String> wires)
        {
            List<Point> grid = new List<Point>();
            Point last = new Point(0, 0);

            foreach(string coord in wires)
            {
                ProcessGrid(last, coord, grid);
                last = grid.Last();
            }

            return grid;
        }

        static void ProcessGrid(Point myPoint, string coord, List<Point> grid)
        {
            string direction = coord.Substring(0, 1);
            int distance = int.Parse(coord.Substring(1));

            switch(direction)
            {
                case "R":
                    for(int i = 1; i <= distance; i++)
                    {
                        grid.Add(new Point(myPoint.X + i, myPoint.Y));
                    }
                    break;
                case "D":
                    for (int i = 1; i <= distance; i++)
                    {
                        grid.Add(new Point(myPoint.X, myPoint.Y - i));
                    }
                    break;
                case "L":
                    for (int i = 1; i <= distance; i++)
                    {
                        grid.Add(new Point(myPoint.X - i, myPoint.Y));
                    }
                    break;
                case "U":
                    for (int i = 1; i <= distance; i++)
                    {
                        grid.Add(new Point(myPoint.X, myPoint.Y + i));
                    }
                    break;
            }

        }
        static int CalcPathToOrigin(int x1, int x2, int y1, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }
    }
}
