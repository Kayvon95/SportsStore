using Microsoft.AspNetCore.Mvc;
using SportsStore.Models; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        int PageSize = 4;

        //Kijk in Startup.cs voor object dat IProduct implementeert
        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }
        //Populate nameless view with list(product) data
        public ViewResult List(string category, int page = 1)
            => View(new ProductsListViewModel
            {
                Products = repository.Products
                //Accepteer null category, of opgegeven cat = p.cat
                .Where(p => category == null  || p.Category == category)
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? 
                        repository.Products.Count() :
                        repository.Products.Where(e => 
                        e.Category == category).Count()
                },
                CurrentCategory = category
            });
    }
}
