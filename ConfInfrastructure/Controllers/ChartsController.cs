using ConfDomain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConfInfrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private record CountByYearResponseItem(string Year, int Count);

        private readonly DbconappContext conferenceContext;

        public ChartsController(DbconappContext confContext)
        {
            this.conferenceContext = confContext;
        }

        [HttpGet("countByYear")]
        public async Task<JsonResult> GetCountByYearAsync(CancellationToken cancellationToken)
        {
            var responseItems = await conferenceContext
                .Conferences
                .GroupBy(conference => conference.Date.Year)
                .Select(group => new CountByYearResponseItem(group.Key.ToString(), group.Count()))
                .ToListAsync(cancellationToken);

            return new JsonResult(responseItems);
        }
    }   
}
