#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EntityFramework.Data;
using EntityFramework.Models;

namespace RazorPagesEntityFramework.Pages.Cars
{
    public class CreateModel : PageModel
    {
        private readonly EntityFramework.Data.DrivingDbContext _context;

        public CreateModel(EntityFramework.Data.DrivingDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["DriverID"] = new SelectList(_context.Drivers, "ID", "FirstName");
            return Page();
        }

        [BindProperty]
        public Car Car { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Cars.Add(Car);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
