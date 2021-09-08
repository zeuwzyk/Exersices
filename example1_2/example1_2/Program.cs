using System;

namespace example1_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter information: ");
            string information = Console.ReadLine();
            string[] words = information.Split(' ');
            //если размер вордс больше 3х работаем еще раз
            int numRows, numColumns;

            for (int i=0; i < words.Length; i++)//цикл не нужен работаем до 3х значений
            {
                if (int.TryParse(words[i], out numRows))
                    Console.WriteLine("int ", numRows);
            }

        }                
    }
}
