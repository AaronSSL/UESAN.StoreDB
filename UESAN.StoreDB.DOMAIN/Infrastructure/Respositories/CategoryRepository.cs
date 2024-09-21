using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UESAN.StoreDB.DOMAIN.Core.ENTITIES;
using UESAN.StoreDB.DOMAIN.Infrastructure.Data;

namespace UESAN.StoreDB.DOMAIN.Infrastructure.Respositories
{
    public class CategoryRepository
    {
        private readonly StoreDbContext _dbContext;
        
        public CategoryRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        //public List<Category> GetCategories()
        //{
        //    var categories = _dbContext.Category.ToList();
        //    return categories;
        //}

        //public IEnumerable<Category> GetCategories()
        //{
        //    var categories = _dbContext.Category.ToList();
        //    return categories;
        //}

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var categories = await _dbContext.Category.ToListAsync();
            return categories;
        }

        //Obtener categoria por ID
        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _dbContext
                    .Category.Where(c => c.Id == id && c.IsActive==true)
                    .FirstOrDefaultAsync();
            return category;
        }

        //crear categoria
        public async Task<int> Insert(Category category)
        {
            await _dbContext.Category.AddAsync(category);
            int rows = await _dbContext.SaveChangesAsync ();
            if (rows > 0)
            {
                return category.Id;
            }
            else
            {
                return -1;
            }
            
        }

        //para actualizar
        public async Task<bool> Update(Category category)
        {
            _dbContext.Category.Update(category);
            int rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var category = await _dbContext
                    .Category
                    .FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) return false;

            category.IsActive = false;
            int rows = await _dbContext.SaveChangesAsync();
            return (rows > 0);

        }




    }
}
