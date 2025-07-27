using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SD_Fuar.Models;

namespace SD_Fuar.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly SD_FuarContext _context;

        protected BaseController(SD_FuarContext context)
        {
            _context = context;
        }

        protected async Task<T?> GetEntityByIdAsync<T>(int? id) where T : class
        {
            if (id == null)
                return null;

            return await _context.Set<T>().FindAsync(id);
        }

        protected async Task<T?> GetEntityWithIncludesAsync<T>(int? id, params string[] includes) where T : class
        {
            if (id == null)
                return null;

            var query = _context.Set<T>().AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        protected bool EntityExists<T>(int id) where T : class
        {
            return _context.Set<T>().Any(e => EF.Property<int>(e, "Id") == id);
        }

        protected async Task<SelectList> CreateSelectListAsync<T>(string valueField, string textField, object? selectedValue = null) where T : class
        {
            var items = await _context.Set<T>()
                .Select(e => new { Value = EF.Property<object>(e, valueField), Text = EF.Property<string>(e, textField) })
                .ToListAsync();

            return new SelectList(items, "Value", "Text", selectedValue);
        }

        protected async Task<SelectList> CreateSelectListWithCustomTextAsync<T>(string valueField, Func<T, string> textSelector, object? selectedValue = null) where T : class
        {
            var items = await _context.Set<T>()
                .Select(e => new { Value = EF.Property<object>(e, valueField), Text = textSelector(e) })
                .ToListAsync();

            return new SelectList(items, "Value", "Text", selectedValue);
        }
    }
} 