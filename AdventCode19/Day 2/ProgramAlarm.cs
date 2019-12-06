using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;

namespace AdventCode19
{
    class ProgramAlarm
    {
        public static void StartProgramSequence()
        {
            using (StreamReader reader = new StreamReader("F:\\Programming\\CodeAdvent\\2019\\Day2Input.txt"))
            {
                string line = null;
                int[] numbers = { };
                int input1Fix = 12;
                int input2Fix = 2;
                int resultIndex = 0;

                line = reader.ReadLine();
                string[] values = line.Split(',');
                numbers = new int[values.Length];

                ProcessInputFile(reader, numbers);
                ProcessProgramSequence(numbers, input1Fix, input2Fix, resultIndex);

                // We need this output in order to completegravity assist.
                int gravityResultReq = 19690720;
                int gravitySeqResult = 0;
                int answer = 0;
                for(int inputAddr1 = 0; inputAddr1 < 100; inputAddr1++)
                {
                    for(int inputAddr2 = 0; inputAddr2 < 100; inputAddr2++)
                    {
                        ProcessInputFile(reader, numbers);
                        gravitySeqResult = ProcessProgramSequence(numbers, inputAddr1, inputAddr2, resultIndex);
                        if(gravityResultReq == gravitySeqResult)
                        {
                            answer = 100 * inputAddr1 + inputAddr2;
                            Console.WriteLine(answer);
                            return;
                        }
                    }
                }

            }
        }

        static int ProcessProgramSequence(int[] program, int ManualInputPos1, int ManualInputPos2, int desiredIndex)
        {
            int opCode = 0;
            int programIndex = 0;
            int index1 = 0;
            int index2 = 0;
            int index3 = 0;
            int result = 0;

            program[1] = ManualInputPos1;
            program[2] = ManualInputPos2;

            while (opCode != 99 || (programIndex > program.Length))
            {
                opCode = program[programIndex++];

                // Process message based on opCode
                switch (opCode)
                {
                    // 1 = Addition. Take the next two indexes, add them together and save result to the index following these two. [1, 5, 6, 3] Add 5 and 6, save to 3.
                    case 1:
                        index1 = program[programIndex++];
                        index2 = program[programIndex++];
                        index3 = program[programIndex++];
                        result = program[index1] + program[index2];
                        program[index3] = result;
                        break;
                    // 2 = Multiplication Take the next two indexes, multiply them together and save result to the index following these two. [1, 5, 6, 3] Multiply 5 and 6, save to 3.
                    case 2:
                        index1 = program[programIndex++];
                        index2 = program[programIndex++];
                        index3 = program[programIndex++];
                        result = program[index1] * program[index2];
                        program[index3] = result;
                        break;
                    // End of program!
                    case 99:
                        Console.WriteLine(program[desiredIndex]);
                        break;
                    default:
                        // Error! non-valid opcode!
                        Console.WriteLine("Error! Non-Valid Op-Code!");
                        return -1;

                }
            }
            return program[desiredIndex];

        }

        static void ProcessInputFile(StreamReader reader, int[] numbers)
        {
            string line = null;
            reader.DiscardBufferedData();
            reader.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);

            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                string[] values = line.Split(',');
                for (int i = 0; i < values.Length; i++)
                {
                    numbers[i] = int.Parse(values[i]);
                }
            }
        }
    }
}
