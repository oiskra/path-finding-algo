using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindShortestPath
{
    public interface IGraphNonWeighted<T>
    {
        int NoOfVertices { get; } // liczba wierzchołków
        int NoOfEdges { get; } // liczba krawędzi
        bool ContainsVertex(T vertex); // sprawdza, czy wierzchołek jest w grafie
        bool ContainsEdge((T, T) edge); // sprawdza, czy krawędź jest w grafie
        IEnumerable<(T, T)> Edges { get; } // zwraca wszystkie krawędzie grafu
        IEnumerable<T> Vertices { get; } // zwraca wszystkie wierzchołki grafu
        IEnumerable<T> Neighbours(T vertex); // zwraca listę sąsiadów wierzchołka

        void AddVertex(T vertex); // dodaje wierzchołek do grafu
        void RemoveVertex(T vertex); // usuwa wierzchołek z grafu
        void AddEdge((T, T) edge); // dodaje krawędź do grafu
        void RemoveEdge((T, T) edge); // usuwa krawędź z grafu
    }
}
