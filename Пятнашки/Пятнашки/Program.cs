using System;
Random rnd = new Random();
int[,] sample_array = new int[4, 4];
List<int> alreadyUsed = new List<int>();
int[,] winningState = {
{1, 2, 3, 4},
{5, 6, 7, 8},
{9, 10, 11, 12},
{13, 14, 15, 16}
};
for (int i = 0; i < sample_array.GetLength(0); i++)
{
    for (int j = 0; j < sample_array.GetLength(1); j++)
    {
        Console.WriteLine("Идёт генерация массива...");
        int tobeassigned = rnd.Next(0, 16);
        Console.Write(sample_array[i, j] + " ");
        while (alreadyUsed.Contains(tobeassigned))
        {
            tobeassigned = rnd.Next(1, 17);
        }
        sample_array[i, j] = tobeassigned;
        alreadyUsed.Add(tobeassigned);
    }
}
bool WinningCondition(int[,] board)
{
    for (int i = 0; i < sample_array.GetLength(0); i++)
    {
        for (int j = 0; j < sample_array.GetLength(1); j++)
        {
            if (board[i, j] != winningState[i, j])
            {
                return false;
            }
        }
    }
    return true;
}
Console.WriteLine(" ");
for (int i = 0; i < sample_array.GetLength(0); i++)
{
    for (int j = 0; j < sample_array.GetLength(1); j++)
    {
        Console.Write(sample_array[i, j] + " ");
    }
    Console.WriteLine();
}
Console.WriteLine(" ");
int ultrai = 0;
int maxMoves = 5;
for (ultrai = 0; ultrai < maxMoves; ultrai++)
{
    Console.WriteLine("Кол-во перемещений - " + ultrai);
    Console.WriteLine("Количество оставшихся перемещений - " + (maxMoves - ultrai));
    Console.WriteLine("Введите первое число");
    int zero = 0;
    int a;
    int h = 0;
    for (a = 0; a < sample_array.GetLength(0); a++)
    {
        for (h = 0; h < sample_array.GetLength(1); h++)
        {
            if (zero == sample_array[a, h])
            {
                zero = sample_array[a, h];
                goto hell;
            }
        }
    }
hell:
    Console.WriteLine("Куда вы хотите подвинуть ноль? Введите 'вверх', 'вниз', 'вправо', или 'влево'.");
    string direction = Console.ReadLine();
    int secondInt;
    switch (direction)
    {
        case "вверх":
            secondInt = sample_array[a - 1, h];
            sample_array[a - 1, h] = zero;
            sample_array[a, h] = secondInt;
        break;
        case "вниз":
            secondInt = sample_array[a + 1, h];
            sample_array[a + 1, h] = zero;
            sample_array[a, h] = secondInt;
        break;
        case "влево":
            secondInt = sample_array[a, h - 1];
            sample_array[a, h - 1] = zero;
            sample_array[a, h] = secondInt;
        break;
        case "вправо":
            secondInt = sample_array[a, h + 1];
            sample_array[a, h + 1] = zero;
            sample_array[a, h] = secondInt;
        break;
    }

    for (int i = 0; i < sample_array.GetLength(0); i++)
    {
        for (int j = 0; j < sample_array.GetLength(1); j++)
        {
            Console.Write(sample_array[i, j] + " ");
        }
        Console.WriteLine();
    }

    if (WinningCondition(sample_array))
    {
        goto TrueEnd;
    }

}
if (ultrai == maxMoves)
{
    Console.WriteLine("Ходы закончились. Вы проиграли.");
    Environment.Exit(0);
}
TrueEnd:;
Console.WriteLine("Вы победили!");