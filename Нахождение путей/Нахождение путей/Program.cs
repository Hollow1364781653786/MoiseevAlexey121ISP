using System;
Random rnd = new Random();
int[,] sample_array = new int[6, 6];
List<int> alreadyUsed = new List<int>();
Console.WriteLine("Идёт генерация массива...");
for (int i = 0; i < sample_array.GetLength(0); i++)
{
    for (int j = 0; j < sample_array.GetLength(1); j++)
    {
        int tobeassigned = rnd.Next(100, 1000);
        while (alreadyUsed.Contains(tobeassigned))
        {
            tobeassigned = rnd.Next(1, 17);
        }
        sample_array[i, j] = tobeassigned;
        alreadyUsed.Add(tobeassigned);
    }
}

Console.WriteLine(" ");
for (int a = 0; a < sample_array.GetLength(0); a++)
{
    for (int j = 0; j < sample_array.GetLength(1); j++)
    {
        Console.Write(sample_array[a, j] + " ");
    }
    Console.WriteLine();
}
int sample_rows = sample_array.GetLength(0);
int sample_cols = sample_array.GetLength(1);
int[,] allWays = new int[sample_rows, sample_cols];

Console.WriteLine(" ");

for (int i = 0; i < sample_rows; i++)
{
    allWays[i, 0] = 1;
}
for (int j = 0; j < sample_cols; j++)
{
    allWays[0, j] = 1;
}

for (int i = 1; i < sample_rows; i++)
{
    for (int j = 1; j < sample_cols; j++)
    {
        allWays[i, j] = allWays[i - 1, j] + allWays[i, j - 1];
    }
}
for (int a = 0; a < allWays.GetLength(0); a++)
{
    for (int j = 0; j < allWays.GetLength(1); j++)
    {
        Console.Write(allWays[a, j] + " ");
    }
    Console.WriteLine();
}
Console.WriteLine("Количество возможных путей: " + allWays[sample_rows - 1, sample_cols - 1]);