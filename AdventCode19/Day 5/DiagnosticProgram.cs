using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;

namespace AdventCode19
{
    class DiagnosticProgram
    {
        public static void StartProgramSequence()
        {
            int[] numbers = { };
            int input = 0;
            using (StreamReader reader = new StreamReader("F:\\Programming\\CodeAdvent\\2019\\TestCases\\Day5Input.txt"))
            {
                numbers = reader.ReadLine().Split(',').Select(int.Parse).ToArray();
                Console.WriteLine("Input ID number.");
                input = int.Parse(Console.ReadLine());
            }

            ProcessProgramSequence(numbers, input);
        }

        static void ProcessProgramSequence(int[] program, int input)
        {
            int opCode = 0;
            int value1 = 0;
            int value2 = 0;
            int writeAddr = 0;
            int param1Mode = 0;
            int param2Mode = 0;
            int programIndex = 0;
            string commandStr = ""; 

            while(opCode != 99 || (programIndex > program.Length))
            {
                param1Mode = 0;
                param2Mode = 0;
                commandStr = program[programIndex++].ToString();

                switch(commandStr.Length)
                {
                    case 1:
                    case 2:
                        opCode = int.Parse(commandStr);
                        break;
                    case 3:
                        opCode = int.Parse(commandStr.Substring(1));
                        param1Mode = DetermineMode(commandStr, 0);
                        break;
                    case 4:
                        opCode = int.Parse(commandStr.Substring(2));
                        param1Mode = DetermineMode(commandStr, 1);
                        param2Mode = DetermineMode(commandStr, 0);
                        break;
                    case 5:
                        opCode = int.Parse(commandStr.Substring(3));
                        param1Mode = DetermineMode(commandStr, 2);
                        param2Mode = DetermineMode(commandStr, 1);
                        break;
                    default:
                        Console.WriteLine("Invalid Command Length.");
                        return;
                }
                if(param1Mode == -1 || param2Mode == -1)
                {
                    Console.WriteLine("Invalid parameter mode.");
                    return;
                }

                switch (opCode)
                {
                    // 1 = Addition. Take the next two indexes, add them together and save result to the index following these two. [1, 5, 6, 3] Add 5 and 6, save to 3.
                    case 1:
                        value1 = DetermineInstructionValue(program, programIndex++, param1Mode);
                        value2 = DetermineInstructionValue(program, programIndex++, param2Mode);
                        writeAddr = program[programIndex++];
                        program[writeAddr] = value1 + value2;
                        break;
                    // 2 = Multiplication Take the next two indexes, multiply them together and save result to the index following these two. [1, 5, 6, 3] Multiply 5 and 6, save to 3.
                    case 2:
                        value1 = DetermineInstructionValue(program, programIndex++, param1Mode);
                        value2 = DetermineInstructionValue(program, programIndex++, param2Mode);
                        writeAddr = program[programIndex++];
                        program[writeAddr] = value1 * value2;
                        break;
                    // 3 = Write a user input into address of parameter. [3, 50] (input: 1) Save 1 to address 50.
                    case 3:
                        writeAddr = program[programIndex++];
                        program[writeAddr] = input;
                        break;
                    // 4 = Output value of parameter address. [4, 50]. Output the value at address 50 to console.
                    case 4:
                        writeAddr = DetermineInstructionValue(program, programIndex++, param1Mode);
                        Console.WriteLine(writeAddr);
                        break;
                    // 5 = jump-if-true: if the first parameter is non-zero, it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
                    case 5:
                        value1 = DetermineInstructionValue(program, programIndex++, param1Mode);
                        value2 = DetermineInstructionValue(program, programIndex++, param2Mode);
                        if(value1 != 0)
                        {
                            programIndex = value2;
                        }
                        break;
                    // 6 = jump-if-false: if the first parameter is zero, it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
                    case 6:
                        value1 = DetermineInstructionValue(program, programIndex++, param1Mode);
                        value2 = DetermineInstructionValue(program, programIndex++, param2Mode);
                        if (value1 == 0)
                        {
                            programIndex = value2;
                        }
                        break;
                    // 7 = less than: if the first parameter is less than the second parameter, it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
                    case 7:
                        value1 = DetermineInstructionValue(program, programIndex++, param1Mode);
                        value2 = DetermineInstructionValue(program, programIndex++, param2Mode);
                        writeAddr = program[programIndex++];
                        if (value1 < value2)
                        {
                            program[writeAddr] = 1;
                        }
                        else
                        {
                            program[writeAddr] = 0;
                        }
                        break;
                    // 8 = equals: if the first parameter is equal to the second parameter, it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
                    case 8:
                        value1 = DetermineInstructionValue(program, programIndex++, param1Mode);
                        value2 = DetermineInstructionValue(program, programIndex++, param2Mode);
                        writeAddr = program[programIndex++];
                        if(value1 == value2)
                        {
                            program[writeAddr] = 1;
                        }
                        else
                        {
                            program[writeAddr] = 0;
                        }
                        break;               
                    // End of program!
                    case 99:
                        Console.WriteLine("DONE!");
                        break;
                    default:
                        // Error! non-valid opcode!
                        Console.WriteLine("Error! Non-Valid Op-Code!");
                        return;

                }

            }

        }

        static int DetermineInstructionValue(int[] program, int index, int Mode)
        {
            int value = 0;
            int readAddr = 0;
            switch(Mode)
            {
                case 0:
                    readAddr = program[index];
                    value = program[readAddr];
                    break;
                case 1:
                    value = program[index];
                    break;
                deafult:
                    break;   
            }

            return value;
        }

        static int DetermineMode(string command, int index)
        {
            int commandMode = 0;
            if(command[index] == '0')
            {
                commandMode = 0;
            }
            else if(command[index] == '1')
            {
                commandMode = 1;
            }
            else
            {
                commandMode = -1;
            }
            return commandMode;
        }

        public static void StartProgramSequence2()
        {
            using (StreamReader reader = new StreamReader("F:\\Programming\\CodeAdvent\\2019\\TestCases\\Day5Input.txt"))
            {
                string line = null;
                int[] numbers = { };
                int resultIndex = 0;

                line = reader.ReadLine();
                string[] values = line.Split(',');
                numbers = new int[values.Length];

                ProcessInputFile(reader, numbers);
                ProcessProgramSequence2(numbers, 12, 2, resultIndex);

                // We need this output in order to completegravity assist.
                int gravityResultReq = 19690720;
                int gravitySeqResult = 0;
                int answer = 0;
                for (int inputAddr1 = 0; inputAddr1 < 100; inputAddr1++)
                {
                    for (int inputAddr2 = 0; inputAddr2 < 100; inputAddr2++)
                    {
                        ProcessInputFile(reader, numbers);
                        gravitySeqResult = ProcessProgramSequence2(numbers, inputAddr1, inputAddr2, resultIndex);
                        if (gravityResultReq == gravitySeqResult)
                        {
                            answer = 100 * inputAddr1 + inputAddr2;
                            Console.WriteLine(answer);
                            return;
                        }
                    }
                }

            }
        }

        static int ProcessProgramSequence2(int[] program, int ManualInputPos1, int ManualInputPos2, int desiredIndex)
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
                    // 3 = 
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
