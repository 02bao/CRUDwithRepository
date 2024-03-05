using CRUDwithRepository.Core;
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

        [HttpGet]
        public async Task<IActionResult> CreateOrEdit(int id=0) 
        {
            if(id == 0)
            {
                return View(new Product());
            }
            else
            {
                try
                {
                    Product product = await _productRepo.GetById(id);
                    if (product != null)
                    {
                        return View(product);
                    }
                }
                catch (Exception ex)
                {

                    TempData["errorMessage"] = ex.Message;
                    return RedirectToAction("Index");
                }
                TempData["errorMessage"] = $"Product details not found with Id: {id}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task< IActionResult > CreateOrEdit(Product model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(model.Id == 0)
                    {
                        await _productRepo.Add(model);
                        TempData["successMessage"] = "Product create successfully";
                    }
                    else
                    {
                        await _productRepo.Update(model);
                        TempData["successMessage"] = "Product details updated successfully!";
                    }
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["errorMessage"] = "Model state is invalid";
                    return View();

                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();

            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Product product = await _productRepo.GetById(id);
                if (product != null)
                {
                    return View(product);
                }
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
            TempData["errorMessage"] = $"Product details not found with Id: {id}";
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _productRepo.Delete(id);
                TempData["successMessage"] = "Product create successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

    }
}
