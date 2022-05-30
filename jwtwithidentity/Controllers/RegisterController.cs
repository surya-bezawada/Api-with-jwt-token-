using jwtwithidentity.Data;
using jwtwithidentity.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

namespace jwtwithidentity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly ApplicationDbContext _datacontext;


        public RegisterController(ApplicationDbContext datacontext)
        {
            _datacontext = datacontext;
        }



        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<IEnumerable<ItemModel>>> GetEmployee()
        {
            return await _datacontext.itemModels.ToListAsync();
        }

        [HttpGet]
        [Route("GetbyId")]
        public async Task<ActionResult<ItemModel>> GetEmployee(int id)
        {
            var employee = await _datacontext.itemModels.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, ItemModel employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            _datacontext.Entry(employee).State = EntityState.Modified;

            try
            {
                await _datacontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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


        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ItemModel>> PostEmployee(ItemModel employee)
        {
            _datacontext.itemModels.Add(employee);
            await _datacontext.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _datacontext.itemModels.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _datacontext.itemModels.Remove(employee);
            await _datacontext.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return _datacontext.itemModels.Any(e => e.Id == id);
        }




    }

            
}
