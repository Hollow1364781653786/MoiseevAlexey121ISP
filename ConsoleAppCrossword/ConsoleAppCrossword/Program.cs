using System;
using System.Linq;
using System.Collections.Generic;

namespace Nonogram
{
    class Program
    {
        static void Main(string[] args)
        {

            int[,] finalGrid = {
                { 1, 0, 0, 0, 0, 0, 1 },
                { 0, 1, 0, 1, 0, 1, 0 },
                { 0, 0, 1, 1, 1, 0, 0 },
                { 0, 1, 1, 1, 1, 1, 0 },
                { 0, 0, 1, 1, 1, 0, 0 },
                { 0, 1, 0, 1, 0, 1, 0 },
                { 1, 0, 0, 0, 0, 0, 1 },
            };

            int[,] userGrid = {
                { 1, 0, 0, 0, 0, 0, 1 },
                { 0, 1, 0, 1, 0, 1, 0 },
                { 0, 0, 1, 1, 1, 0, 0 },
                { 0, 1, 1, 1, 1, 1, 0 },
                { 0, 0, 1, 1, 1, 0, 0 },
                { 0, 1, 0, 1, 0, 1, 0 },
                { 1, 0, 0, 0, 0, 0, 1 },
            };

            int[,] differenceGrid = {
                { 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0 },
            };

            // Сравнение и запись разницы в третий массив
            for (int i = 0; i < finalGrid.GetLength(0); i++)
            {
                for (int j = 0; j < finalGrid.GetLength(1); j++)
                {
                    if (userGrid[i, j] != finalGrid[i, j])
                    {
                        differenceGrid[i, j] = finalGrid[i, j];
                    }
                }
            }

        cycleStart:

            List<List<int>> rows = GetRowInstructions(differenceGrid);

            Console.WriteLine("Rows:");
            PrintInstructions(rows);

            Console.WriteLine();

            for (int i = 0; i < finalGrid.GetLength(1); i++)
            {
                for (int j = 0; j < finalGrid.GetLength(0); j++)
                {
                    Console.Write(userGrid[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            Console.WriteLine();

            // Вводим номер строки и столбца
            Console.Write("Введите номер строки, в которой хотите совершить замену: ");
            int row = int.Parse(Console.ReadLine());

            Console.Write("Введите номер столбца, в которой хотите совершить замену:  ");
            int column = int.Parse(Console.ReadLine());

            // Проверяем валидность введенных значений
            if (row - 1 < 0 || row - 1 >= userGrid.GetLength(0) || column - 1 < 0 || column - 1 >= userGrid.GetLength(1))
            {
                Console.WriteLine("Некорректные значения строки и столбца!");
                goto cycleStart;
            }

            // Меняем значение в выбранной клетке
            if (userGrid[row - 1, column - 1] == 0)
            {
                userGrid[row - 1, column - 1] = 1;
            }
            else userGrid[row - 1, column - 1] = 0;

            // Сравнение и запись разницы в третий массив
            for (int i = 0; i < finalGrid.GetLength(0); i++)
            {
                for (int j = 0; j < finalGrid.GetLength(1); j++)
                {
                    if (userGrid[i, j] != finalGrid[i, j])
                    {
                        differenceGrid[i, j] = finalGrid[i, j];
                    } else differenceGrid[i, j] = 0;
                }
            }

            bool winCondition = userGrid.Cast<int>().SequenceEqual(finalGrid.Cast<int>());

            Console.WriteLine(winCondition); // Выведет true

            if (winCondition)
            {
                Console.WriteLine("Вы победили и правильно ввели нонограм!");
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("...");
                goto cycleStart;
            }
        }

        static List<List<int>> GetRowInstructions(int[,] grid)
        {
            List<List<int>> rowInstructions = new List<List<int>>();
            int rowCount = grid.GetLength(0);
            int columnCount = grid.GetLength(1);

            for (int i = 0; i < rowCount; i++)
            {
                List<int> instructions = new List<int>();
                int count = 0;

                for (int j = 0; j < columnCount; j++)
                {
                    if (grid[i, j] == 1)
                    {
                        count++;
                    }
                    else if (count > 0)
                    {
                        instructions.Add(count);
                        count = 0;
                    }
                }

                if (count > 0)
                {
                    instructions.Add(count);
                }

                rowInstructions.Add(instructions);
            }

            return rowInstructions;
        }

        static void PrintInstructions(List<List<int>> instructions)
        {
            foreach (List<int> row in instructions)
            {
                foreach (int num in row)
                {
                    Console.Write(num + " ");
                }
                Console.WriteLine();
            }
        }
    }
}