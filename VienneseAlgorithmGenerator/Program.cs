using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VienneseAlgorithmGenerator
{
    class Program
    {
        private static readonly Random rand = new Random();
        private const int MAX_ADD = 10;

        static void Main(string[] args)
        {
            Console.Write("Введите размерность квадратной матрицы [5]: ");
            string sizeStr = Console.ReadLine();
            int size = int.Parse(sizeStr == "" ? "5" : sizeStr);

            Console.Write("Введите имя файла для сохранения теста [test.txt]: ");
            string pathStr = Console.ReadLine();
            string filePath = pathStr == "" ? "test.txt" : pathStr;

            Point[][] matrix = new Point[size][];

            for (int i = 0; i < size; i++)
            {
                matrix[i] = new Point[size];
                for (int j = 0; j < size; j++)
                {
                    if (i == j)
                    {
                        matrix[i][j] = new Point(0, true);
                        continue;
                    }

                    matrix[i][j] = new Point(rand.Next(1, 7));
                }
            }

            for (int i = 0; i < rand.Next(5,17); i++)
            {
                if (rand.Next(0, 100) < 50)
                    ShuffleRows(matrix);
                if (rand.Next(0, 100) < 50)
                    ShuffleCols(matrix);
                if (rand.Next(0, 100) < 65)
                    AddToCols(matrix);
                if (rand.Next(0, 100) < 65)
                    AddToRows(matrix);
            }

            var problemWithAnswer = MatrixToStringWithAnswer(matrix);

            File.WriteAllText(filePath, problemWithAnswer.Item1 + "\n\n" + problemWithAnswer.Item2);
            Console.WriteLine("Файл успешно сохранен!");
        }

        static void ShuffleRows(Point[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                int j = rand.Next(0, i);
                Point[] a = matrix[i];
                matrix[i] = matrix[j];
                matrix[j] = a;
            }
        }

        static void ShuffleCols(Point[][] matrix)
        {
            int size = matrix.Length;
            int[] indexes = new int[size];
            
            for (int i = 0; i < size; i++)
            {
                indexes[i] = i;
            }

            for (int i = 0; i < size; i++)
            {
                int j = rand.Next(0, i);
                int a = indexes[i];
                indexes[i] = indexes[j];
                indexes[j] = a;
            }

            for (int i = 0; i < size; i++)
            {
                Point[] row = matrix[i];
                Point[] newRow = new Point[size];

                for (int j = 0; j < size; j++)
                {
                    newRow[j] = row[indexes[j]];
                }

                matrix[i] = newRow;
            }
        }

        static void AddToRows(Point[][] matrix)
        {
            int size = matrix.Length;

            for (int i = 0; i < size; i++)
            {
                int pp = rand.Next(0, MAX_ADD);
                for (int j = 0; j < size; j++)
                {
                    matrix[i][j].Val += pp;
                }
            }
        }

        static void AddToCols(Point[][] matrix)
        {
            int size = matrix.Length;

            int[] pps = new int[size];

            for (int i = 0; i < size; i++)
            {
                pps[i] = rand.Next(0, MAX_ADD);
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i][j].Val += pps[j];
                }
            }
        }

        static Tuple<string, string> MatrixToStringWithAnswer(Point[][] matrix)
        {
            int size = matrix.Length;
            string res = "";
            string answer = "";

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (matrix[i][j].WasZero)
                        answer += $"{i},{j}\n";
                    res += matrix[i][j] + ",";
                }
                res = res.TrimEnd(',');
                res += "\n";
            }

            return new Tuple<string, string>(res, answer);
        }
    }
}
