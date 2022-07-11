#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EntityFramework.Data;
using EntityFramework.Models;

namespace RazorPagesEntityFramework.Pages.Drivers
{
    public class IndexModel : PageModel
    {
        private readonly EntityFramework.Data.DrivingDbContext _context;

        public IndexModel(EntityFramework.Data.DrivingDbContext context)
        {
            _context = context;
        }

        public IList<Driver> Driver { get;set; }
        public ICollection<Car> Car { get; set; }

        public async Task OnGetAsync(int? id)
        {
            Driver = await _context.Drivers
                  .Include(i => i.Cars)
                  .AsNoTracking()
                  .OrderBy(i => i.LastName)
                  .ToListAsync();

            if (id != null)
            {
                Car = Driver.Where(
                    x => x.ID == id).Single().Cars;
            }
        }
    }
}
