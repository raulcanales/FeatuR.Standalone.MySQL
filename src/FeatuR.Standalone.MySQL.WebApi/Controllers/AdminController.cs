using FeatuR.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace FeatuR.Standalone.MySQL.WebApi.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : Controller
    {
        private readonly FeatuRDbContext _context;
        private readonly ILogger<AdminController> _logger;

        public AdminController(FeatuRDbContext context, ILogger<AdminController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("feature/{featureId}")]
        public async Task<IActionResult> Get(string featureId, CancellationToken token = default)
        {
            return Ok(await _context.Features
                .SingleOrDefaultAsync(f => f.Id == featureId, token)
                .ConfigureAwait(false));
        }

        [HttpGet("features")]
        public async Task<IActionResult> GetAll(CancellationToken token = default)
        {
            return Ok(await _context.Features
                .ToListAsync(token)
                .ConfigureAwait(false));
        }

        [HttpPost("feature")]
        public async Task<IActionResult> Post(Feature feature, CancellationToken token = default)
        {
            await _context.Features.AddAsync(feature, token).ConfigureAwait(false);
            await _context.SaveChangesAsync(token).ConfigureAwait(false);
            return CreatedAtAction("Get", new { featureId = feature.Id });
        }

        [HttpPut("feature/{featureId}")]
        public async Task<IActionResult> Put(string featureId, Feature feature, CancellationToken token = default)
        {
            _context.Features.Update(feature);
            await _context.SaveChangesAsync(token).ConfigureAwait(false);
            return NoContent();
        }

        [HttpDelete("feature/{featureId}")]
        public async Task<IActionResult> Delete(string featureId, CancellationToken token = default)
        {
            var feature = await _context.Features.SingleOrDefaultAsync(f => f.Id == featureId, token);
            _context.Features.Remove(feature);
            await _context.SaveChangesAsync(token).ConfigureAwait(false);
            return NoContent();
        }
    }
}
