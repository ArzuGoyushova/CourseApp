using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Service.Utilities.Helper
{
    public class TablePrinter
    {
        private readonly string[] titles;
        private readonly List<int> lengths;
        private readonly List<string[]> rows = new List<string[]>();

        public TablePrinter(params string[] titles)
        {
            this.titles = titles;
            lengths = titles.Select(t => t.Length).ToList();
        }

        public void AddRow(params object[] row)
        {
            if (row.Length != titles.Length)
            {
                throw new Exception($"Added row length [{row.Length}] is not equal to title row length [{titles.Length}]");
            }
            rows.Add(row.Select(o => o.ToString()).ToArray());
            for (int i = 0; i < titles.Length; i++)
            {
                if (rows.Last()[i].Length > lengths[i])
                {
                    lengths[i] = rows.Last()[i].Length;
                }
            }
        }

        public void Print()
        {
            PrintSeparator();
            PrintRow(titles);
            PrintSeparator();

            foreach (var row in rows)
            {
                PrintRow(row);
            }

            PrintSeparator();
        }

        private void PrintRow(string[] row)
        {
            string line = "";
            for (int i = 0; i < row.Length; i++)
            {
                if (int.TryParse(row[i], out int n))
                {
                    line += "| " + row[i].PadLeft(lengths[i]) + ' ';
                }
                else
                {
                    line += "| " + row[i].PadRight(lengths[i]) + ' ';
                }
            }
            Console.WriteLine(line + "|");
        }

        private void PrintSeparator()
        {
            lengths.ForEach(l => Console.Write("+-" + new string('-', l) + '-'));
            Console.WriteLine("+");
        }

        public static void PrintSideBySide(TablePrinter left, TablePrinter right)
        {
            int maxRows = Math.Max(left.rows.Count, right.rows.Count);

            PrintCombinedSeparator(left.lengths, right.lengths);
            PrintCombinedRow(left.titles, right.titles, left.lengths, right.lengths);
            PrintCombinedSeparator(left.lengths, right.lengths);

            for (int i = 0; i < maxRows; i++)
            {
                string[] leftRow = i < left.rows.Count ? left.rows[i] : new string[left.titles.Length];
                string[] rightRow = i < right.rows.Count ? right.rows[i] : new string[right.titles.Length];

                PrintCombinedRow(leftRow, rightRow, left.lengths, right.lengths);
            }

            PrintCombinedSeparator(left.lengths, right.lengths);
        }

        private static void PrintCombinedRow(string[] leftRow, string[] rightRow, List<int> leftLengths, List<int> rightLengths)
        {
            string line = "";

            for (int i = 0; i < leftRow.Length; i++)
            {
                if (leftRow[i] == null) leftRow[i] = string.Empty;
                if (int.TryParse(leftRow[i], out int n))
                {
                    line += "| " + leftRow[i].PadLeft(leftLengths[i]) + ' ';
                }
                else
                {
                    line += "| " + leftRow[i].PadRight(leftLengths[i]) + ' ';
                }
            }
            line += "| ";

            for (int i = 0; i < rightRow.Length; i++)
            {
                if (rightRow[i] == null) rightRow[i] = string.Empty;
                if (int.TryParse(rightRow[i], out int n))
                {
                    line += "| " + rightRow[i].PadLeft(rightLengths[i]) + ' ';
                }
                else
                {
                    line += "| " + rightRow[i].PadRight(rightLengths[i]) + ' ';
                }
            }
            line += "|";

            Console.WriteLine(line);
        }

        private static void PrintCombinedSeparator(List<int> leftLengths, List<int> rightLengths)
        {
            leftLengths.ForEach(l => Console.Write("+-" + new string('-', l) + '-'));
            Console.Write("+ ");
            rightLengths.ForEach(l => Console.Write("+-" + new string('-', l) + '-'));
            Console.WriteLine("+");
        }
        public void PrintMenu()
        {
            lengths.ForEach(l => System.Console.Write("+-" + new string('-', l) + '-'));
            System.Console.WriteLine("+");

            string line = "";
            for (int i = 0; i < titles.Length; i++)
            {
                line += "| " + titles[i].PadRight(lengths[i]) + ' ';
            }
            System.Console.WriteLine(line + "|");

            //lengths.ForEach(l => System.Console.Write("+-" + new string('-', l) + '-'));
            //System.Console.WriteLine("+");

            foreach (var row in rows)
            {
                line = "";
                for (int i = 0; i < row.Length; i++)
                {
                    if (int.TryParse(row[i], out int n))
                    {
                        line += "| " + row[i].PadLeft(lengths[i]) + ' ';
                    }
                    else
                    {
                        line += "| " + row[i].PadRight(lengths[i]) + ' ';
                    }

                }
                lengths.ForEach(l => System.Console.Write("+-" + new string('-', l) + '-'));
                System.Console.WriteLine("+");
                System.Console.WriteLine(line + "|");
            }
            lengths.ForEach(l => System.Console.Write("+-" + new string('-', l) + '-'));
            System.Console.WriteLine("+");

        }
    }
}
