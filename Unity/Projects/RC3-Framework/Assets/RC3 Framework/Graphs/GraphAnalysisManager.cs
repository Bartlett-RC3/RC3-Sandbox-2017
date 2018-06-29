using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using RC3.Graphs;
using RC3.WFC;
using RC3.Unity.WFCDemo;


namespace RC3.Graphs
{

    [CreateAssetMenu(menuName = "RC3/WFC Demo/GraphAnalysisManager")]
    public class GraphAnalysisManager : MonoBehaviour
    {
        #region Member Variables
        private ProcessingUtil _graphprocessing = new ProcessingUtil();

        [SerializeField]
        private TileGraphExtractor _graphextractor;

        [SerializeField]
        private TileModelManager _tilemodelmanager;
        private TileModel _tilemodel;

        [SerializeField]
        private SharedAnalysisEdgeGraph _analysisgraph;
        private GraphVisualizer _graphvisualizer;

        private int _graphviz = 0;

        #endregion

        #region Constructors
        private void Awake()
        {
            if (_tilemodelmanager != null)
            {
                _tilemodel = _tilemodelmanager.TileModel;
            }

            _analysisgraph.Initialize();

            _graphvisualizer = GetComponent<GraphVisualizer>();
        }

        #endregion

        #region Public Methods
        #endregion

        #region Private Methods

        void Start()
        {
            EdgeGraph testgraph = new EdgeGraph(15);
            ProcessingUtil graphprocessing = new ProcessingUtil();

            for (int i = 0; i < 20; i++)
            {
                testgraph.AddVertex();
            }

            //TestGraph | Build graph
            //first connected component set of edges
            testgraph.AddEdge(1, 2);
            testgraph.AddEdge(2, 3);
            testgraph.AddEdge(3, 1);
            testgraph.AddEdge(3, 5);
            testgraph.AddEdge(5, 6);
            testgraph.AddEdge(6, 4);
            testgraph.AddEdge(4, 5);
            testgraph.AddEdge(7, 6);
            testgraph.AddEdge(3, 8);
            testgraph.AddEdge(8, 1);
            testgraph.AddEdge(6, 9);
            testgraph.AddEdge(9, 4);
            testgraph.AddEdge(8, 10);
            testgraph.AddEdge(10, 11);
            testgraph.AddEdge(11, 12);
            testgraph.AddEdge(12, 10);

            //second component (not connected to first) 
            testgraph.AddEdge(15, 18);
            testgraph.AddEdge(18, 19);
            testgraph.AddEdge(17, 18);

            //TestGraph | Analysis
            int componentcount = 0;
            int closurecount = 0;
            List<HashSet<int>> components = new List<HashSet<int>>();
            _graphprocessing.CountClosures(testgraph, out componentcount, out closurecount, out components);
            float closurerate = (float)closurecount / (float)testgraph.EdgeCount;

            //TestGraph | Debug Print Results
            Debug.Log("TestGraph | Components Count = " + componentcount);
            for (int i = 0; i < components.Count; i++)
            {
                HashSet<int> set = components[i];
                string setstring = string.Join(",", components[i]);
                Debug.Log("TestGraph | ConnectedComponent# " + (i + 1) + " = " + setstring);
            }

            float[] normalizedcomponents = _graphprocessing.RemapComponentsToArray(testgraph, components);
            string normalizedcomponentsstring = string.Join(",", normalizedcomponents);
            Debug.Log("TestGraph | NormalizedComponents = " + normalizedcomponentsstring);

            Debug.Log("TestGraph | Closures Count = " + closurecount);
            Debug.Log("TestGraph | Closures Rate = " + closurerate);

        }

        void Update()
        {
            KeyPressMethod();
        }

