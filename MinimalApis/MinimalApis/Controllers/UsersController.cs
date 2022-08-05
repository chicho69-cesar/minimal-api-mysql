using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalApis.Data;
using MinimalApis.Models;

namespace MinimalApis.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {
        private readonly ApiContext _context;

        public UsersController(ApiContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<User> Get(int id) {
            try {
                var user = await _context.Users
                .Where(u => u.Id == id)
                .FirstAsync();

                return user;
            } catch(Exception) {
                return null;
            }
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAll() {
            return await _context.Users
                .ToListAsync();
        }

        [HttpPost]
        public async Task<User> Add(User user) {
            var userAdded = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return userAdded.Entity;
        }

        [HttpPut]
        public async Task<bool> Update(User user) {
            var searched = await _context.Users.FindAsync(user.Id);

            if (searched is null) {
                return false;
            }

            var modified = await _context.Users
                .Where(u => u.Id == user.Id)
                .FirstAsync();

            if (modified is null) {
                return false;
            }

            modified.FirstName = user.FirstName;
            modified.LastName = user.LastName;
            modified.Phone = user.Phone;
            modified.Email = user.Email;

            _context.Entry(modified).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        [HttpDelete]
        public async Task<bool> Delete(int id) {
            var searched = await _context.Users
                .Where(u => u.Id == id)
                .FirstAsync();

            if (searched is null) {
                return false;
            }

            _context.Remove(searched);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}