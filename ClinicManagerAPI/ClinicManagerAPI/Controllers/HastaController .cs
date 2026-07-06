using ClinicManagerAPI.Data;
using ClinicManagerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HastaController : ControllerBase
    {
        private readonly ApiDbContext dbcontext;

        public HastaController(ApiDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("GetHastalar")]
        public async Task<IEnumerable<Hasta>> GetHastalar()
        {
            return await dbcontext.Hastalar.ToListAsync();
        }

        [HttpGet]
        [Route("GetHastaById/{id}")]
        public async Task<Hasta> GetHastaById(int id)
        {
            return await dbcontext.FindAsync<Hasta>(id);
        }

        [HttpPost]
        [Route("AddHasta")]
        public async Task<Hasta> AddHasta(Hasta hasta)
        {
            dbcontext.Add(hasta);
            await dbcontext.SaveChangesAsync();
            return hasta;
        }

        [HttpPut]
        [Route("UpdateHasta/{id}")]
        public async Task<Hasta> UpdateHasta(Hasta hasta)
        {
            dbcontext.Update(hasta);
            await dbcontext.SaveChangesAsync();
            return hasta;
        }

        [HttpDelete]
        [Route("DeleteHasta/{id}")]
        public bool DeleteHasta(int id)
        {
            var islem = false;
            var result = dbcontext.Hastalar.Find(id);
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