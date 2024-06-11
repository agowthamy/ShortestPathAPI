namespace ShortestRouteAPI.Models.Dtos
{
    public class ShortestRouteRequest
    {
        public required string FromNode { get; set; }
        public required string ToNode { get; set; }
        public required List<Node> GraphNodes { get; set; }
    }
}