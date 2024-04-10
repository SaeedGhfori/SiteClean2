using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Persistence.Seeds.ApplicationSeed
{
    public class ApplicationContextSeed
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            foreach (var categoryBrand in GetCategoryBrands())
            {
                modelBuilder.Entity<CategoryBrand>().HasData(categoryBrand);
            }
            foreach (var category in GetCategorys())
            {
                modelBuilder.Entity<Category>().HasData(category);
            }
            foreach (var invoice in GetInvoices())
            {
                modelBuilder.Entity<Invoice>().HasData(invoice);
            }
        }

        #region CategoryBrands

        private static IEnumerable<CategoryBrand> GetCategoryBrands()
        {
            return new List<CategoryBrand>()
            {
                new CategoryBrand() {  Id=1,  Brand="اسنوا"},
                new CategoryBrand() {  Id= 2,  Brand="ایکس ویژن"},

            };
        }

        #endregion

        #region Categorys

        private static IEnumerable<Category> GetCategorys()
        {
            return new List<Category>()
            {
                new Category() { Id=1, Name = "سامسونگ" },
                new Category() { Id=2, Name = "شیائومی " },
                new Category() { Id=3, Name = "اپل" },
                new Category() { Id=4, Name = "هوآوی" },
                new Category() { Id=5, Name = "نوکیا " },
                new Category() { Id=6, Name = "ال جی" }
            };
        }

        #endregion

        #region Invoices

        private static IEnumerable<Invoice> GetInvoices()
        {
            return new List<Invoice>()
            {
                new Invoice() { Id=1,Count=3,Price=1900,PurchaseDate = DateTime.Now,Title="تلویزیون" },
                new Invoice() { Id=2,Count=1,Price=980,PurchaseDate = DateTime.Now,Title="یخچال" },
            };
        }

        #endregion




    }
}
