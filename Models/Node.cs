namespace ShortestRouteAPI.Models
{
    public class Node
    {
        public required string Name { get; set; }
        public Dictionary<string, int> Edges { get; set; } = [];
    }
}