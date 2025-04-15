using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebRest.EF.Data;
using WebRest.EF.Models;

namespace WebRestAPI.Controllers.UD;

[ApiController]
[Route("api/[controller]")]
public class AddressController : ControllerBase, iController<Address>
{
    private WebRestOracleContext _context;
    private readonly IMapper _mapper;

    public AddressController(WebRestOracleContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> Get()
    {
        List<Address> lst = null;
        lst = await _context.Addresses.ToListAsync();
        return Ok(lst);
    }

    [HttpGet]
    [Route("Get/{ID}")]
    public async Task<IActionResult> Get(string ID)
    {
        var itm = await _context.Addresses
            .Where(x => x.AddressId == ID)
            .FirstOrDefaultAsync();

        return Ok(itm);
    }

    [HttpDelete]
    [Route("Delete/{ID}")]
    public async Task<IActionResult> Delete(string ID)
    {
        var itm = await _context.Addresses
            .Where(x => x.AddressId == ID)
            .FirstOrDefaultAsync();

        if (itm == null)
            return NotFound($"Address with ID '{ID}' not found.");

        _context.Addresses.Remove(itm);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Address _Address)
    {
        var trans = _context.Database.BeginTransaction();

        try
        {
            var itm = await _context.Addresses.AsNoTracking()
                .Where(x => x.AddressId == _Address.AddressId)
                .FirstOrDefaultAsync();

            if (itm != null)
            {
                itm = _mapper.Map<Address>(_Address);

                /* Optionally uncomment and map manually if needed
                itm.AddressLine1 = _Address.AddressLine1;
                itm.AddressLine2 = _Address.AddressLine2;
                itm.AddressLine3 = _Address.AddressLine3;
                itm.AddressCity  = _Address.AddressCity;
                itm.AddressState = _Address.AddressState;
                itm.AddressZip   = _Address.AddressZip;
                itm.AddressCrtdId= _Address.AddressCrtdId;
                itm.AddressCrtdDt= _Address.AddressCrtdDt;
                itm.AddressUpdtId= _Address.AddressUpdtId;
                itm.AddressUpdtDt= _Address.AddressUpdtDt;
                */

                _context.Addresses.Update(itm);
                await _context.SaveChangesAsync();
                trans.Commit();
            }
            else
            {
                return NotFound($"Address with ID '{_Address.AddressId}' not found.");
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
    public async Task<IActionResult> Post([FromBody] Address _Address)
    {
        var trans = _context.Database.BeginTransaction();

        try
        {
            _Address.AddressId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            _context.Addresses.Add(_Address);
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
