using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebRest.EF.Data;
using WebRest.EF.Models;

namespace WebRestAPI.Controllers.UD;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase, iController<Order>
{
    private readonly WebRestOracleContext _context;
    private readonly IMapper _mapper;

    public OrderController(WebRestOracleContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> Get()
    {
        var lst = await _context.Orders.ToListAsync();
        return Ok(lst);
    }

    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var itm = await _context.Orders
            .FirstOrDefaultAsync(x => x.OrdersId == id);

        if (itm == null)
            return NotFound($"Order with ID '{id}' not found.");

        return Ok(itm);
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var itm = await _context.Orders
            .FirstOrDefaultAsync(x => x.OrdersId == id);

        if (itm == null)
            return NotFound($"Order with ID '{id}' not found.");

        _context.Orders.Remove(itm);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Order _Order)
    {
        var trans = _context.Database.BeginTransaction();

        try
        {
            var itm = await _context.Orders.AsNoTracking()
                .FirstOrDefaultAsync(x => x.OrdersId == _Order.OrdersId);

            if (itm != null)
            {
                itm = _mapper.Map<Order>(_Order);

                /* Optional manual mappings if needed:
                itm.OrdersDate = _Order.OrdersDate;
                itm.OrdersCustomerId = _Order.OrdersCustomerId;
                itm.OrdersCrtdId = _Order.OrdersCrtdId;
                itm.OrdersCrtdDt = _Order.OrdersCrtdDt;
                itm.OrdersUpdtId = _Order.OrdersUpdtId;
                itm.OrdersUpdtDt = _Order.OrdersUpdtDt;
                */

                _context.Orders.Update(itm);
                await _context.SaveChangesAsync();
                trans.Commit();
            }
            else
            {
                return NotFound($"Order with ID '{_Order.OrdersId}' not found.");
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
    public async Task<IActionResult> Post([FromBody] Order _Order)
    {
        var trans = _context.Database.BeginTransaction();

        try
        {
            _Order.OrdersId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            _context.Orders.Add(_Order);
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
