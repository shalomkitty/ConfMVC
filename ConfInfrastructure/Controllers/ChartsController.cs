using ConfDomain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace ConfInfrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private record CountByYearResponseItem(string Year, int Count);
        private record CountByMonthResponseItem(string Month, int Count);
        private record CountByDayResponseItem(string Day, int Count);

        private readonly DbconappContext conferenceContext;

        public ChartsController(DbconappContext confContext, DbconappContext pubContext)
        {
            this.conferenceContext = confContext;
            this.conferenceContext = pubContext;
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

        [HttpGet("countByMonth")]
        public async Task<JsonResult> GetCountByMonthAsync(CancellationToken cancellationToken)
        {
            var responseItems = await conferenceContext
                .Conferences
                .GroupBy(conference => conference.Date.Month)
                .OrderBy(group => group.Key)
                .Select(group => new CountByMonthResponseItem(
                    CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(group.Key),
                    group.Count()))
                .ToListAsync(cancellationToken);

            return new JsonResult(responseItems);
        }

        [HttpGet("countByDay")]
        public async Task<JsonResult> GetCountByDayAsync(CancellationToken cancellationToken)
        {
            var responseItems = await conferenceContext
                .Publications
                .GroupBy(conference => conference.UploadDate.Day)
                .OrderBy(group => group.Key)
                .Select(group => new CountByDayResponseItem(
                    group.Key.ToString("d", CultureInfo.CurrentCulture),
                    group.Count()))
                .ToListAsync(cancellationToken);

            return new JsonResult(responseItems);
        }
    }
}