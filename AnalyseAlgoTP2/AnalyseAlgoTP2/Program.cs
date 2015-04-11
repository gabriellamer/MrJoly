using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyseAlgoTP2
{
    class Program
    {
        static Int32 size;
        static Int32 show;
        static Int32 promisingTupleCounter;

        static void Main(string[] args)
        {
            String input;

            do {
                input = String.Empty;
                size = 0;
                promisingTupleCounter = 0;
                TupleCollection.Instance().Collection.Clear();
                TupleCollection.Instance().PromisingTupleCounters.Clear();

                Console.WriteLine("================= Algo Reines =================\n");
                Console.WriteLine("(Vous pouvez appuyer sur la touche 'Q' pour quitter)\n");

                do {
                    Console.Write("Veuillez saisir le nombre de rangées/colonnes du plateau (entre 1 et 10): ");
                    input = Console.ReadLine();
                } while (!input.Equals("q", StringComparison.InvariantCultureIgnoreCase) && (!Int32.TryParse(input, out size) || size <= 0 || size > 10));

                if (input.Equals("q", StringComparison.InvariantCultureIgnoreCase))
                    break;

                Queens(0, new List<Int32>(), new List<Int32>(), new List<Int32>(), new List<Int32>());

                Console.WriteLine("\nNombre de solutions trouvées: {0}\n", TupleCollection.Instance().Collection.Count);

                if (TupleCollection.Instance().Collection.Count != 0)
                {
                    do
                    {
                        Console.Write("Veuillez saisir le nombre de solutions à afficher: ");
                        input = Console.ReadLine();
                    } while (!input.Equals("q", StringComparison.InvariantCultureIgnoreCase) && (!Int32.TryParse(input, out show) || show <= 0 || show > TupleCollection.Instance().Collection.Count));

                    if (input.Equals("q", StringComparison.InvariantCultureIgnoreCase))
                        break;

                    Console.WriteLine();

                    for (int i = 0; i < show; i++)
                    {
                        Show(i);
                    }
                }

                Console.Write("Nombre total de tuples prometteurs: {0}\n", promisingTupleCounter);
                input = Console.ReadLine();
            } while (!input.Equals("q", StringComparison.InvariantCultureIgnoreCase));
        }

        private static void Queens(Int32 rowIndex, List<Int32> cols, List<Int32> diag45, List<Int32> diag135, List<Int32> tuples)
        {
            if (rowIndex == size) 
            {
                TupleCollection.Instance().Collection.Add(new List<Int32>(tuples));
                TupleCollection.Instance().PromisingTupleCounters.Add(promisingTupleCounter);
            }
            else
            {
                for (int i = 0; i < size; i++)
                {
                    if (!cols.Exists(x => x == i) && !diag45.Exists(x => x == (i - rowIndex)) && !diag135.Exists(x => x == (i + rowIndex)))
                    {
                        tuples.Add(i);
                        cols.Add(i);
                        diag45.Add(i - rowIndex);
                        diag135.Add(i + rowIndex);

                        promisingTupleCounter++;

                        Queens(rowIndex + 1, cols, diag45, diag135, tuples);

                        tuples.RemoveAt(tuples.Count - 1);
                        cols.RemoveAt(cols.Count - 1);
                        diag45.RemoveAt(diag45.Count - 1);
                        diag135.RemoveAt(diag135.Count - 1);
                    }
                }
            }
        }

        private static void Show(int element)
        {
            string table;

            table = "";

            for (int k = 0; k < ((size * 2) + 1); k++)
            {
                table += "-";
            }

            table += "\n";

            for (int j = 1; j < ((size * 2) + 1); j++)
            {
                if (table[table.Length - 2] == '|')
                {
                    for (int k = 0; k < ((size * 2) + 1); k++)
                    {
                        table += "-";
                    }

                    table += "\n";
                }
                else
                {
                    table += "|";

                    for (int k = 0; k < size; k++)
                    {
                        if (TupleCollection.Instance().Collection.ElementAt(element)[((j - 1) / 2)] == k)
                        {
                            table += "R|";
                        }
                        else
                        {
                            table += " |";
                        }
                    }

                    table += "\n";
                }
            }

            Console.Write(table);
            Console.Write("Trouvé après {0} tuples prometteurs\n", TupleCollection.Instance().PromisingTupleCounters.ElementAt(element));

            Console.WriteLine();
        }
    }
}
