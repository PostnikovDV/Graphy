using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_4
{
    internal class AlgorithmDeikstry
    {
        static int InputMessage(string tmp)
        {
            Console.Write(tmp);
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
        public static void Menu(Graph graph)
        {
            int indexStart, indexEnd;
            Console.WriteLine("Введите номер начальной точки: ");
            do
            {
                indexStart = InputMessage("> ");
            } while (indexStart < 1 || indexStart > graph.matrix.GetLength(0));
            Console.WriteLine("Введите номер конечной точки: ");
            do
            {
                indexEnd = InputMessage("> ");
            } while ( indexEnd < 1 || indexEnd > graph.matrix.GetLength(0));

            graph.Points[indexStart - 1].Value = 0;
            graph.Points[indexEnd - 1].IsChecked = true;

            OneStep(graph, graph.Points[indexStart - 1], graph.Points[indexEnd - 1]);
            AlgorithmDeikstry.Run(graph, graph.Points[indexStart - 1], graph.Points[indexEnd - 1]);

            if (graph.Points[indexEnd - 1].Value != Point.maxValue)
            {
                Console.WriteLine(graph.Points[indexEnd - 1].Value);
                Point temp = graph.Points[indexEnd - 1];
                List<Point> path = new List<Point>();
                while (temp != null)
                {
                    path.Add(temp);
                    temp = temp.prev;
                }
                path.Reverse();
                Console.Write("Путь: ");
                int count = 0;
                foreach (var p in path)
                { if(count< path.Count-1)
                    Console.Write($"{p.Name} -> ");
                  else
                        Console.Write(p.Name);
                   count++;
                }
            }
            else Console.WriteLine("Пути не существует!");

            Console.WriteLine();
            for (int i = 0; i < graph.Points.Count; i++)
            {
                graph.Points[i].IsChecked = false;
                graph.Points[i].Value = Point.maxValue;
                graph.Points[i].prev = null;
            }
        }

        private static void Run(Graph graph, Point pointStart, Point pointEnd)
        {
            if (pointStart != pointEnd)
            {
                foreach (var edge in pointStart.Rebra)
                {
                    OneStep(graph, edge.anotherPoint(pointStart), pointEnd);
                    pointStart.IsChecked = true;
                }
                foreach (var edge in pointStart.Rebra)
                {

                    if (!edge.anotherPoint(pointStart).IsChecked)
                    {
                        edge.anotherPoint(pointStart).prev = pointStart;
                        Run(graph, edge.anotherPoint(pointStart), pointEnd);
                    }
                }
            }
        }

        private static void OneStep(Graph graph, Point pointStart, Point pointEnd)
        {

            foreach (var edge in pointStart.Rebra)
            {
                if (edge.anotherPoint(pointStart).Value >= (pointStart.Value + edge.Value))
                {
                    edge.anotherPoint(pointStart).Value = pointStart.Value + edge.Value;
                    edge.anotherPoint(pointStart).prev = pointStart;
                }
            }
        }
    }
}
