using EntityLayer.Entities;
using EntityLayer.Context;
using Microsoft.AspNetCore.Mvc;
using ShoppingList.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using X.PagedList;
using NuGet.Protocol.Core.Types;

namespace ShoppingList.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        const int pageItemsCount = 10;
        ShoppingDbContext context;
        public AdminController() {
          context = new ShoppingDbContext();
        }
        
        
        public IActionResult Category(int page = 1)
        {

           List<Category> categories = new List<Category>();
            categories = context.Categories.ToList();
            IPagedList<Category> pagedCategories = categories.ToPagedList(page, pageItemsCount);

            if (categories.Count==0)
            {
                ViewData["NotCategories"] = "true";
            }
            return View(pagedCategories);
        }
        [HttpGet]
        public IActionResult CategoryAdd()
        {


            return View();
        }
        [HttpPost]
        public IActionResult CategoryAdd(Category category) 
        { if (ModelState.IsValid)
            {
                Category value = context.Categories.Where(x => x.CategoryName == category.CategoryName).SingleOrDefault();
                if (value == null)
                {
                    ViewData["CategoryAdd"] = "true";
                    context.Categories.Add(category);
                    context.SaveChanges();
                    return View();
                }
                ViewData["NotCategoryAdd"] = "true";
                ModelState.Remove("CategoryName");
                return View();

            }
        return View();
        }

        public IActionResult CategoryDelete(int id)
        {
            
            Category model = context.Categories.Where(x => x.CategoryID == id).SingleOrDefault();
            if(model!=null)
            {
                context.Categories.Remove(model);
                context.SaveChanges();
                return RedirectToAction("Category");
            }
            ModelState.AddModelError("", "Böyle bir kategori mevcut değil");

            return RedirectToAction("Category");
            
        }

        [HttpGet]
        public IActionResult CategoryEdit(int id)
        {
            ViewBag.category=context.Categories.Where(x => x.CategoryID == id).Select(x => x.CategoryName).SingleOrDefault();
            return View();

        }

        [HttpPost]
        public IActionResult CategoryEdit(int id, Category model)
        {
            if (ModelState.IsValid)
            {
                Category category = context.Categories.Where(x => x.CategoryID == id).SingleOrDefault();
                category.CategoryName=model.CategoryName;

                List<Category> categories = context.Categories.ToList();
                categories.RemoveAll(x=>x.CategoryID == id);
                var categoriesCopy=new List<Category>(categories);

                bool state = true;
              foreach (var item in categoriesCopy)
                {
                  
                    if (item.CategoryName==model.CategoryName)
                    {
                        ViewData["NotAdCategoryEdit"]="Böyle bir kategori var";
                         state = false;
                    }
                                            
                }

                if (state == true)
                {
                    context.Update(category);
                    context.SaveChanges();
                    ViewData["AdCategoryEdit"] = "Başarılı";                   
                    return View();
                }
                else
                {
                    return View();
                }

            }
            else
            {               
                return View();

            }


        }
        public IActionResult Product(int page = 1)
        {
            
            List<Product> products = new List<Product>();
            products = context.Products.Include(x=>x.Category).ToList();
            IPagedList<Product> pagedProducts = products.ToPagedList(page, pageItemsCount);
            if (products.Count == 0)
            {
                ViewData["NotProducts"] = "true";
            }
            return View(pagedProducts);
           
        }

        void Get()
        {
            
                List<SelectListItem> selectList = new List<SelectListItem>();
                List<string> categories = context.Categories.Select(x => x.CategoryName).ToList();
                foreach (var item in context.Categories.Select(a => new { a.CategoryID, a.CategoryName }))
                {
                    selectList.Add(new SelectListItem()
                    {

                        Text = item.CategoryName,
                        Value = item.CategoryID.ToString()
                    });
                }
                ViewBag.Categories = categories;
            
        }

        [HttpGet]
        public IActionResult ProductAdd()
        {

            Get();
            return View();
        }

        [HttpPost]
        public IActionResult ProductAdd(ProductAddViewModel model)
        {

            if (ModelState.IsValid)
            {
                Product product = context.Products.Where(x => x.ProductName == model.ProductName).SingleOrDefault();

                if (product == null)
                {
                    product = new Product();
                    if (model.Image != null)
                    {
                        var extension = Path.GetExtension(model.Image.FileName);
                        var newimagename = Guid.NewGuid() + extension;
                        var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImage/", newimagename);
                        var stream = new FileStream(location, FileMode.Create);
                        model.Image.CopyTo(stream);
                        product.Image = newimagename;


                        product.ProductName = model.ProductName;
                        product.CategoryID = context.Categories
                            .Where(x => x.CategoryName == model.CategoryName).Select(x => x.CategoryID).SingleOrDefault();

                        ViewData["ProductAdd"] = "true";
                        context.Products.Add(product);
                        context.SaveChanges();
                        Get();
                        return View();
                    }
                    else
                    {
                        ViewData["Image"] = "true";
                        Get();
                        return View();
                    }
                }
                else
                {
                    ViewData["NotProductAdd"] = "true";
                    ModelState.Remove("ProductName");
                }
            }
            Get();
            return View();

        }
        public IActionResult ProductDelete(int id)
        {

            Product model = context.Products.Where(x => x.ProductID == id).SingleOrDefault();
            if (model != null)
            {
                context.Products.Remove(model);
                context.SaveChanges();
                return RedirectToAction("Product");
            }
            ModelState.AddModelError("", "Böyle bir ürün mevcut değil");

            return RedirectToAction("Product");

        }

        [HttpGet]
        public IActionResult ProductEdit(int id)
        {

            Get();
            ViewBag.productname=context.Products.Where(x=>x.ProductID==id).Select(x=>x.ProductName).SingleOrDefault();
            ViewBag.categoryname = context.Products.Include(x => x.Category).Where(x => x.ProductID == id).Select(x => x.Category.CategoryName).SingleOrDefault();
            return View();
            
        }

        [HttpPost]
        public IActionResult ProductEdit(int id,ProductEditViewModel model)
        {
            if(ModelState.IsValid)
            {
                 Product product=context.Products.Where(x=>x.ProductID==id).SingleOrDefault();
                product.ProductName = model.ProductName;
               

                List<Product> products = context.Products.ToList();
                products.RemoveAll(x => x.ProductID == id);
                var productsCopy = new List<Product>(products);

                bool state = true;
                foreach (var item in productsCopy)
                {

                    if (item.ProductName == model.ProductName)
                    {
                        ViewData["NotAdProductEdit"] = "Böyle bir ürün adı zaten var";
                        state = false;
                    }
                }


                if (state == true)
                {
                    if (model.Image == null)
                        product.Image = context.Products.Where(x => x.ProductID == id).Select(x => x.Image).SingleOrDefault();
                    else
                    {
                        var extension = Path.GetExtension(model.Image.FileName);
                        var newimagename = Guid.NewGuid() + extension;
                        var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProductImage/", newimagename);
                        var stream = new FileStream(location, FileMode.Create);
                        model.Image.CopyTo(stream);
                        product.Image = newimagename;

                    }


                    product.CategoryID = context.Categories.Where(x => x.CategoryName == model.CategoryName).Select(x => x.CategoryID).SingleOrDefault();
                    context.Update(product);
                    context.SaveChanges();
                    ViewData["AdProductEdit"] = "Başarılı";
                    Get();
                    return RedirectToAction("Product", "Admin");
                }
                else
                {
                    Get();
                    return View();
                }

            }
            else
            {

                Get();
                return View();

            }


        }
    }
}
