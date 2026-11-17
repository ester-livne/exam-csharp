using App;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = new UTF8Encoding(false);
        string path = args.Length > 0 ? args[0] : FindSample();
        string[] lines = File.ReadAllLines(path, Encoding.UTF8);
        int reportDay = ReadReportDay(lines);
        List<string[]> rows = ReadRows(lines);
        Console.Error.WriteLine("skeleton ok: read " + Path.GetFileName(path) + " - report day " + reportDay + " - " + rows.Count + " rows");

        List<Lone> lones = new List<Lone>();

        foreach (string[] row in rows)
        {
            if (row.Length < 5) continue;

            bool returned = row.Length >= 6 && row[5] != "";
            int returnDay = returned ? int.Parse(row[5]) : reportDay;

            Lone l = new Lone(int.Parse(row[0]), row[1], row[2], row[3], int.Parse(row[4]), returnDay);
            l.Returned = returned;
            lones.Add(l);
        }

        PrintFines(lones);
        PrintFlagged(lones);
    }

    // חישוב קנס לפי סוג הקוראת
    static int CalcFine(Lone l)
    {
        int days = l.Return_day - l.Loan_day;

        if (l.type == "student")
        {
            int late = days - 14;
            if (late <= 1) return 0;          
            return Math.Min(late, 30);       
        }

        if (l.type == "guest")
        {
            int late = days - 7;
            if (late <= 0) return 0;
            return (int)Math.Ceiling(late * 2.5);   
        }

        return 0;   // teacher
    }

    // מורה שלא החזירה ועברו יותר מ-60 ימים
    static bool IsFlagged(Lone l)
    {
        return l.type == "teacher" && !l.Returned && (l.Return_day - l.Loan_day) > 60;
    }

    static void PrintFines(List<Lone> lones)
    {
        Console.WriteLine("FINES");
        foreach (Lone l in lones)
        {
            Console.WriteLine($"{l.ReaderId},{l.name},{CalcFine(l)}");
        }
    }

    static void PrintFlagged(List<Lone> lones)
    {
        Console.WriteLine("FLAGGED");
        foreach (Lone l in lones)
        {
            if (IsFlagged(l))
                Console.WriteLine($"{l.ReaderId},{l.name}");
        }
    }

    static int ReadReportDay(string[] lines)
    {
        return int.Parse(lines[0].Split(':')[1].Trim());
    }

    // The lines after the two first ones, each split into its fields
    static List<string[]> ReadRows(string[] lines)
    {
        List<string[]> rows = new List<string[]>();
        for (int i = 2; i < lines.Length; i++)
        {
            if (lines[i].Trim() == "") continue;
            string[] fields = lines[i].Split(',');
            for (int j = 0; j < fields.Length; j++) fields[j] = fields[j].Trim();
            rows.Add(fields);
        }
        return rows;
    }

    // Without a path, look for data/sample.txt next to the project
    static string FindSample()
    {
        DirectoryInfo dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir != null)
        {
            string candidate = Path.Combine(dir.FullName, "data", "sample.txt");
            if (File.Exists(candidate)) return candidate;
            dir = dir.Parent;
        }
        return Path.Combine("data", "sample.txt");
    }
}