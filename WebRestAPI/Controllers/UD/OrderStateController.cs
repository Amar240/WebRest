using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebRest.EF.Data;
using WebRest.EF.Models;

namespace WebRestAPI.Controllers.UD;

[ApiController]
[Route("api/[controller]")]
public class OrderStateController : ControllerBase, iController<OrderState>
{
    private readonly WebRestOracleContext _context;
    private readonly IMapper _mapper;

    public OrderStateController(WebRestOracleContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> Get()
    {
        var lst = await _context.OrderStates.ToListAsync();
        return Ok(lst);
    }

    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var itm = await _context.OrderStates
            .FirstOrDefaultAsync(x => x.OrderStateId == id);

        if (itm == null)
            return NotFound($"OrderState with ID '{id}' not found.");

        return Ok(itm);
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var itm = await _context.OrderStates
            .FirstOrDefaultAsync(x => x.OrderStateId == id);

        if (itm == null)
            return NotFound($"OrderState with ID '{id}' not found.");

        _context.OrderStates.Remove(itm);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] OrderState _OrderState)
    {
        var trans = _context.Database.BeginTransaction();

        try
        {
            var itm = await _context.OrderStates.AsNoTracking()
                .FirstOrDefaultAsync(x => x.OrderStateId == _OrderState.OrderStateId);

            if (itm != null)
            {
                itm = _mapper.Map<OrderState>(_OrderState);

                /* Optional manual mappings:
                itm.OrderStateOrdersId = _OrderState.OrderStateOrdersId;
                itm.OrderStateOrderStatusId = _OrderState.OrderStateOrderStatusId;
                itm.OrderStateEffDate = _OrderState.OrderStateEffDate;
                itm.OrderStateCrtdId = _OrderState.OrderStateCrtdId;
                itm.OrderStateCrtdDt = _OrderState.OrderStateCrtdDt;
                itm.OrderStateUpdtId = _OrderState.OrderStateUpdtId;
                itm.OrderStateUpdtDt = _OrderState.OrderStateUpdtDt;
                */

                _context.OrderStates.Update(itm);
                await _context.SaveChangesAsync();
                trans.Commit();
            }
            else
            {
                return NotFound($"OrderState with ID '{_OrderState.OrderStateId}' not found.");
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
    public async Task<IActionResult> Post([FromBody] OrderState _OrderState)
    {
        var trans = _context.Database.BeginTransaction();

        try
        {
            _OrderState.OrderStateId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            _context.OrderStates.Add(_OrderState);
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
