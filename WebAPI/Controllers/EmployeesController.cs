using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly MyDbContext _db;
        public EmployeesController(MyDbContext mydbcontext)
        {
           _db = mydbcontext;
        }

        //Read
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _db.Employees.ToListAsync();
            return Ok(employees);
        }

        //Add
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();
            await _db.Employees.AddAsync(employeeRequest);
            await _db.SaveChangesAsync();
            return Ok(employeeRequest);
        }

        //Update 
        [HttpGet]
        [Route("{id: Guid}")]
        public async Task<IActionResult> updateEmployee([FromRoute] Guid id)
        {
            var employee = await _db.Employees.SingleOrDefaultAsync(x => x.Id == id);

            if(employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
    }
}
