using System;
using System.Collections.Generic;

namespace FindShortestPath
{
    public static class Algo
    {
        public static List<T>? FindShortestPath<T>(
            GraphMatrix<T> graph,
            T start,
            T end
            ) where T : struct, IEquatable<T>
        {
            var vs = graph.Vertices.ToList();
            var visited = new bool[vs.Count];
            var previous = new T?[vs.Count];
            Queue<T> queue = new ();
            Dictionary<T,int> indexedValues = new();

            for (int i = 0; i < vs.Count; i++)
            {
                indexedValues.Add(vs[i], i);
                previous[i] = null; 
            }

            visited[indexedValues[start]] = true;
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                T node = queue.Dequeue();

                if (node.Equals(end))
                {
                    return ConstructPath(indexedValues, previous, end);
                }

                for (int i = 0; i < vs.Count; i++)
                {
                    if (graph.ContainsEdge((node, vs[i])) && !visited[i])
                    {
                        visited[i] = true;
                        previous[i] = node;
                        queue.Enqueue(vs[i]);
                    }
                }
            }

            return null;
        }

        public static List<T> ConstructPath<T>(Dictionary<T, int> indexed,  T?[] previous, T end) where T : struct, IEquatable<T>
        {
            List<T> path = new();
            for (T? at = end; at != null; at = previous[indexed[(T)at]])
            {
                path.Add((T)at);
            }
            path.Reverse();
            return path;
        }
    }
}
