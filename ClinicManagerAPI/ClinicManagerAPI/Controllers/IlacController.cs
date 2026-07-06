using ClinicManagerAPI.Data;
using ClinicManagerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IlacController : ControllerBase
    {
        private readonly ApiDbContext dbcontext;

        public IlacController(ApiDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("GetIlaclar")]
        public async Task<IEnumerable<Ilac>> GetIlaclar()
        {
            return await dbcontext.Ilaclar.ToListAsync();
        }

        [HttpGet]
        [Route("GetIlacById/{id}")]
        public async Task<Ilac> GetIlacById(int id)
        {
            return await dbcontext.FindAsync<Ilac>(id);
        }

        [HttpPost]
        [Route("AddIlac")]
        public async Task<Ilac> AddIlac(Ilac ilac)
        {
            dbcontext.Add(ilac);
            await dbcontext.SaveChangesAsync();
            return ilac;
        }

        [HttpPut]
        [Route("UpdateIlac/{id}")]
        public async Task<Ilac> UpdateIlac(Ilac ilac)
        {
            dbcontext.Update(ilac);
            await dbcontext.SaveChangesAsync();
            return ilac;
        }

        [HttpDelete]
        [Route("DeleteIlac/{id}")]
        public bool DeleteIlac(int id)
        {
            var islem = false;
            var result = dbcontext.Ilaclar.Find(id);
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