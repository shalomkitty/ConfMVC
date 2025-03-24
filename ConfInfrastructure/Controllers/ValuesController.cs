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
        private readonly DbconappContext dbconappContext;
        public ChartsController(DbconappContext dbconappContext)
        {
            this.dbconappContext = dbconappContext;
        }

        [HttpGet("countByYear")]
        public async Task<JsonResult> GetCountByYearAsync(CancellationToken cancellationToken)
        {
            var responseItems = await dbconappContext
                .Conferences
                .GroupBy(conference => conference.Date)
                .Select(group => new CountByYearResponseItem(group.Key.ToString(), group.Count()))
                .ToListAsync(cancellationToken);

            return new JsonResult(responseItems);
        }
    }

}
