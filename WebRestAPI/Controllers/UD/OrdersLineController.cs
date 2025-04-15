using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebRest.EF.Data;
using WebRest.EF.Models;

namespace WebRestAPI.Controllers.UD;

[ApiController]
[Route("api/[controller]")]
public class OrdersLineController : ControllerBase, iController<OrdersLine>
{
    private readonly WebRestOracleContext _context;
    private readonly IMapper _mapper;

    public OrdersLineController(WebRestOracleContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> Get()
    {
        var lst = await _context.OrdersLines.ToListAsync();
        return Ok(lst);
    }

    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var itm = await _context.OrdersLines
            .FirstOrDefaultAsync(x => x.OrdersLineId == id);

        if (itm == null)
            return NotFound($"OrdersLine with ID '{id}' not found.");

        return Ok(itm);
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var itm = await _context.OrdersLines
            .FirstOrDefaultAsync(x => x.OrdersLineId == id);

        if (itm == null)
            return NotFound($"OrdersLine with ID '{id}' not found.");

        _context.OrdersLines.Remove(itm);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] OrdersLine _OrdersLine)
    {
        var trans = _context.Database.BeginTransaction();

        try
        {
            var itm = await _context.OrdersLines.AsNoTracking()
                .FirstOrDefaultAsync(x => x.OrdersLineId == _OrdersLine.OrdersLineId);

            if (itm != null)
            {
                itm = _mapper.Map<OrdersLine>(_OrdersLine);

                /* Optional manual mappings if needed:
                itm.OrdersLineOrdersId = _OrdersLine.OrdersLineOrdersId;
                itm.OrdersLineProduct = _OrdersLine.OrdersLineProduct;
                itm.OrdersLineQuantity = _OrdersLine.OrdersLineQuantity;
                itm.OrdersLinePrice = _OrdersLine.OrdersLinePrice;
                itm.OrdersLineCrtdId = _OrdersLine.OrdersLineCrtdId;
                itm.OrdersLineCrtdDt = _OrdersLine.OrdersLineCrtdDt;
                itm.OrdersLineUpdtId = _OrdersLine.OrdersLineUpdtId;
                itm.OrdersLineUpdtDt = _OrdersLine.OrdersLineUpdtDt;
                */

                _context.OrdersLines.Update(itm);
                await _context.SaveChangesAsync();
                trans.Commit();
            }
            else
            {
                return NotFound($"OrdersLine with ID '{_OrdersLine.OrdersLineId}' not found.");
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
    public async Task<IActionResult> Post([FromBody] OrdersLine _OrdersLine)
    {
        var trans = _context.Database.BeginTransaction();

        try
        {
            _OrdersLine.OrdersLineId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            _context.OrdersLines.Add(_OrdersLine);
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
