using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindShortestPath
{
    public class GraphMatrix<T> : IGraphNonWeighted<T> where T : IEquatable<T>
    {
        // fundament: dwuwymiarowa symetryczna tablica sąsiadów
        // oraz mapowanie etykiet wierzchołków na indeksy w tablicy i odwrotnie
        private readonly int[,] matrix;
        private readonly Dictionary<T, int> labelToIndex = new(); // C# < 12
        private readonly Dictionary<int, T> indexToLabel = new(); // C# 12+

        // właściwości i metody możliwe do zaimplementowania
        // wyłącznie na podstawie wiedzy o wewnętrznej strukturze grafu
        public int NoOfVertices => matrix.GetLength(0);
        public bool ContainsVertex(T label) => labelToIndex.ContainsKey(label);
        public IEnumerable<T> Vertices => labelToIndex.Keys;
        public bool ContainsEdge((T, T) edge) => matrix[labelToIndex[edge.Item1], labelToIndex[edge.Item2]] == 1;

        // celowo nie implementowane dodawanie i usuwanie wierzchołków
        public void AddVertex(T vertex) => throw new NotImplementedException();
        public void RemoveVertex(T vertex) => throw new NotImplementedException();


        // -------

        public GraphMatrix(IEnumerable<T> vertices)
        {
            int n = vertices.Count();
            matrix = new int[n, n];

            int i = 0;
            foreach (var vertex in vertices)
            {
                labelToIndex[vertex] = i;
                indexToLabel[i] = vertex;
                i++;
            }
        }

        public void AddEdge(T v1, T v2)
        {
            if (!ContainsVertex(v1) || !ContainsVertex(v2))
                throw new ArgumentException("Vertex not found in graph");
            if (v1.Equals(v2))
                throw new ArgumentException("Self-loops are not allowed in simple graph");
            int i = labelToIndex[v1];
            int j = labelToIndex[v2];
            matrix[i, j] = matrix[j, i] = 1;
        }

        // przeciążenie metody AddEdge dla krotki (T, T)
        public void AddEdge((T, T) edge) => AddEdge(edge.Item1, edge.Item2);

        public void RemoveEdge(T v1, T v2)
        {
            if (!ContainsVertex(v1) || !ContainsVertex(v2))
                throw new ArgumentException("Vertex not found in graph");

            var (i, j) = (labelToIndex[v1], labelToIndex[v2]);
            matrix[i, j] = matrix[j, i] = 0;
        }

        // przeciążenie metody RemoveEdge dla krotki (T, T)
        public void RemoveEdge((T, T) edge) => RemoveEdge(edge.Item1, edge.Item2);

        public IEnumerable<T> Neighbours(T v)
        {
            if (!ContainsVertex(v))
                throw new ArgumentException("Vertex not found in graph");
            int i = labelToIndex[v];
            for (int j = 0; j < NoOfVertices; j++)
                if (matrix[i, j] == 1)
                    yield return indexToLabel[j];
        }

        public IEnumerable<(T, T)> Edges =>
            from i in Enumerable.Range(0, NoOfVertices)
            from j in Enumerable.Range(i + 1, NoOfVertices - i - 1)
            where matrix[i, j] == 1
            select (indexToLabel[i], indexToLabel[j]);

        public int NoOfEdges => Edges.Count();

        // dostęp, tylko do odczytu, do wartości w macierzy sąsiedztwa
        public int this[T v1, T v2] =>
            (ContainsVertex(v1) && ContainsVertex(v2)) ?
                matrix[labelToIndex[v1], labelToIndex[v2]]
                : throw new ArgumentException("Vertex not found in graph");

        public void Display()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int k = 0; k < matrix.GetLength(1); k++)
                {
                    Console.Write(matrix[i, k]);
                }
                Console.WriteLine();
            }
        }
    }
}
