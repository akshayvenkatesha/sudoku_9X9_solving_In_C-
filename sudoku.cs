using System;
using System.IO;

namespace sudoku
{
    class Program
    {
        class Number
        {
            public int Value { get; set; }
            public bool IsConstent { get; set; }

        }

        private Number[,] number = new Number[9, 9];

        static void Main(string[] args)
        {
            var a = new Program();
            a.ReadInputFromFile();
            a.InsertNumbers();
        }

        private void InsertNumbers()
        {
            int i = 0, j = 0;
            while (i > -1)
            {
                int n = 1;
                j = 0;
                while (n > 0 && n <= 9)
                {
                    while (j > -1 && j < 9)
                    {
                        if (n > 9) break;
                        if (Place(i, j, n))
                        {

                            number[i, j].Value = n;
                            n++;
                            //PrintAll();
                            j = 0;
                        }
                        else
                        {
                            j++;
                        }
                    }
                    if (n == 10)
                    {
                        i++;
                        n = 1;
                        j = 0;
                    }
                    else
                    {
                        if (n > 1)
                        {
                            n--;
                        }
                        else
                        {
                            j = GetIndexofN(i, n);
                            if (j != -2 && j != -1)
                            {
                                number[i, j].Value = 0;
                                j++;
                            }
                            i--;
                            n = 9;
                        }
                    top:

                        j = GetIndexofN(i, n);
                        if (j != -2)
                        {
                            number[i, j].Value = 0;
                            j++;
                        }
                        else
                        {
                            if (n == 1)
                            {
                                j = GetIndexofN(i, n);
                                if (j != -2)
                                {
                                    number[i, j].Value = 0;
                                    j++;
                                }
                                n = 9;
                                i--;
                            }
                            else
                            { n--; }
                            goto top;
                        }
                    }
                }
            }
        }

        private int GetIndexofN(int i, int n)
        {
            if (i < 0)
            {
                PrintAll();
                Environment.Exit(1);
            }
            if (n == 0)
            {
                Console.WriteLine("number is 0");
                Environment.Exit(2);
            }
            for (int j = 0; j < 9; j++)
            {

                if (number[i, j].Value == n && number[i, j].IsConstent)
                {
                    return -2;
                }
                if (number[i, j].Value == n && !number[i, j].IsConstent)
                {
                    return j;
                }
            }
            return -1;
        }

        private bool Place(int i, int j, int n)
        {
            if (i >= 9)
            {
                PrintAll();
                Environment.Exit(0);
            }
            if (number[i, j].IsConstent && number[i, j].Value == n)
            {
                return true;
            }

            for (int k = 0; k < 9; k++)
            {
                if (number[i, j].Value != 0 || (number[i, k].Value == n || number[k, j].Value == n) || !Check3X3(i, j, n))
                {
                    return false;
                }
            }
            return true;
        }

        private void PrintAll()
        {
            string temp = string.Empty;
            for (int k = 0; k < 9; k++)
            {
                for (int l = 0; l < 9; l++)
                {
                    temp += number[k, l].Value + ",";
                }
                temp += "\n";
            }
            Console.WriteLine(temp);
            Console.WriteLine();
        }

        private bool Check3X3(int i, int j, int n)
        {
            for (var k = (i / 3) * 3; k < ((i + 3) / 3) * 3; k++)
            {
                for (var l = (j / 3) * 3; l < ((j + 3) / 3) * 3; l++)
                {
                    if (n == number[k, l].Value)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void ReadInputFromFile()
        {
            var readAllLines = File.ReadAllLines(@"e:\sudoku.txt");

            for (var i = 0; i < readAllLines.Length; i++)
            {
                var line = readAllLines[i].Split(',');
                for (var j = 0; j < line.Length; j++)
                {
                    int value;
                    var constent = false;
                    int.TryParse(line[j], out value);
                    if (value != 0)
                    {
                        constent = true;
                    }
                    number[i, j] = new Number()
                    {
                        Value = value,
                        IsConstent = constent
                    };
                }
            }
        }
    }
}
