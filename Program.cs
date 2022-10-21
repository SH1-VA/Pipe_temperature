using System.Globalization;

internal class Program
{
    private static void Main(string[] args)
    {
        //Создаём массив узлов сетки (по умолчанию заполняется нулями)
        double [,] temperature = new double[61, 61];

        //Задаём граничные условия
        //Для днища температуры уже заданы нулями по умолчанию
        for (int j = 31; j < 61; j++)
        {
            //Задаём возрастание температуры выше середины для внешней стенки так, чтобы оно плавно возрастало и наверху уже было равно 100 градусам
            //Левая стенка
            temperature[0, j] = 100.0 / 30 * (j - 30);
            //Правая стенка
            temperature[60, j] = 100.0 / 30 * (j - 30);
        }
        //Задаём температуру верхней части
        for (int i = 0; i < 61; i++)
        {
            temperature[i, 60] = 100;
        }
        //Задаём температуру воды внутри
        for(int i = 18; i <= 42; i++)
        {
            for(int j = 18; j <= 42; j++)
            {
                temperature[i, j] = 200;
            }
        }

        //Выводим значения для проверки
        //for (int j = 60; j >= 0; j--)
        //{
        //    for (int i = 0; i <= 60; i++)
        //    {
        //        var temp = string.Format("{0:f0}", temperature[i, j]);
        //        Console.Write($"{temp,3}", temp);
        //    }
        //    Console.WriteLine();
        //}
        //Console.WriteLine();

        //Теперь обсчитываем сечение
        for (int i = 1; i <= 59; i++)
        {
            if (i >= 18 && i <= 42)
            {
                for (int j = 1; j <= 17; j++)
                {
                    temperature[i, j] = 1.0 / 4 * (temperature[i - 1, j] + temperature[i, j - 1] + temperature[i + 1, j] + temperature[i, j + 1]);
                }
                for (int j = 43; j < 59; j++)
                {
                    temperature[i, j] = 1.0 / 4 * (temperature[i - 1, j] + temperature[i, j - 1] + temperature[i + 1, j] + temperature[i, j + 1]);
                }
            }
            else
            {
                for (int j = 1; j <= 59; j++)
                {
                    temperature[i, j] = 1.0 / 4 * (temperature[i - 1, j] + temperature[i, j - 1] + temperature[i + 1, j] + temperature[i, j + 1]);
                }
            }
        }

        //Выводим значения
        for (int j = 60; j >= 0; j--)
        {
            for (int i = 0; i <= 60; i++)
            {
                var temp = string.Format("{0:f0}", temperature[i, j]);
                Console.Write($"{temp, 3}", temp);
            }
            Console.WriteLine();
        }
    }
}