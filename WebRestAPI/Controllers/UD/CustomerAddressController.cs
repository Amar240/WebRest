using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebRest.EF.Data;
using WebRest.EF.Models;

namespace WebRestAPI.Controllers.UD;

[ApiController]
[Route("api/[controller]")]
public class CustomerAddressController : ControllerBase, iController<CustomerAddress>
{
    private readonly WebRestOracleContext _context;
    private readonly IMapper _mapper;

    public CustomerAddressController(WebRestOracleContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> Get()
    {
        var lst = await _context.CustomerAddresses.ToListAsync();
        return Ok(lst);
    }

    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var itm = await _context.CustomerAddresses
            .FirstOrDefaultAsync(x => x.CustomerAddressId == id);

        if (itm == null)
            return NotFound($"CustomerAddress with ID '{id}' not found.");

        return Ok(itm);
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var itm = await _context.CustomerAddresses
            .FirstOrDefaultAsync(x => x.CustomerAddressId == id);

        if (itm == null)
            return NotFound($"CustomerAddress with ID '{id}' not found.");

        _context.CustomerAddresses.Remove(itm);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] CustomerAddress _CustomerAddress)
    {
        var trans = _context.Database.BeginTransaction();

        try
        {
            var itm = await _context.CustomerAddresses.AsNoTracking()
                .FirstOrDefaultAsync(x => x.CustomerAddressId == _CustomerAddress.CustomerAddressId);

            if (itm != null)
            {
                itm = _mapper.Map<CustomerAddress>(_CustomerAddress);

                /* Optional manual mappings if necessary
                itm.CustomerAddressCustomerId = _CustomerAddress.CustomerAddressCustomerId;
                itm.CustomerAddressAddressId = _CustomerAddress.CustomerAddressAddressId;
                itm.CustomerAddressAddressTypeId = _CustomerAddress.CustomerAddressAddressTypeId;
                itm.CustomerAddressActvInd = _CustomerAddress.CustomerAddressActvInd;
                itm.CustomerAddressDefaultInd = _CustomerAddress.CustomerAddressDefaultInd;
                itm.CustomerAddressCrtdId = _CustomerAddress.CustomerAddressCrtdId;
                itm.CustomerAddressCrtdDt = _CustomerAddress.CustomerAddressCrtdDt;
                itm.CustomerAddressUpdtId = _CustomerAddress.CustomerAddressUpdtId;
                itm.CustomerAddressUpdtDt = _CustomerAddress.CustomerAddressUpdtDt;
                */

                _context.CustomerAddresses.Update(itm);
                await _context.SaveChangesAsync();
                trans.Commit();
            }
            else
            {
                return NotFound($"CustomerAddress with ID '{_CustomerAddress.CustomerAddressId}' not found.");
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
    public async Task<IActionResult> Post([FromBody] CustomerAddress _CustomerAddress)
    {
        var trans = _context.Database.BeginTransaction();

        try
        {
            _CustomerAddress.CustomerAddressId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            _context.CustomerAddresses.Add(_CustomerAddress);
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
