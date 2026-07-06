using ClinicManagerAPI.Data;
using ClinicManagerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoktorController : ControllerBase
    {
        private readonly ApiDbContext dbcontext;

        public DoktorController(ApiDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("GetDoktorlar")]
        public async Task<IEnumerable<Doktor>> GetDoktorlar()
        {
            return await dbcontext.Doktorlar.ToListAsync();
        }

        [HttpGet]
        [Route("GetDoktorById/{id}")]
        public async Task<Doktor> GetDoktorById(int id)
        {
            return await dbcontext.FindAsync<Doktor>(id);
        }

        [HttpPost]
        [Route("AddDoktor")]
        public async Task<Doktor> AddDoktor(Doktor doktor)
        {
            dbcontext.Add(doktor);
            await dbcontext.SaveChangesAsync();
            return doktor;
        }

        [HttpPut]
        [Route("UpdateDoktor/{id}")]
        public async Task<Doktor> UpdateDoktor(Doktor doktor)
        {
            dbcontext.Update(doktor);
            await dbcontext.SaveChangesAsync();
            return doktor;
        }

        [HttpDelete]
        [Route("DeleteDoktor/{id}")]
        public bool DeleteDoktor(int id)
        {
            var islem = false;
            var result = dbcontext.Doktorlar.Find(id);
            if (result != null)
            {
                islem = true;
                dbcontext.Remove(result);
                dbcontext.SaveChanges();
            }
            else
            {
                return islem;
            }
            return islem;
        }
    }
}