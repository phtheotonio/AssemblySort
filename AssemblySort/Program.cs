using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace AssemblySort
{

    

    class Program
    {

        static List<AssemblyProduct> assemblySort = new List<AssemblyProduct>();
        static void Main(string[] args)
        {
            FileHandler.OpenFile(assemblySort);
        }        

    }
}
