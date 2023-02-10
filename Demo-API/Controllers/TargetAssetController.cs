using Demo_API.Models;
using Demo_API.Services.BaseHttpReqester;
using Microsoft.AspNetCore.Mvc;

namespace Demo_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TargetAssetController : ControllerBase
    {
        private readonly IBaseHttpReqester baseHttpReqester;
        private readonly ILogger<TargetAssetController> _logger;

        public TargetAssetController(
            IBaseHttpReqester baseHttpReqester,
            ILogger<TargetAssetController> logger
            )
        {
            this.baseHttpReqester = baseHttpReqester;
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<EnrichedTargetAsset>> Get()
        {
            try
            {
                _logger.Log(LogLevel.Information, DateTime.Now.ToString() + "Request recieved from: " + Request?.HttpContext.Connection.RemoteIpAddress);
                
                string url = "https://06ba2c18-ac5b-4e14-988c-94f400643ebf.mock.pstmn.io/targetAsset";

                _logger.Log(LogLevel.Information, "Calling /targetAsset API.");
                var httpResponseMessage = await baseHttpReqester.Get_Request(url);

                List<TargetAsset>? assets = null;

                List<EnrichedTargetAsset> enrichedTargetAssets = new List<EnrichedTargetAsset>();

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    _logger.Log(LogLevel.Information, "/targetAsset API request succeeded.");
                    using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                    // Deserializing string json to json
                    assets = await System.Text.Json.JsonSerializer.DeserializeAsync<List<TargetAsset>>(contentStream);

                    if (assets != null)
                    {
                        _logger.Log(LogLevel.Information, $"Result count: {assets.Count}");

                        // iterator to update all assets
                        foreach (var asset in assets)
                        {
                            if (asset != null)
                            {
                                var newAsset = asset.toEnnrichedTargetAsset();
                                newAsset.isStartable = false;
                                if (DateTime.Now.Day == 3 && asset.status == "Running")
                                {
                                    newAsset.isStartable = true;
                                }

                                List<int> visted_ids = new List<int>();
                                var parent = asset;
                                visted_ids.Add(parent.id);

                                // Iterator to count parentTargetAssetCount
                                while (parent.parentId != null)
                                {
                                    parent = assets
                                        .Where(x => x != null && x.id == parent.parentId)?
                                        .FirstOrDefault();

                                    if (parent == null || visted_ids.IndexOf(parent.id) != -1)
                                    {
                                        break;
                                    }

                                    visted_ids.Add(parent.id);
                                }

                                newAsset.parentTargetAssetCount = visted_ids.Count();


                                enrichedTargetAssets.Add(newAsset);
                            }
                        }
                    }
                    return enrichedTargetAssets;

                }
                else
                {
                    _logger.Log(LogLevel.Error, $"{DateTime.Now} Api request failed.");
                    throw new HttpRequestException("Api request failed", null, System.Net.HttpStatusCode.NotFound);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
