using EntityLayer.Context;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShoppingList.ViewModels;
using System.Net;
using System.Security.Claims;
using X.PagedList;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ShoppingList.Controllers
{

    [Authorize(Roles = "User,Admin")]
    public class UserController : Controller
    {
        ShoppingDbContext context;
       
        public UserController()
        {
            context = new ShoppingDbContext();

        }



        [HttpGet]

        public IActionResult List(int id)
        {
            var user = context.Users.Where(x => x.UserID == id).SingleOrDefault();
            if (user != null)
            {
                UserListViewModel viewModel = new UserListViewModel();
                List<ListViewModel> shopList = context.ShopLists.Include(x => x.User).Where(x => x.User.UserID == id).Select(x => new ListViewModel()
                {
                    ListID = x.ListID,
                    ListName = x.ListName

                }).ToList();


                viewModel.UserID = id;
                viewModel.ListViewModel = shopList;


                return View(viewModel);
            }
            else
                return RedirectToAction("Error404", "ErrorPage");
 

        }



        [HttpGet]
        public IActionResult ListPost()
        {

            return View();
        }

        [HttpPost]
        public IActionResult ListPost(CreateListViewModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {

                // Var olan bir ShopList'i bulmaya çalış
                ShopList shopList = context.ShopLists.SingleOrDefault(x => x.ListName == model.ListName);

                // Eğer shopList değeri null değilse, yani aynı isimde bir liste varsa hata döndür
                if (shopList == null)
                {
                    // Aynı isimde bir liste yoksa, yeni bir ShopList oluştur ve kaydet
                    shopList = new ShopList()
                    {
                        ListName = model.ListName,
                        UserID = id
                    };
                    context.ShopLists.Add(shopList);
                    context.SaveChanges();
                    ViewData["ListPost"] = "true";
                    ModelState.Remove("ListName");
                    return View();


                }


                ViewData["NotListPost"] = "true";
                return View();
            }
        }


        public IActionResult ListDelete(int id)
        {
            ShopList shopList = context.ShopLists.SingleOrDefault(x => x.ListID == id);

            // Eğer shopList değeri null değilse, yani aynı isimde bir liste varsa hata döndür
            if (shopList == null)
            {
                return BadRequest("Bu isimde bir liste mevcut değil");
            }
            else
            {
                var userid = shopList.UserID;
                context.ShopLists.Remove(shopList);
                context.SaveChanges();
                UserListViewModel viewModel = new UserListViewModel();
                List<ListViewModel> modell = context.ShopLists.Include(x => x.User).Where(x => x.User.UserID == userid).Select(x => new ListViewModel()
                {
                    ListID = x.ListID,
                    ListName = x.ListName

                }).ToList();

                viewModel.UserID = userid;
                viewModel.ListViewModel = modell;

                return View("../User/List", viewModel);
            }
        }

        [HttpGet]
        public IActionResult ListDetail(int id,int page=1)
        {

            List<ListDetailViewModel> product = context.ListProductDetails.Include(x => x.Product).Where(x => x.ListID == id).Select(x => new ListDetailViewModel()
            {
                //    Description=x.Description,
                ProductName = x.Product.ProductName,
                Image = x.Product.Image
            }).ToList();

            if (product.Count == 0)
            { ViewData["NoProduct"] = "true"; }
            ViewBag.ListName = context.ShopLists.Where(x => x.ListID == id).Select(x => x.ListName).SingleOrDefault();


            TempData["ListId"] = id;
            TempData["ListId2"] = id;
            return View("../User/ListDetail", product);
        }




        void Get()
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            List<string> products = context.Products.Select(x => x.ProductName).ToList();
            foreach (var item in context.Products.Select(a => new { a.ProductID, a.ProductName }))
            {
                selectList.Add(new SelectListItem()
                {

                    Text = item.ProductName,
                    Value = item.ProductID.ToString()
                });
            }
            ViewBag.Products = products;
        }


        [HttpGet]
        public IActionResult ProductAdd()
        {
            Get();
            return View();
        }



        [HttpPost]
        public IActionResult ProductAdd(int id, ProductViewModel p)
        {
            if (ModelState.IsValid)
            {
                int productid = context.Products.Where(x => x.ProductName == p.ProductName).Select(x => x.ProductID).SingleOrDefault();
                ListProductDetail product = context.ListProductDetails.Where(x => x.ProductID == productid && x.ListID == id).SingleOrDefault();
                if (product == null)
                {
                    product = new ListProductDetail()
                    {

                        ListID = id,
                        ProductID = productid,                     
                        Description = p.ProductDescription

                    };
                    context.ListProductDetails.Add(product);
                    context.SaveChanges();
                    ViewData["ProductAdd"] = "true";
                    ModelState.Remove("ProductDescription");
                    Get();
                    return View();
                }
                else
                {
                    ViewData["NotListPost"] = "true";
                    Get();
                    ModelState.AddModelError("", "Listenizde bu ürün mevcut");
                    return View();
                }
            }
            else
            {
                Get();
                return View();
            }
        }



        public IActionResult ProductDelete(int id, string name)
        {


            ListProductDetail listProductDetail = new ListProductDetail();
            listProductDetail.ProductID = context.Products.Where(x => x.ProductName == name).Select(x => x.ProductID).SingleOrDefault();
            listProductDetail.ListID = id;
            //listProductDetail.Description = context.ListProductDetails.Where(x => x.ProductID == listProductDetail.ProductID && x.ListID == id).Select(x => x.Description).SingleOrDefault();

            context.ListProductDetails.Remove(listProductDetail);
            context.SaveChanges(true);

            List<ListDetailViewModel> product = context.ListProductDetails.Include(x => x.Product).Where(x => x.ListID == id).Select(x => new ListDetailViewModel()
            {
                //Description = x.Description,
                ProductName = x.Product.ProductName,
                Image = x.Product.Image
            }).ToList();
            TempData["ListId"] = id;
            return View("../User/ListDetail", product);
        }


        //[HttpGet("{id}/{name}")]

        [HttpGet]
        public IActionResult EditDescription(int id, string name)
        {
           
            var productid = context.Products.Where(x => x.ProductName == name).Select(x => x.ProductID).SingleOrDefault();
            ListProductDetail detail = context.ListProductDetails.Where(x => x.ListID == id && x.ProductID == productid).SingleOrDefault();
            
                if (detail.Description == null)
                    ViewBag.Description = "Ürün açıklaması ekleyebilirsiniz...";
                else
                    ViewBag.Description = detail.Description;

          
            return View(); 
        }

        //[HttpPost("{id}/{name}")]
        [HttpPost]
        public IActionResult EditDescription(int id, string name, EditDescriptionviewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var productid = context.Products.Where(x => x.ProductName == name).Select(x => x.ProductID).SingleOrDefault();
                ListProductDetail detail = context.ListProductDetails.Where(x => x.ListID == id && x.ProductID == productid).SingleOrDefault();
                detail.Description = model.Description;

                ViewData["Edit"] = "true";
                context.Update(detail);
                context.SaveChanges();
                return View();
            }
            ModelState.Remove("Description");
            return View();
        }


        public IActionResult Shop(int id)
        {
            List<GoShopViewModel> product = context.ListProductDetails.Include(x => x.Product).Where(x => x.ListID == id).Select(x => new GoShopViewModel()
            {
                ProductID=x.ProductID,
                Description = x.Description,
                ProductName = x.Product.ProductName,
                Image = x.Product.Image
               

            }).ToList();

            if (product.Count == 0)
            { ViewData["NoProduct"] = "true"; }
            ViewBag.ListName = context.ShopLists.Where(x => x.ListID == id).Select(x => x.ListName).SingleOrDefault();


     
            return View(product);

        }

        [HttpPost]
        public IActionResult Shop(int id,int[] selectedProducts)
        {
            var productsToRemove = context.ListProductDetails
              .Where(x => x.ListID == id && selectedProducts.Contains(x.ProductID))
             .ToList();

            foreach (var item in productsToRemove)
            {
                context.ListProductDetails.Remove(item);
            }

            context.SaveChanges();

            return RedirectToAction("ListDetail", new { id = id });

        
            
        }

    }

	    
	}

