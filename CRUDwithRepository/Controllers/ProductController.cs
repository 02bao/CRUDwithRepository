using CRUDwithRepository.Infastructure.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CRUDwithRepository.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepo;

        public ProductController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productRepo.GetAll();
            return View(products);
        }
    }
}
