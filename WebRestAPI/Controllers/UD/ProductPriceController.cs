using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebRest.EF.Data;
using WebRest.EF.Models;

namespace WebRestAPI.Controllers.UD;

[ApiController]
[Route("api/[controller]")]
public class ProductPriceController : ControllerBase, iController<ProductPrice>
{
    private readonly WebRestOracleContext _context;
    private readonly IMapper _mapper;

    public ProductPriceController(WebRestOracleContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> Get()
    {
        var lst = await _context.ProductPrices.ToListAsync();
        return Ok(lst);
    }

    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var itm = await _context.ProductPrices
            .FirstOrDefaultAsync(x => x.ProductPriceId == id);

        if (itm == null)
            return NotFound($"ProductPrice with ID '{id}' not found.");

        return Ok(itm);
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var itm = await _context.ProductPrices
            .FirstOrDefaultAsync(x => x.ProductPriceId == id);

        if (itm == null)
            return NotFound($"ProductPrice with ID '{id}' not found.");

        _context.ProductPrices.Remove(itm);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] ProductPrice _ProductPrice)
    {
        var trans = _context.Database.BeginTransaction();

        try
        {
            var itm = await _context.ProductPrices.AsNoTracking()
                .FirstOrDefaultAsync(x => x.ProductPriceId == _ProductPrice.ProductPriceId);

            if (itm != null)
            {
                itm = _mapper.Map<ProductPrice>(_ProductPrice);

                /* Optional manual mappings:
                itm.ProductPriceProductId = _ProductPrice.ProductPriceProductId;
                itm.ProductPriceEffDate = _ProductPrice.ProductPriceEffDate;
                itm.ProductPricePrice = _ProductPrice.ProductPricePrice;
                itm.ProductPriceCrtdId = _ProductPrice.ProductPriceCrtdId;
                itm.ProductPriceCrtdDt = _ProductPrice.ProductPriceCrtdDt;
                itm.ProductPriceUpdtId = _ProductPrice.ProductPriceUpdtId;
                itm.ProductPriceUpdtDt = _ProductPrice.ProductPriceUpdtDt;
                */

                _context.ProductPrices.Update(itm);
                await _context.SaveChangesAsync();
                trans.Commit();
            }
            else
            {
                return NotFound($"ProductPrice with ID '{_ProductPrice.ProductPriceId}' not found.");
            }
        }
        catch (Exception ex)
        {
            trans.Rollback();
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProductPrice _ProductPrice)
    {
        var trans = _context.Database.BeginTransaction();

        try
        {
            _ProductPrice.ProductPriceId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            _context.ProductPrices.Add(_ProductPrice);
            await _context.SaveChangesAsync();
            trans.Commit();
        }
        catch (Exception ex)
        {
            trans.Rollback();
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

        return Ok();
    }
}
