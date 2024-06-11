using ShortestRouteAPI.Models;
using ShortestRouteAPI.Models.Dtos;

namespace ShortestRouteAPI.Services
{
    public class ShortestPathOptimizerService
    {
        public ShortestPathData ShortestPath(string fromNodeName, string toNodeName, List<Node> graphNodes)
        {
            if (string.IsNullOrEmpty(fromNodeName) || string.IsNullOrEmpty(toNodeName) || graphNodes == null || graphNodes.Count == 0)
            {
                throw new ArgumentException("Invalid input parameters.");
            }

            Dictionary<string, Node> nodes = [];
            foreach (var node in graphNodes)
            {
                nodes[node.Name] = node;
            }

            if (!nodes.ContainsKey(fromNodeName) || !nodes.ContainsKey(toNodeName))
            {
                throw new ArgumentException("Invalid node names provided.");
            }

            Dictionary<string, int> distances = [];
            Dictionary<string, string> previousNodes = [];
            HashSet<string> unvisited = [];

            foreach (var node in nodes)
            {
                distances[node.Key] = int.MaxValue;
                previousNodes[node.Key] = null;
                unvisited.Add(node.Key);
            }

            distances[fromNodeName] = 0;

            while (unvisited.Count > 0)
            {
                var currentNode = GetNodeWithSmallestDistance(distances, unvisited);
                unvisited.Remove(currentNode);

                if (currentNode == toNodeName)
                    break;

                foreach (var neighbor in nodes[currentNode].Edges)
                {
                    var newDistance = distances[currentNode] + neighbor.Value;
                    if (newDistance < distances[neighbor.Key])
                    {
                        distances[neighbor.Key] = newDistance;
                        previousNodes[neighbor.Key] = currentNode;
                    }
                }
            }

            return ConstructPath(toNodeName, previousNodes, distances);
        }

        private string GetNodeWithSmallestDistance(Dictionary<string, int> distances, HashSet<string> unvisited)
        {
            var minDistance = int.MaxValue;
            string minNode = null;

            foreach (var node in unvisited)
            {
                if (distances[node] < minDistance)
                {
                    minDistance = distances[node];
                    minNode = node;
                }
            }

            return minNode;
        }

        private ShortestPathData ConstructPath(string toNodeName, Dictionary<string, string> previous, Dictionary<string, int> distances)
        {
            var path = new List<string>();

            var currentNode = toNodeName;

            while (currentNode != null)
            {
                path.Insert(0, currentNode);
                currentNode = previous[currentNode];
            }

            return new ShortestPathData
            {
                NodeNames = path,
                Distance = distances[toNodeName]
            };
        }
    }
}