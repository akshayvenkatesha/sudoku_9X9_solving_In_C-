using System;
using System.IO;

namespace sudoku
{
    public class Program
    {
        class Number
        {
            public int Value { get; set; }
            public bool IsConstent { get; set; }

        }

        private readonly Number[,] _number = new Number[MaxNumber, MaxNumber];

        static void Main(string[] args)
        {
            var a = new Program();
            a.ReadInputFromFile();
            a.InsertNumbers();
        }

        public const int MaxNumber = 9;
        public readonly int RootMaxNumber = (int) Math.Sqrt(MaxNumber);

        public void InsertNumbers()
        {
            int i = 0, j = 0;
            while (i > -1)
            {
                int n = 1;
                j = 0;
                while (n > 0 && n <= MaxNumber)
                {
                    while (j > -1 && j < MaxNumber)
                    {
                        if (n > MaxNumber) break;
                        if (Place(i, j, n))
                        {

                            _number[i, j].Value = n;
                            n++;
                            j = 0;
                        }
                        else
                        {
                            j++;
                        }
                    }
                    if (n == MaxNumber + 1)
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
                                _number[i, j].Value = 0;
                                j++;
                            }
                            i--;
                            n = MaxNumber;
                        }
                    top:

                        j = GetIndexofN(i, n);
                        if (j != -2)
                        {
                            _number[i, j].Value = 0;
                            j++;
                        }
                        else
                        {
                            if (n == 1)
                            {
                                j = GetIndexofN(i, n);
                                if (j != -2)
                                {
                                    _number[i, j].Value = 0;
                                    j++;
                                }
                                n = MaxNumber;
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
                Console.WriteLine("Something is Wrong please see the input values are proper");
                Environment.Exit(1);
            }
            for (int j = 0; j < MaxNumber; j++)
            {

                if (_number[i, j].Value == n && _number[i, j].IsConstent)
                {
                    return -2;
                }
                if (_number[i, j].Value == n && !_number[i, j].IsConstent)
                {
                    return j;
                }
            }
            return -1;
        }

        private bool Place(int i, int j, int n)
        {
            if (i >= MaxNumber)
            {
                PrintAll();
                Environment.Exit(0);
            }
            if (_number[i, j].IsConstent && _number[i, j].Value == n)
            {
                return true;
            }

            for (int k = 0; k < MaxNumber; k++)
            {
                if (_number[i, j].Value != 0 || (_number[i, k].Value == n || _number[k, j].Value == n) || !Check3X3(i, j, n))
                {
                    return false;
                }
            }
            return true;
        }

        private void PrintAll()
        {
            string temp = string.Empty;
            for (int k = 0; k < MaxNumber; k++)
            {
                for (int l = 0; l < MaxNumber; l++)
                {
                    temp += _number[k, l].Value + ",";
                }
                temp += "\n";
            }
            Console.WriteLine(temp);
            Console.WriteLine();
        }

        private bool Check3X3(int i, int j, int n)
        {
            for (var k = (i / RootMaxNumber) * RootMaxNumber; k < ((i + RootMaxNumber) / RootMaxNumber) * RootMaxNumber; k++)
            {
                for (var l = (j / RootMaxNumber) * RootMaxNumber; l < ((j + RootMaxNumber) / RootMaxNumber) * RootMaxNumber; l++)
                {
                    if (n == _number[k, l].Value)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void ReadInputFromFile(string filename=@"e:\sudoku.txt")
        {
            if (!File.Exists(filename)) throw new FileNotFoundException(filename);
            var readAllLines = File.ReadAllLines(filename);

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
                    _number[i, j] = new Number()
                    {
                        Value = value,
                        IsConstent = constent
                    };
                }
            }
        }
    }
}
