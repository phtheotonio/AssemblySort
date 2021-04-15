using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblySort
{
    public class AssemblySorter
    {

        public List<AssemblyProduct> AddAssemblyLine(List<AssemblyProduct> assembly, string line)
        {
            var split = line.Split(" ");
            if (line.ToLower().Contains("maintenance"))
            {
                assembly.Add(new AssemblyProduct { productName = line, assemblyValue = 5 });
            }
            else
            {
                foreach (var item in split)
                {
                    if (item.Contains("min"))
                    {
                        assembly.Add(new AssemblyProduct { productName = line, assemblyValue = (int.Parse(item.Replace("min", ""))) });
                    }
                }
            }
            return assembly;
        }

        public void CreateAssemblyLine(List<AssemblyProduct> assembly)
        {
            var resposta = LineMaker(assembly);

            var i = 1;
            foreach (var line in resposta)
            {

                Console.WriteLine("Linha de Producao " + i);
                foreach (var product in line.assemblyLine)
                {
                    Console.WriteLine(product.startingTime + ": " + product.productName);
                }
                Console.WriteLine();
                i++;
            }
        }

        private List<AssemblyLine> LineMaker(List<AssemblyProduct> assembly)
        {
            List<AssemblyProduct> remove = new List<AssemblyProduct>();//Itens a serem removidos
            List<AssemblyLine> assemblyList = new List<AssemblyLine>(); //Fila de Linhas de Produção
            AssemblyProduct almoco = new AssemblyProduct { assemblyValue = 60, productName = "Almoco" };
            AssemblyProduct ginasticaLaboral = new AssemblyProduct { assemblyValue = 60, productName = "Ginastica Laboral" };

            DateTime work = new DateTime(2021, 1, 1, 9, 0, 0);//Data aleatória para controle das horas
            DateTime workComparative = new DateTime(2021, 1, 1, 9, 0, 0);//Data aleatória para calcular se a fila de produção pode receber mais um item sem exceder os horarios de Almoço e Ginástica Laboral

            while (assembly.Count > 0)
            {
                AssemblyLine line = new AssemblyLine();
                int filaReprocessada = 0;
                bool almocoRealizado = false;
                bool ginastica = false;
                while (work.Hour < 12) //Cria o período da Manhã
                {
                    foreach (var productItem in assembly)
                    {
                        workComparative = workComparative.AddMinutes(productItem.assemblyValue);
                        if (workComparative.Hour < 12 || (workComparative.Hour == 12 && workComparative.Minute == 0))
                        {
                            productItem.startingTime = work.TimeOfDay.ToString();
                            line.assemblyLine.Add(productItem);
                            work = work.AddMinutes(productItem.assemblyValue);
                            remove.Add(productItem);
                        }
                        else if (!almocoRealizado && work.Hour == 12)
                        {
                            line.assemblyLine.Add(new AssemblyProduct { assemblyValue = 60, productName = "Almoço", startingTime = work.TimeOfDay.ToString() });
                            work = work.AddMinutes(60);
                            almocoRealizado = true;
                        }                        
                        else if (filaReprocessada >=3 && !almocoRealizado && workComparative.Hour >= 12 && workComparative.Minute > 0)
                        {
                            while (work.Hour < 12)
                            {
                                work = work.AddMinutes(1);
                            }
                            line.assemblyLine.Add(new AssemblyProduct { assemblyValue = 60, productName = "Almoço", startingTime = work.TimeOfDay.ToString() });
                            work = work.AddMinutes(60);
                            almocoRealizado = true;
                        }
                        
                        workComparative = work;
                    }
                    foreach (var removeItem in remove)//Remove os itens usados nessa iteração da fila total para evitar duplicidade
                    {
                        assembly.Remove(removeItem);
                    }
                    remove = new List<AssemblyProduct>();
                    filaReprocessada++;
                }
                filaReprocessada = 0;
                while (work.Hour <= 13 && work.Hour < 17) //Cria o período da Tarde
                {
                    foreach (var productItem in assembly)
                    {
                        workComparative = workComparative.AddMinutes(productItem.assemblyValue);
                        if ((workComparative.Hour <= 16 && workComparative.Hour < 17) || (workComparative.Hour == 17 && workComparative.Minute == 0))
                        {

                            productItem.startingTime = work.TimeOfDay.ToString();
                            line.assemblyLine.Add(productItem);
                            work = work.AddMinutes(productItem.assemblyValue);
                            remove.Add(productItem);
                        }
                        else if (work.Hour >= 16 && work.Hour <= 17 && !ginastica && filaReprocessada >=3)
                        {
                            line.assemblyLine.Add(new AssemblyProduct { assemblyValue = 60, productName = "Ginastica Laboral", startingTime = work.TimeOfDay.ToString() });
                            work = work.AddMinutes(60);
                            ginastica = true;
                        }
                        workComparative = work;
                    }
                    

                    foreach (var removeItem in remove)//Remove os itens usados nessa iteração da fila total para evitar duplicidade
                    {
                        assembly.Remove(removeItem);
                    }
                    remove = new List<AssemblyProduct>();
                    filaReprocessada++;
                }
                while (!ginastica)//Adiciona a Ginastica Laboral no período adequado mesmo se a fila de produção estiver finalizada
                {
                    if (work.Hour >= 16 && work.Hour <= 17 && !ginastica)
                    {
                        line.assemblyLine.Add(new AssemblyProduct { assemblyValue = 60, productName = "Ginastica Laboral", startingTime = work.TimeOfDay.ToString() });
                        work = work.AddMinutes(60);
                        ginastica = true;
                    }
                    work = work.AddMinutes(1);
                }
                assemblyList.Add(line);
                work = new DateTime(2021, 1, 1, 9, 0, 0);
                workComparative = new DateTime(2021, 1, 1, 9, 0, 0);
                almocoRealizado = false;
                ginastica = false;
                filaReprocessada = 0;
            }

            return assemblyList;

        }
    }
}
