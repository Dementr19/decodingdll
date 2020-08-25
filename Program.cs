using System;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;

namespace decodingdll
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0;
            string path = "";
            while (i != 1)
            {
                Console.WriteLine("Введите путь к директории");
                path = Console.ReadLine();
                FindDll(path);
                Console.WriteLine("Завершить?Y/N");
                string t = Console.ReadLine();
                if (t == "Y" || t == "y") i = 1;
            }
            
        }

        public static void FindDll(string path)
        {
            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    if (file.Substring(file.Length - 4) == ".dll")
                    {
                        FileInfo fileinf = new FileInfo(file); 
                        DisplayInfoFromDll(fileinf);
                    }
                }
            }
            else Console.WriteLine("Директория не существует");
        } 

        public static void DisplayInfoFromDll(FileInfo file)
        {
            try
            {
                Assembly dll = Assembly.LoadFrom(file.FullName);
                Console.WriteLine(dll.FullName);
                Type[] classes = dll.GetTypes();
                foreach (var clss in classes)
                {
                    Console.WriteLine("   " + clss.Name);
                    MethodInfo[] methods = clss.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
                    foreach (var method in methods)
                    {
                        Console.WriteLine("      -" + method.Name);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Невозможно прочитать библиотеку " + file.Name);
            }
        }

    }
}
