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
                    Console.Write("Veuillez saisir le nombre de rangées/colonnes du plateau (entre 1 et 12): ");
                    input = Console.ReadLine();
                } while (!input.Equals("q", StringComparison.InvariantCultureIgnoreCase) && (!Int32.TryParse(input, out size) || size <= 0 || size > 12));

                if (input.Equals("q", StringComparison.InvariantCultureIgnoreCase))
                    break;

                Queens(0, new List<Int32>(), new List<Int32>(), new List<Int32>(), new List<Int32>());

                Console.WriteLine("\nNombre de solutions trouvées: {0}\n", TupleCollection.Instance().Collection.Count);

                do
                {
                    Console.Write("Veuillez saisir le nombre de solutions à afficher: ");
                    input = Console.ReadLine();
                } while (!input.Equals("q", StringComparison.InvariantCultureIgnoreCase) && (!Int32.TryParse(input, out size) || size <= 0 || size > TupleCollection.Instance().Collection.Count));

                if (input.Equals("q", StringComparison.InvariantCultureIgnoreCase))
                    break;

                Console.WriteLine();

                for (int i = 0; i < size; i++)
                {
                    foreach(Int32 number in TupleCollection.Instance().Collection.ElementAt(i)) 
                    {
                        Console.Write(number + " ");
                    }
                    Console.Write(" ==> {0} tuples prometteurs", TupleCollection.Instance().PromisingTupleCounters.ElementAt(i));

                    Console.WriteLine();
                }

                Console.Write("\nNombre total de tuples prometteurs: {0}\n", promisingTupleCounter);
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
    }
}
