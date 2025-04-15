using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebRest.EF.Data;
using WebRest.EF.Models;

namespace WebRestAPI.Controllers.UD;

[ApiController]
[Route("api/[controller]")]
public class OrderStatusController : ControllerBase, iController<OrderStatus>
{
    private readonly WebRestOracleContext _context;
    private readonly IMapper _mapper;

    public OrderStatusController(WebRestOracleContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> Get()
    {
        var lst = await _context.OrderStatuses.ToListAsync();
        return Ok(lst);
    }

    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var itm = await _context.OrderStatuses
            .FirstOrDefaultAsync(x => x.OrderStatusId == id);

        if (itm == null)
            return NotFound($"OrderStatus with ID '{id}' not found.");

        return Ok(itm);
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var itm = await _context.OrderStatuses
            .FirstOrDefaultAsync(x => x.OrderStatusId == id);

        if (itm == null)
            return NotFound($"OrderStatus with ID '{id}' not found.");

        _context.OrderStatuses.Remove(itm);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] OrderStatus _OrderStatus)
    {
        var trans = _context.Database.BeginTransaction();

        try
        {
            var itm = await _context.OrderStatuses.AsNoTracking()
                .FirstOrDefaultAsync(x => x.OrderStatusId == _OrderStatus.OrderStatusId);

            if (itm != null)
            {
                itm = _mapper.Map<OrderStatus>(_OrderStatus);

                /* Optional manual mappings if needed:
                itm.OrderStatusDesc = _OrderStatus.OrderStatusDesc;
                itm.OrderStatusNextOrderStatusId = _OrderStatus.OrderStatusNextOrderStatusId;
                itm.OrderStatusCrtdId = _OrderStatus.OrderStatusCrtdId;
                itm.OrderStatusCrtdDt = _OrderStatus.OrderStatusCrtdDt;
                itm.OrderStatusUpdtId = _OrderStatus.OrderStatusUpdtId;
                itm.OrderStatusUpdtDt = _OrderStatus.OrderStatusUpdtDt;
                */

                _context.OrderStatuses.Update(itm);
                await _context.SaveChangesAsync();
                trans.Commit();
            }
            else
            {
                return NotFound($"OrderStatus with ID '{_OrderStatus.OrderStatusId}' not found.");
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
    public async Task<IActionResult> Post([FromBody] OrderStatus _OrderStatus)
    {
        var trans = _context.Database.BeginTransaction();

        try
        {
            _OrderStatus.OrderStatusId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            _context.OrderStatuses.Add(_OrderStatus);
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
