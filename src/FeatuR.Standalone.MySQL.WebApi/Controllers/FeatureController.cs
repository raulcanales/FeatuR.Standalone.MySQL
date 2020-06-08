using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FeatuR.Standalone.MySQL.WebApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class FeatureController : ControllerBase
    {
        private readonly IFeatureService _featureService;
        private readonly ILogger<FeatureController> _logger;

        public FeatureController(IFeatureService featureService, ILogger<FeatureController> logger)
        {
            _featureService = featureService;
            _logger = logger;
        }
        public static readonly string DefaultIsFeatureEnabledEndpoint = "feature/{featureId}";
        public static readonly string DefaultGetAllEnabledFeaturesEndpoint = "features";
        public static readonly string DefaultEvaluateFeaturesEndpoint = "features/evaluate";

        [HttpGet("")]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpGet("feature/{featureId}/isenabled")]
        public async Task<IActionResult> IsFeatureEnabledAsync(string featureId, CancellationToken token = default)
        {
            var context = GetFeatureContext();
            return Ok(await _featureService.IsFeatureEnabledAsync(featureId, context, token).ConfigureAwait(false));
        }

        [HttpPost("features/evaluate")]
        public async Task<IActionResult> EvaluateFeaturesAsync(string[] features, CancellationToken token = default)
        {
            var context = GetFeatureContext();
            return Ok(await _featureService.EvaluateFeaturesAsync(features, context, token).ConfigureAwait(false));
        }

        [HttpGet("features")]
        public async Task<IActionResult> GetEnabledFeaturesAsync(CancellationToken token = default)
        {
            var context = GetFeatureContext();
            return Ok(await _featureService.GetEnabledFeaturesAsync(context, token).ConfigureAwait(false));
        }

        private IFeatureContext GetFeatureContext()
        {
            var context = new FeatureContext();

            if (HttpContext?.Request?.Headers != null)
                foreach (var header in HttpContext.Request.Headers)
                {
                    context.Parameters.TryAdd(header.Key, header.Value.FirstOrDefault());
                }

            return context;
        }
    }
}
