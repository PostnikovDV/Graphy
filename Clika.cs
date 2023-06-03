using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_4
{
    internal class Clika
    {
        static List<Point> maxClique = new List<Point>();

        static public void Run(Graph graph)
        {
            List<Point> subgraph = new List<Point>();
            List<Point> candidates = new List<Point>(graph.Points);
            maxClique = new List<Point>();
            List<Point> not = new List<Point>();
            Extend(subgraph, candidates, not);
            Console.Write("Максимальная клика: ");
            foreach (var point in maxClique) Console.Write(point.Name + ' ');
            Console.WriteLine();
        }

        static void Extend(List<Point> subgraph, List<Point> candidates, List<Point> not)
        {

            while (candidates.Count != 0 && Check(candidates, not))
            {
                Point v = candidates[0];
                subgraph.Add(v);
                List<Point> new_subgraph = new List<Point>();
                List<Point> new_not = new List<Point>();
                bool flag;
                foreach (var point in candidates)
                {
                    flag = false;
                    foreach (var edge in point.Rebra)
                    {
                        if (v == edge.anotherPoint(point)) flag = true;
                    }
                    if (flag) new_subgraph.Add(point);
                }
                foreach (var point in not)
                {
                    flag = false;
                    foreach (var edge in point.Rebra)
                    {
                        if (v == edge.anotherPoint(point)) flag = true;
                    }
                    if (flag) new_not.Add(point);
                }
                if (new_subgraph.Count == 0 && new_not.Count == 0)
                {
                    if (subgraph.Count > maxClique.Count)
                    {
                        maxClique = new List<Point>(subgraph);
                    }
                }
                else
                {
                    Extend(subgraph, new_subgraph, new_not);
                }
                candidates.Remove(v);
                subgraph.Remove(v);
                not.Remove(v);
            }
        }

        static bool Check(List<Point> candidates, List<Point> not)
        {

            foreach (var point in not)
            {
                bool check = false;
                foreach (var edge in point.Rebra)
                {
                    if (candidates.Contains(edge.anotherPoint(point)))
                    {
                        check = true;
                        break;
                    }
                }
                if (!check) return false;
            }
            return true;
        }
    }
}
