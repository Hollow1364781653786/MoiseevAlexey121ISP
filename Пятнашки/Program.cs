Random rnd = new Random();
int[] sample_array = new int[4];
int highestCount = 1;
for (int i = 0; i < sample_array.Length; i++)
{
    Console.WriteLine("Идёт генерация массива...");
    sample_array[i] = rnd.Next(10);
    Console.Write(sample_array[i] + " ");
    for (int n = 0; n < i; n++)
    {
        if (sample_array[i] == sample_array[n]) highestCount++;
    }
    if (highestCount > 1)
    {
        highestCount = 1;
        i = 0;
        Console.WriteLine("Ошибка генерации массива. Повторная генерация...");
    }
}
Console.WriteLine(" ");
 for (int i = 0; i < sample_array.Length; i++)
{
    Console.Write(sample_array[i] + " ");
}
Console.WriteLine(" ");
Console.WriteLine("Введите первое число");
int firstInt = Convert.ToInt32(Console.ReadLine());
int a;
for (a = 0; a < sample_array.Length; a++)
{
    if (firstInt == sample_array[a])
    {
        firstInt = sample_array[a];
        break;
    }
    Console.WriteLine("Идёт поиск числа...");
}
Console.WriteLine("Введите второе число");
int secondInt = Convert.ToInt32(Console.ReadLine());
int b;
for (b = 0; b < sample_array.Length; b++)
{
    if (secondInt == sample_array[b])
    {
        secondInt = sample_array[b];
        break;
    }
    Console.WriteLine("Идёт поиск числа...");
}

sample_array[a] = secondInt;

sample_array[b] = firstInt;

for (int i = 0; i < sample_array.Length; i++)
{
    Console.Write(sample_array[i] + " ");
}