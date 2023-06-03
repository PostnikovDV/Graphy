using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace laba_4
{
    public class Program
    {
        static int InputMessage(string tmp)
        {
            Console.WriteLine(tmp);
            string f = Console.ReadLine();
            bool t = int.TryParse(f, out int temp);
            if (!t)
            {
                Console.WriteLine("Ввели не число ошибка!");
                return 0;
            }
            else
                return temp;
        }
       
       
        static void Main(string[] args)
        {
            int n =1;
            Graph graph = new Graph();
            Console.WriteLine("Граф ориентированный!");
            graph.Create(n);
            graph.Print();
            bool exit = false;
            while (!exit)
            {
                int x;
                Console.WriteLine("1. Создать граф\r\n" +
                    "2. Вывести матрицу смежности\r\n" +
                    "3. Поиск минимального пути методом Дейкстры\r\n" +
                    "4. Поиск клики графа\r\n" +
                    "5. Ярусно - Параллельная форма\r\n" +
                    "6. Компоненты сильной связности\r\n" +
                    "7. Очистить консоль\r\n" +
                    "8. Выход");
                do
                {
                    x = InputMessage("> ");
                } while (x < 1 || x > 7);
                switch (x)
                {
                    case 1:
                         n = InputMessage("1. Ориентированный\r\n" + "2. Неориентированный\r\n");
                        graph = new Graph();
                        graph.Create(n);
                        break;
                    case 2:
                        if (graph == null)
                            return;
                        graph.Print();
                        break;
                    case 3:
                          AlgorithmDeikstry.Menu(graph); 
                       
                        break;
                    case 4:
                        if (n == 2)
                            Clika.Run(graph);
                        else
                        { 
                            Console.WriteLine("Ошибка, нужно создать неориентированный граф!");
                            
                        }
                        break;
                    case 5:
                     /*   if (n == 1)
                        {
                            graph.GetLayeredParallelForm();
                        }
                        else Console.WriteLine("Ошибка, нужно создать ориентированный граф!");*/
                        break;
                    case 6:
                        if (graph == null)
                            return;

                        break;
                    case 7:
                        Console.Clear();
                        break;
                    case 8:
                        exit = true;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
