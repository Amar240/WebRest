using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebRest.EF.Data;
using WebRest.EF.Models;

namespace WebRestAPI.Controllers.UD;

[ApiController]
[Route("api/[controller]")]
public class GenderController : ControllerBase, iController<Gender>
{
    private readonly WebRestOracleContext _context;
    private readonly IMapper _mapper;

    public GenderController(WebRestOracleContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> Get()
    {
        var lst = await _context.Genders.ToListAsync();
        return Ok(lst);
    }

    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var itm = await _context.Genders
            .FirstOrDefaultAsync(x => x.GenderId == id);

        if (itm == null)
            return NotFound($"Gender with ID '{id}' not found.");

        return Ok(itm);
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var itm = await _context.Genders
            .FirstOrDefaultAsync(x => x.GenderId == id);

        if (itm == null)
            return NotFound($"Gender with ID '{id}' not found.");

        _context.Genders.Remove(itm);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Gender _Gender)
    {
        var trans = _context.Database.BeginTransaction();

        try
        {
            var itm = await _context.Genders.AsNoTracking()
                .FirstOrDefaultAsync(x => x.GenderId == _Gender.GenderId);

            if (itm != null)
            {
                itm = _mapper.Map<Gender>(_Gender);

                /* Optional manual mappings if necessary
                itm.GenderDesc = _Gender.GenderDesc;
                itm.GenderCrtdId = _Gender.GenderCrtdId;
                itm.GenderCrtdDt = _Gender.GenderCrtdDt;
                itm.GenderUpdtId = _Gender.GenderUpdtId;
                itm.GenderUpdtDt = _Gender.GenderUpdtDt;
                */

                _context.Genders.Update(itm);
                await _context.SaveChangesAsync();
                trans.Commit();
            }
            else
            {
                return NotFound($"Gender with ID '{_Gender.GenderId}' not found.");
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
    public async Task<IActionResult> Post([FromBody] Gender _Gender)
    {
        var trans = _context.Database.BeginTransaction();

        try
        {
            _Gender.GenderId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            _context.Genders.Add(_Gender);
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
