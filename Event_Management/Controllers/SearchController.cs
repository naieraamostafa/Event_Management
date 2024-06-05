using Event_Management.Services;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet("search")] 
        public IActionResult SearchEvents([FromQuery] string name, [FromQuery] DateTime? date, [FromQuery] string location)
        {
            var events = _searchService.SearchEvents(name, date, location);
            return Ok(events);
        }
    }
}
