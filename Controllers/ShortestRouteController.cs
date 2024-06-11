using Microsoft.AspNetCore.Mvc;
using ShortestRouteAPI.Models.Dtos;
using ShortestRouteAPI.Services;

namespace ShortestRouteAPI.Controllers
{
    [ApiController]
    [Route("api/shortestpath")]
    public class ShortestRouteController : ControllerBase
    {
        private readonly ShortestPathOptimizerService _shortestPathOptimizerService;
        public ShortestRouteController(ShortestPathOptimizerService shortestPathOptimizerService)
        {
            _shortestPathOptimizerService = shortestPathOptimizerService;
        }

        [HttpPost]
        public ActionResult<ShortestPathData> GetShortestPath([FromBody] ShortestRouteRequest request)
        {
            try
            {
                var result = _shortestPathOptimizerService.ShortestPath(request.FromNode, request.ToNode, request.GraphNodes);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }
    }
}