using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AssemblySort
{
    public static class FileHandler
    {

        public static void OpenFile(List<AssemblyProduct> assemblySort)
        {
            Console.WriteLine("Input file path");
            //var filePath = Console.ReadLine();
            var filePath = "C:\\Users\\PICHAU\\Desktop\\Teste.txt";
            Console.WriteLine("Opening file at file path: " + filePath);
            AssemblySorter a = new AssemblySorter();
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Open);
                using (StreamReader r = new StreamReader(fs))
                {
                    string line = r.ReadLine();
                    while (!String.IsNullOrEmpty(line))
                    {
                        assemblySort = a.AddAssemblyLine(assemblySort, line);
                        line = r.ReadLine();
                    }
                    assemblySort.Sort();
                }
                a.CreateAssemblyLine(assemblySort);
            }
            catch (Exception e) { }
            foreach (var item in assemblySort)
            {
                Console.WriteLine(item.productName);
            }


        }
    }
}