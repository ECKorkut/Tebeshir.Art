using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Tebeshir.Art.Catalog.DataLayer;
using Tebeshir.Art.Catalog.DataLayer.Model;

namespace Tebeshir.Art.Catalog.Controller
{
    [Authorize(AuthenticationSchemes= JwtBearerDefaults.AuthenticationScheme,Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private CatalogDbContext context;
        public CatalogController(CatalogDbContext context)
        {
            this.context = context;
        }
        [HttpGet("GetCatalog")]
        [ProducesResponseType(typeof(IEnumerable<CatalogModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CatalogModel>>> GetCatalog()
        {
            var products = await context.CatalogModels.ToListAsync();

            return Ok(products);
        }
    }
}
