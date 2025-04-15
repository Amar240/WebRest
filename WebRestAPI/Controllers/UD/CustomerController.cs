using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebRest.EF.Data;
using WebRest.EF.Models;

namespace WebRestAPI.Controllers.UD;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase, iController<Customer>
{
    private WebRestOracleContext _context;
    // Create a field to store the mapper object
    private readonly IMapper _mapper;

    public CustomerController(WebRestOracleContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> Get()
    {

        List<Customer> lst = null;
        lst = await _context.Customers.ToListAsync();
        return Ok(lst);
    }


    [HttpGet]
    [Route("Get/{ID}")]
    public async Task<IActionResult> Get(string ID)
    {
        var itm = await _context.Customers.Where(x => x.CustomerId == ID).FirstOrDefaultAsync();
        return Ok(itm);
    }


    [HttpDelete]
    [Route("Delete/{ID}")]
    public async Task<IActionResult> Delete(string ID)
    {
        var itm = await _context.Customers.Where(x => x.CustomerId == ID).FirstOrDefaultAsync();
        _context.Customers.Remove(itm);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Customer _Customer)
    {
        var trans = _context.Database.BeginTransaction();

        try
        {
            var itm = await _context.Customers.AsNoTracking()
            .Where(x => x.CustomerId == _Customer.CustomerId)
            .FirstOrDefaultAsync();


            if (itm != null)
            {
                itm = _mapper.Map<Customer>(_Customer);

                 /*
                        itm.CustomerFirstName = _Customer.CustomerFirstName;
                        itm.CustomerMiddleName = _Customer.CustomerMiddleName;
                        itm.CustomerLastName = _Customer.CustomerLastName;
                        itm.CustomerDateOfBirth = _Customer.CustomerDateOfBirth;
                        itm.CustomerGenderId = _Customer.CustomerGenderId;
                   */      
                _context.Customers.Update(itm);
                await _context.SaveChangesAsync();
                trans.Commit();

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
    public async Task<IActionResult> Post([FromBody] Customer _Customer)
    {
        var trans = _context.Database.BeginTransaction();

        try
        {
            _Customer.CustomerId = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            _context.Customers.Add(_Customer);
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