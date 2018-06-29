using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RC3.Graphs;

/*
 * Notes
 */

namespace RC3.Unity
{
    /// <summary>
    /// 
    /// </summary>
    public class SharedAnalysisEdgeGraph<V, E> : ScriptableObject
        where V : VertexObject
        where E : EdgeObject
    {
        private EdgeGraph _graph;
        private List<V> _vertexObjs;
        private List<E> _edgeObjs;

        //Graph Analysis Variables 
        private int _connectedcomponentscount = -1;
        private List<HashSet<int>> _connectedcomponents = new List<HashSet<int>>();
        private float[] _normalizedcomponents;
        private float[] _normalizedcomponentsbysize;
        private int _closurescount = -1;
        private float _closurerate = -1f;
        private List<int> _sources = new List<int>();
        private int[] _depths;
        private float[] _normalizeddepths;
        private int _maxdepth = -1;
        private int _deepverticescount = -1;
        private List<int> _unreachablevertices = new List<int>();
        private List<int> _edgelessvertices = new List<int>();

        //Used for drawing the graph
        private Vector3[] _vertices;
        private List<int> _lineindices;


        /// <summary>
        /// 
        /// </summary>
        public EdgeGraph Graph
        {
            get { return _graph; }
            set { _graph = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public List<V> VertexObjects
        {
            get { return _vertexObjs; }
            set { _vertexObjs = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public List<E> EdgeObjects
        {
            get { return _edgeObjs; }
        }


        /// <summary>
        /// 
        /// </summary>
        public float[] NormalizedComponentsBySize
        {
            get { return _normalizedcomponentsbysize; }
            set { _normalizedcomponentsbysize = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public float[] NormalizedComponents
        {
            get { return _normalizedcomponents; }
            set { _normalizedcomponents = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<HashSet<int>> ConnectedComponents
        {
            get { return _connectedcomponents; }
            set { _connectedcomponents = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public int ConnectedComponentsCount
        {
            get { return _connectedcomponentscount; }
            set { _connectedcomponentscount = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ClosuresCount
        {
            get { return _closurescount; }
            set { _closurescount = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public float ClosureRate
        {
            get { return (float)_closurescount / (float)_graph.EdgeCount; }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<int> Sources
        {
            get { return _sources; }
            set { _sources = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int SourcesCount
        {
            get { return _sources.Count; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int[] Depths
        {
            get { return _depths; }
            set { _depths = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public float[] NormalizedDepths
        {
            get { return _normalizeddepths; }
            set { _normalizeddepths = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public int MaxDepth
        {
            get { return _maxdepth; }
            set { _maxdepth = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int DeepVerticesCount
        {
            get { return _deepverticescount; }
            set { _deepverticescount = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int UnreachableVerticesCount
        {
            get { return _unreachablevertices.Count; }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<int> UnreachableVertices
        {
            get { return _unreachablevertices; }
            set { _unreachablevertices = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public int EdgelessVerticesCount
        {
            get { return _edgelessvertices.Count; }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<int> EdgelessVertices
        {
            get { return _edgelessvertices; }
            set { _edgelessvertices = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Vector3[] Vertices
        {
            get { return _vertices; }
            set { _vertices = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<int> LineIndices
        {
            get { return _lineindices; }
            set { _lineindices = value; }
        }



        /// <summary>
        /// 
        /// </summary>
        public void Initialize()
        {
            _graph = new EdgeGraph();
            _vertexObjs = new List<V>(_graph.VertexCount);
            _edgeObjs = new List<E>(_graph.EdgeCount);
            _vertices = new Vector3[_graph.VertexCount];
            _lineindices = new List<int>();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Initialize(int vertexcount)
        {
            _graph = new EdgeGraph(vertexcount);
            _vertexObjs = new List<V>(vertexcount);
            for (int i = 0; i < vertexcount; i++)
            {
                _graph.AddVertex();
            }

            _edgeObjs = new List<E>();
            _vertices = new Vector3[vertexcount];
            _lineindices = new List<int>();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Initialize(EdgeGraph graph)
        {
            _graph = graph;
            _vertexObjs = new List<V>(_graph.VertexCount);
            for (int i = 0; i < _graph.VertexCount; i++)
            {
                _graph.AddVertex();
            }

            _edgeObjs = new List<E>(_graph.EdgeCount);
            _vertices = new Vector3[_graph.VertexCount];
            _lineindices = new List<int>();
        }
    }
}