        private void UpdateAnalysis()
        {

            if (_graphextractor != null && _tilemodelmanager != null)
            {
                if (_tilemodelmanager.Status == CollapseStatus.Complete)
                {
                    _graphextractor.ExtractSharedEdgeGraph(_analysisgraph);

                    //Extracted Graph | Analysis
                    //analyze/get # of closures / strongly connected components
                    int closurecount = 0;
                    int componentcount = 0;
                    List<HashSet<int>> connectedcomponents = new List<HashSet<int>>();
                    _graphprocessing.CountClosures(_analysisgraph.Graph, out componentcount, out closurecount, out connectedcomponents);
                    float closurerate = (float)closurecount / (float)_analysisgraph.Graph.EdgeCount;

                    //normalized/remapped components to an array for graph coloring
                    float[] normalizedcomponents = _graphprocessing.RemapComponentsToArray(_analysisgraph.Graph, connectedcomponents);
                    float[] normalizedcomponentsbysize = _graphprocessing.RemapComponentsSizeToArray(_analysisgraph.Graph, connectedcomponents);

                    //analyze/get 1) ground support sources, 2) list of vertex depths 3) max depth 
                    List<int> sources = _graphprocessing.GetGroundSources(_analysisgraph.Graph, _analysisgraph.Vertices, 2f);
                    int[] depths = _graphprocessing.DepthsFromGroundSources(_analysisgraph.Graph, _analysisgraph.Vertices, 2f);
                    int maxdepth = _graphprocessing.MaxDepth(depths);

                    //analyze/get 1) unreachable vertices, 2) remapped vertex depths between 0,1, 3) edgeless vertices
                    float[] normalizeddepths = new float[_analysisgraph.Graph.VertexCount];
                    List<int> unreachablevertices = new List<int>();
                    List<int> edgelessvertices = new List<int>();
                    _graphprocessing.RemapGraphDepths(_analysisgraph.Graph, depths, 0, 1, out normalizeddepths, out unreachablevertices, out edgelessvertices);

                    //store analysis in the shared analysis graph scriptable object - VIEW THIS DATA ON A UI CANVAS
                    _analysisgraph.ClosuresCount = closurecount;
                    _analysisgraph.ConnectedComponents = connectedcomponents;
                    _analysisgraph.NormalizedComponents = normalizedcomponents;
                    _analysisgraph.NormalizedComponentsBySize = normalizedcomponentsbysize;
                    _analysisgraph.ConnectedComponentsCount = componentcount;
                    _analysisgraph.Sources = sources;
                    _analysisgraph.Depths = depths;
                    _analysisgraph.NormalizedDepths = normalizeddepths;
                    _analysisgraph.MaxDepth = maxdepth;
                    _analysisgraph.UnreachableVertices = unreachablevertices;
                    _analysisgraph.EdgelessVertices = edgelessvertices;

                    //Extracted Graph | Debug Print Results
                    Debug.Log("Exracted Graph | ComponentsCount = " + _analysisgraph.ConnectedComponentsCount);
                    for (int i = 0; i < connectedcomponents.Count; i++)
                    {
                        HashSet<int> set = connectedcomponents[i];
                        string setstring = string.Join(",", connectedcomponents[i]);
                        Debug.Log("Exracted Graph | ConnectedComponent# " + (i + 1) + " = " + setstring);
                    }

                    string normalizedcomponentsstring = string.Join(",", _analysisgraph.NormalizedComponents);
                    string normalizedcomponentsbysizestring = string.Join(",", _analysisgraph.NormalizedComponentsBySize);

                    Debug.Log("Exracted Graph | NormalizedComponents = " + normalizedcomponentsstring);
                    Debug.Log("Exracted Graph | NormalizedComponentsBySize = " + normalizedcomponentsbysizestring);

                    Debug.Log("Exracted Graph | Closures Count = " + _analysisgraph.ClosuresCount);
                    Debug.Log("Exracted Graph | Closures Rate = " + _analysisgraph.ClosureRate);

                    string sourcesstring = string.Join(",", _analysisgraph.Sources);
                    string depthsstring = string.Join(",", _analysisgraph.Depths);
                    Debug.Log("Exracted Graph | Depths = " + depthsstring);
                    Debug.Log("Exracted Graph | Max Depth = " + _analysisgraph.MaxDepth);
                    Debug.Log("Exracted Graph | SourcesCount = " + _analysisgraph.SourcesCount);
                    Debug.Log("Exracted Graph | Sources = " + sourcesstring);

                    string normalizeddepthsstring = string.Join(",", _analysisgraph.NormalizedDepths);
                    string unreachablevrtsstring = string.Join(",", _analysisgraph.UnreachableVertices);
                    string edgelessvrtsstring = string.Join(",", _analysisgraph.EdgelessVertices);

                    Debug.Log("Exracted Graph | Normalized Depths = " + normalizeddepthsstring);
                    Debug.Log("Exracted Graph | Unreachable Vertices Count = " + _analysisgraph.UnreachableVerticesCount);
                    Debug.Log("Exracted Graph | Unreachable Vertices = " + unreachablevrtsstring);
                    Debug.Log("Exracted Graph | Edgeless Vertices Count = " + _analysisgraph.EdgelessVerticesCount);
                    Debug.Log("Exracted Graph | Edgeless Vertices = " + edgelessvrtsstring);

                }
            }
            else
            {
                Debug.Log("No Graph Extractor OR WFC Incomplete");
            }
        }

        private void UpdateGraphMesh()
        {
            if (_graphvisualizer != null)
            {
                _graphvisualizer.CreateMesh();
            }

            else
            {
                Debug.Log("No Graph Visualizer Component Attached!");
            }
        }


        private void KeyPressMethod()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                UpdateAnalysis();
                UpdateGraphMesh();
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                if (_graphviz < 2)
                {
                    _graphviz++;
                    if (_graphviz == 1)
                    {
                        _graphvisualizer.VizMode = GraphVisualizer.RenderMode.Components;
                    }

                    if (_graphviz == 2)
                    {
                        _graphvisualizer.VizMode = GraphVisualizer.RenderMode.ComponentsSize;
                    }
                }
                else
                {
                    _graphviz = 0;
                    _graphvisualizer.VizMode = GraphVisualizer.RenderMode.DepthFromSource;
                }

                _graphvisualizer.SetVizColors();
            }
        }

        #endregion

        #region Public Properties

        public IEdgeGraph Graph
        {
            get { return _analysisgraph.Graph; }
        }

        public SharedAnalysisEdgeGraph AnalysisGraph
        {
            get { return _analysisgraph; }
        }

        #endregion

    }
}
