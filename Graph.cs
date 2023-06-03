using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace laba_4
{
    public class Graph
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
        public List<Point> Points { get; private set; }

        public Rebro[,] matrix { get; set; }
        /*public List<string> catalogCycles;*/
        
        public Graph()
        {
            Points = null;
        }

        public Graph(List<Point> points)
        {
            Points = points;
        }
       
        public void Create(int n)
        {
            int x;
            Console.WriteLine("1. Создать вручную\r\n" + "2. Создать случайно\r\n");
         
            do
            {
                x = InputMessage("> ");
            } while (x < 1 || x > 2);
            Points = new List<Point>();
            char[] pointNames = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            int pointsAmount;
            Console.WriteLine($"Введите количество вершин (не больше {pointNames.Length}): ");
            do
            {
                pointsAmount = InputMessage("> ");
            } while(pointsAmount < 2 || pointsAmount > 36);
            for (int i = 0; i < pointsAmount; i++)
            {
                Points.Add(new Point(pointNames[i].ToString(), i));
            }
            int edgesAmount;

            Console.WriteLine($"Введите количество ребер (не больше {pointsAmount * (pointsAmount - 1) / 2}):");
            do
            {
                edgesAmount = InputMessage("> ");
            } while (edgesAmount < 1 || edgesAmount > (pointsAmount * (pointsAmount - 1) / 2));
            matrix = new Rebro[pointsAmount, pointsAmount];
            for (int i = 0; i < pointsAmount; i++)
            {
                for (int j = 0; j < pointsAmount; j++)
                {
                    matrix[i, j] = new Rebro();
                }
            }
            switch (x)
            {
                case 1:

                    Console.WriteLine("Матрица: ");
                    Print();
                    for (int edgeCounter = 0; edgeCounter < edgesAmount; edgeCounter++)
                    {
                        int indexFirst, indexSecond;
                        Console.WriteLine("Введите номера вершин: ");
                        
                        do
                        {
                            indexFirst = InputMessage("> ");
                        } while ( indexFirst < 0 || indexFirst > pointsAmount);

                        do
                        {
                            indexSecond = InputMessage("> ");
                        } while(indexSecond < 0 || indexSecond > pointsAmount || indexFirst == indexSecond);

                        int newEdgeValue;

                        Console.WriteLine("Введите вес ребра: ");
                        do
                        {
                            
                            newEdgeValue = InputMessage("> ");
                        } while ( newEdgeValue < 0);
                        indexFirst -= 1;
                        indexSecond -= 1;
                        if (n == 1)
                        {
                            matrix[indexFirst, indexSecond].Value = newEdgeValue;
                            matrix[indexFirst, indexSecond].FirstPoint = Points[indexFirst];
                            matrix[indexFirst, indexSecond].SecondPoint = Points[indexSecond];
                            Points[indexFirst].Rebra.Add(matrix[indexFirst, indexSecond]);
                        }
                        if (n == 2)
                        {
                          
                            matrix[indexFirst, indexSecond].Value = newEdgeValue;
                            matrix[indexFirst, indexSecond].FirstPoint = Points[indexFirst];
                            matrix[indexFirst, indexSecond].SecondPoint = Points[indexSecond];
                            Points[indexFirst].Rebra.Add(matrix[indexFirst, indexSecond]);
                            Points[indexSecond].Rebra.Add(matrix[indexFirst, indexSecond]);
                            matrix[indexSecond, indexFirst].Value = newEdgeValue;
                            matrix[indexSecond, indexFirst].FirstPoint = Points[indexFirst];
                            matrix[indexSecond, indexFirst].SecondPoint = Points[indexSecond];
                        }
                    }
                    break;
                case 2:
                    Random rnd = new Random();
                    for (int edgeCounter = 0; edgeCounter < edgesAmount; edgeCounter++)
                    {
                        int indexFirst = rnd.Next(pointsAmount), indexSecond = rnd.Next(pointsAmount), newEdgeValue = rnd.Next(0, 100);
                        if (matrix[indexFirst, indexSecond].Value == 99999999 && indexFirst != indexSecond)
                        {
                            if (n == 1)
                            {
                                matrix[indexFirst, indexSecond].Value = newEdgeValue;
                                matrix[indexFirst, indexSecond].FirstPoint = Points[indexFirst];
                                matrix[indexFirst, indexSecond].SecondPoint = Points[indexSecond];
                                Points[indexFirst].Rebra.Add(matrix[indexFirst, indexSecond]);
                            }
                     
                            
                            if(n==2)
                            {
                                matrix[indexFirst, indexSecond].Value = newEdgeValue;
                                matrix[indexFirst, indexSecond].FirstPoint = Points[indexFirst];
                                matrix[indexFirst, indexSecond].SecondPoint = Points[indexSecond];
                                Points[indexFirst].Rebra.Add(matrix[indexFirst, indexSecond]);
                                Points[indexSecond].Rebra.Add(matrix[indexFirst, indexSecond]);
                                matrix[indexSecond, indexFirst].Value = newEdgeValue;
                                matrix[indexSecond, indexFirst].FirstPoint = Points[indexFirst];
                                matrix[indexSecond, indexFirst].SecondPoint = Points[indexSecond];
                            }


                        }
                        else edgeCounter--;
                    }
                    break;
            }
        }


      /*  public List<Point> TopologicalSort()
        {
            List<Point> sortedVertices = new List<Point>();

            // Создаем словарь для отслеживания посещенных вершин
            Dictionary<Point, bool> visited = new Dictionary<Point, bool>();

            // Инициализируем все вершины как непосещенные
            foreach (Point vertex in Points)
            {
                visited[vertex] = false;
            }

            // Создаем стек для сохранения временного упорядочения вершин
            Stack<Point> stack = new Stack<Point>();

            // Выполняем обход в глубину (DFS) для каждой вершины
            foreach (Point vertex in Points)
            {
                if (!visited[vertex])
                {
                    if (DFSTopologicalSort(vertex, visited, stack))
                    {
                        // Если обнаружен цикл, граф не является DAG, возвращаем null или выбрасываем исключение
                        return null;
                    }
                }
            }

            // Извлекаем вершины из стека в обратном порядке
            while (stack.Count > 0)
            {
                sortedVertices.Add(stack.Pop());
            }

            return sortedVertices;
        }
        public List<Point> GetAdjacentVertices(Point vertex)
        {
            List<Point> adjacentVertices = new List<Point>();

            // Получаем индекс вершины в матрице
            int vertexIndex = Points.IndexOf(vertex);

            // Проверяем все вершины в матрице
            for (int i = 0; i < Points.Count; i++)
            {
                // Проверяем наличие ребра между вершинами vertexIndex и i
                if (matrix[vertexIndex, i] != null)
                {
                    // Получаем смежную вершину
                    Point adjacentVertex = Points[i];

                    // Добавляем вершину в список смежных вершин, если ее имя не совпадает с исходной вершиной
                    if (adjacentVertex.Name != vertex.Name)
                    {
                        adjacentVertices.Add(adjacentVertex);
                    }
                }
            }

            return adjacentVertices;
        }
        private bool DFSTopologicalSort(Point vertex, Dictionary<Point, bool> visited, Stack<Point> stack)
        {
            // Помечаем текущую вершину как посещенную
            visited[vertex] = true;

            // Получаем смежные вершины текущей вершины
            List<Point> adjacentVertices = GetAdjacentVertices(vertex);

            foreach (Point adjacentVertex in adjacentVertices)
            {
                // Если смежная вершина еще не посещена, выполняем рекурсивный вызов DFS
                if (!visited[adjacentVertex])
                {
                    if (DFSTopologicalSort(adjacentVertex, visited, stack))
                    {
                        // Если обнаружен цикл, граф не является DAG, возвращаем true
                        return true;
                    }
                }
                // Если смежная вершина уже посещена и находится в стеке,
                // значит, в графе есть цикл, возвращаем true
                else if (stack.Contains(adjacentVertex))
                {
                    return true;
                }
            }

            // Добавляем текущую вершину в стек
            stack.Push(vertex);

            return false;
        }


        public List<List<Point>> GetLayeredParallelForm()
        {
            // Получаем топологически упорядоченный список вершин
            List<Point> sortedVertices = TopologicalSort();

            if (sortedVertices == null)
            {
                // Граф содержит циклы, невозможно построить ярусно-параллельную форму
                Console.WriteLine("Граф содержит циклы. Ярусно-параллельная форма не может быть построена.");
                return null;
            }

            // Создаем список слоев для хранения вершин на каждом уровне
            List<List<Point>> layers = new List<List<Point>>();

            foreach (Point vertex in sortedVertices)
            {
                // Получаем смежные вершины для текущей вершины
                List<Point> adjacentVertices = GetAdjacentVertices(vertex);

                // Получаем максимальный уровень смежных вершин
                int maxLevel = -1;
                foreach (Point adjacentVertex in adjacentVertices)
                {
                    int level = layers.FindIndex(layer => layer.Contains(adjacentVertex));
                    if (level > maxLevel)
                    {
                        maxLevel = level;
                    }
                }

                // Добавляем текущую вершину на следующий уровень
                int currentLevel = maxLevel + 1;
                if (currentLevel >= layers.Count)
                {
                    layers.Add(new List<Point>());
                }
                layers[currentLevel].Add(vertex);
            }

            // Выводим ярусно-параллельную форму
            Console.WriteLine("Ярусно-параллельная форма графа:");
            for (int i = 0; i < layers.Count; i++)
            {
                Console.Write($"Уровень {i + 1}: ");
                foreach (Point vertex in layers[i])
                {
                    Console.Write($"{vertex.Name} ");
                }
                Console.WriteLine();
            }

            return layers;
        }*/

        public void Print()
        {

            Console.WriteLine();
            Console.Write("  ");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.Write($"{Points[i].Name}\t");

            }
            Console.Write("\r\n");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.Write(Points[i].Name + " ");
                for (int j = 0; j < matrix.GetLength(1); j++)
                {

                    if (matrix[i, j].Value < 99999999) Console.Write($"{matrix[i, j].Value.ToString()}\t");
                    else Console.Write("0\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}



