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

        //Populate selected Update row 
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> updateEmployee([FromRoute] Guid id)
        {
            var employee = await _db.Employees.SingleOrDefaultAsync(x => x.Id == id);

            if(employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        //officually update in database
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> officialUpdate([FromRoute] Guid id, Employee officialUpdate)
        {
            var employee = await _db.Employees.FindAsync(id);

            if(employee == null)
            {
                return NotFound();
            }
            employee.Name = officialUpdate.Name;
            employee.Email = officialUpdate.Email;
            employee.Salary = officialUpdate.Salary;
            employee.Phone = officialUpdate.Phone;
            employee.Department = officialUpdate.Department;
            await _db.SaveChangesAsync();

            return Ok(employee);
        }
    }
}
