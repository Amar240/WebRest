using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebRest.EF.Data;
using WebRest.EF.Models;

namespace WebRestAPI.Controllers.UD;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase, iController<Product>
{
    private readonly WebRestOracleContext _context;
    private readonly IMapper _mapper;

    public ProductController(WebRestOracleContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> Get()
    {
        var lst = await _context.Products.ToListAsync();
        return Ok(lst);
    }

    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var itm = await _context.Products
            .FirstOrDefaultAsync(x => x.ProductId == id);

        if (itm == null)
            return NotFound($"Product with ID '{id}' not found.");

        return Ok(itm);
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var itm = await _context.Products
            .FirstOrDefaultAsync(x => x.ProductId == id);

        if (itm == null)
            return NotFound($"Product with ID '{id}' not found.");

        _context.Products.Remove(itm);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Product _Product)
    {
        var trans = _context.Database.BeginTransaction();

        try
        {
            var itm = await _context.Products.AsNoTracking()
                .FirstOrDefaultAsync(x => x.ProductId == _Product.ProductId);

            if (itm != null)
            {
                itm = _mapper.Map<Product>(_Product);

                /* Optional manual mappings:
                itm.ProductName = _Product.ProductName;
                itm.ProductDesc = _Product.ProductDesc;
                itm.ProductProductStatusId = _Product.ProductProductStatusId;
                itm.ProductCrtdId = _Product.ProductCrtdId;
                itm.ProductCrtdDt = _Product.ProductCrtdDt;
                itm.ProductUpdtId = _Product.ProductUpdtId;
                itm.ProductUpdtDt = _Product.ProductUpdtDt;
                */

                _context.Products.Update(itm);
                await _context.SaveChangesAsync();
                trans.Commit();
            }
            else
            {
                return NotFound($"Product with ID '{_Product.ProductId}' not found.");
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
    public async Task<IActionResult> Post([FromBody] Product _Product)
    {
        var trans = _context.Database.BeginTransaction();

        try
        {
            _Product.ProductId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            _context.Products.Add(_Product);
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
