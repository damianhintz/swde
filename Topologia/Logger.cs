using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace egib.TopologiaKonturuów
{
    public static class Logger
    {
        private static string _inputPath;
        private static string _outputPath;

        public static string dir(this string fileName) { return Path.GetDirectoryName(fileName); }
        public static string name(this string fileName) { return Path.GetFileName(fileName); }
        public static string path(this string name) { return Path.Combine(_inputPath, name); }
        public static string outPath(this string name) { return Path.Combine(_outputPath, name); }
        public static string outPath() { return _outputPath; }

        public static int topologiczne = 0;
        public static int oznaczenia = 0;

        public static void inputPath(string path)
        {
            //Kontrakt.requires(Directory.Exists(path));
            Console.WriteLine("Katalog roboczy: {0}", path);
            _inputPath = path;
            _outputPath = Path.Combine(_inputPath, DateTime.Now.ToString("yyyyMMddTHHmm"));
            if (!Directory.Exists(_outputPath)) Directory.CreateDirectory(_outputPath);
        }

        public static string asExt(this string fileName, string ext)
        {
            return Path.Combine(
                Path.GetDirectoryName(fileName),
                Path.GetFileNameWithoutExtension(fileName) + ext);
        }

        public static string asLog(this string fileName)
        {
            return asExt(fileName, ".log");
        }

        public static string[] files(this string pattern)
        {
            return Directory.GetFiles(_inputPath, pattern,
                SearchOption.TopDirectoryOnly);
        }

        public static void writeWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Ostrzeżenie: {0}", message);
            Console.ResetColor();
            File.AppendAllLines("egib.kluKontrola.log".outPath(),
                new string[] { string.Format("Ostrzeżenie: {0}", message) },
                Encoding.GetEncoding(1250));
            oznaczenia++;
        }

        public static void writeTopo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Ostrzeżenie: {0}", message);
            Console.ResetColor();
            File.AppendAllLines("egib.kluKontrola.log".outPath(),
                new string[] { string.Format("Ostrzeżenie: {0}", message) },
                Encoding.GetEncoding(1250));
            topologiczne++;
        }

        public static void writeBłąd(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Błąd: {0}", message);
            Console.ResetColor();
            File.AppendAllLines("egib.kluKontrola.log".outPath(),
                new string[] { string.Format("Błąd: {0}", message) },
                Encoding.GetEncoding(1250));
            topologiczne++;
        }

        public static void write(string message)
        {
            //Console.ForegroundColor = ConsoleColor.Yellow;
            //Console.WriteLine("{0}", message);
            //Console.ResetColor();
            File.AppendAllLines("egib.kluKontrola.log".outPath(),
                new string[] { string.Format("{0}", message) },
                Encoding.GetEncoding(1250));
        }

        public static void writeEnd()
        {
            Console.WriteLine("Niezgodności oznaczenia klasoużytku: {0}", oznaczenia);
            Console.WriteLine("Niezgodności topologiczne: {0}", topologiczne);
            write("Niezgodności oznaczenia klasoużytku: " + oznaczenia);
            write("Niezgodności topologiczne: " + topologiczne);
        }
    }
}
