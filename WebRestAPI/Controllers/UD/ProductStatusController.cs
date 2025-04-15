using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebRest.EF.Data;
using WebRest.EF.Models;

namespace WebRestAPI.Controllers.UD;

[ApiController]
[Route("api/[controller]")]
public class ProductStatusController : ControllerBase, iController<ProductStatus>
{
    private readonly WebRestOracleContext _context;
    private readonly IMapper _mapper;

    public ProductStatusController(WebRestOracleContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> Get()
    {
        var lst = await _context.ProductStatuses.ToListAsync();
        return Ok(lst);
    }

    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var itm = await _context.ProductStatuses
            .FirstOrDefaultAsync(x => x.ProductStatusId == id);

        if (itm == null)
            return NotFound($"ProductStatus with ID '{id}' not found.");

        return Ok(itm);
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var itm = await _context.ProductStatuses
            .FirstOrDefaultAsync(x => x.ProductStatusId == id);

        if (itm == null)
            return NotFound($"ProductStatus with ID '{id}' not found.");

        _context.ProductStatuses.Remove(itm);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] ProductStatus _ProductStatus)
    {
        var trans = _context.Database.BeginTransaction();

        try
        {
            var itm = await _context.ProductStatuses.AsNoTracking()
                .FirstOrDefaultAsync(x => x.ProductStatusId == _ProductStatus.ProductStatusId);

            if (itm != null)
            {
                itm = _mapper.Map<ProductStatus>(_ProductStatus);

                /* Optional manual mappings:
                itm.ProductStatusDesc = _ProductStatus.ProductStatusDesc;
                itm.ProductStatusCrtdId = _ProductStatus.ProductStatusCrtdId;
                itm.ProductStatusCrtdDt = _ProductStatus.ProductStatusCrtdDt;
                itm.ProductStatusUpdtId = _ProductStatus.ProductStatusUpdtId;
                itm.ProductStatusUpdtDt = _ProductStatus.ProductStatusUpdtDt;
                */

                _context.ProductStatuses.Update(itm);
                await _context.SaveChangesAsync();
                trans.Commit();
            }
            else
            {
                return NotFound($"ProductStatus with ID '{_ProductStatus.ProductStatusId}' not found.");
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
    public async Task<IActionResult> Post([FromBody] ProductStatus _ProductStatus)
    {
        var trans = _context.Database.BeginTransaction();

        try
        {
            _ProductStatus.ProductStatusId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            _context.ProductStatuses.Add(_ProductStatus);
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
