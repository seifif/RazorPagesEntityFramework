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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RazorPagesEntityFramework.Pages.Cars
{
    public class IndexModel : PageModel
    {
        private readonly EntityFramework.Data.DrivingDbContext _context;

        public IndexModel(EntityFramework.Data.DrivingDbContext context)
        {
            _context = context;
        }

        public IList<Car> Cars { get;set; }
        public SelectList Makes { get; set; }
        [BindProperty(SupportsGet = true)]
        public string CarMake { get; set; }

        public async Task OnGetAsync()
        {
            // Use LINQ to get list of genres.
            IQueryable<string> makesQuery = from m in _context.Cars
                                            orderby m.Make
                                            select m.Make;

            var cars = from m in _context.Cars
                         select m;


            if (!string.IsNullOrEmpty(CarMake))
            {
                cars = cars.Where(x => x.Make == CarMake);
            }
            Makes = new SelectList(await makesQuery.Distinct().ToListAsync());
            Cars = await cars.ToListAsync();
        }
    }
}
