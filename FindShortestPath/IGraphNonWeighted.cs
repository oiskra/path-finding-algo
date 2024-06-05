using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindShortestPath
{
    public interface IGraphNonWeighted<T>
    {
        int NoOfVertices { get; } 
        int NoOfEdges { get; } 
        bool ContainsVertex(T vertex); 
        bool ContainsEdge((T, T) edge); 
        IEnumerable<(T, T)> Edges { get; } 
        IEnumerable<T> Vertices { get; } 
        IEnumerable<T> Neighbours(T vertex); 

        void AddVertex(T vertex);
        void RemoveVertex(T vertex); 
        void AddEdge((T, T) edge); 
        void RemoveEdge((T, T) edge); 
    }
}
