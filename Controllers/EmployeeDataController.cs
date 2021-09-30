using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using you.Models;

namespace you.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeDataController : ControllerBase
    {
        private readonly ConnectionDatabase _context;

        public EmployeeDataController(ConnectionDatabase context)
        {
            _context = context;
        }

        // GET: api/EmployeeData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeData>>> GetEmployee()
        {
            return await _context.Employee.ToListAsync();
        }

        // GET: api/EmployeeData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeData>> GetEmployeeData(int id)
        {
            var employeeData = await _context.Employee.FindAsync(id);

            if (employeeData == null)
            {
                return NotFound();
            }

            return employeeData;
        }

        // PUT: api/EmployeeData/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeData(int id, EmployeeData employeeData)
        {
            employeeData.Id = id;

            _context.Entry(employeeData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeDataExists(id))
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

        // POST: api/EmployeeData
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<EmployeeData>> PostEmployeeData(EmployeeData employeeData)
        {
            _context.Employee.Add(employeeData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeeData", new { id = employeeData.Id }, employeeData);
        }

        // DELETE: api/EmployeeData/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EmployeeData>> DeleteEmployeeData(int id)
        {
            var employeeData = await _context.Employee.FindAsync(id);
            if (employeeData == null)
            {
                return NotFound();
            }

            _context.Employee.Remove(employeeData);
            await _context.SaveChangesAsync();

            return employeeData;
        }

        private bool EmployeeDataExists(int id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }
    }
}
