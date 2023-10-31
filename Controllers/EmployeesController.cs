using crud.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeesDbContext _employeesDbContext;
        public EmployeesController(EmployeesDbContext employeesDbContext)
        {
            _employeesDbContext = employeesDbContext;
        }

        [HttpGet ("GetAllEmployees")]
        public async Task<ActionResult<IEnumerable<Employees>>> GetEmployees()
        {
            return await _employeesDbContext.Employees.ToListAsync();
        }

        [HttpGet("GetEmployee{id}")]
        public async Task<ActionResult<Employees>> GetEmployee(int id)
        {
            var employee = await _employeesDbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }

        [HttpPost ("AddEmployee")]
        public async Task<ActionResult<Employees>> PostEmployee(Employees employee)
        {
            _employeesDbContext.Employees.Add(employee);
            await _employeesDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        [HttpPut("UpdateEmployee{id}")]
        public async Task<ActionResult<Employees>> PutEmployee(int  id, Employees employee)
        {
            if(id!=employee.Id)
            {
                return BadRequest();
            }
            _employeesDbContext.Entry(employee).State = EntityState.Modified;

            try
            {
                await _employeesDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var findEmployee = await _employeesDbContext.Employees.FindAsync(id);
                if(findEmployee == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

            }
            return NoContent();

        }

        [HttpDelete ("DeleteEmployee{id}")]
        public async Task<ActionResult> DeleteEmployees(int id)
        {
            var employee = await _employeesDbContext.Employees.FindAsync(id);
            if(id!=employee.Id) 
            {
                return NotFound();
            }
            _employeesDbContext.Remove(employee);
            await _employeesDbContext.SaveChangesAsync();

            return NoContent();
        }

    }
    
}
