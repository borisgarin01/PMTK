using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestTaskPTMK.Models;
using TestTaskPTMK.Repositories.Interfaces.Base;

namespace TestTaskPTMK.Controllers
{
    public class SexesController : Controller
    {
        private readonly ISexesRepository _sexesRepository;

        public SexesController(ISexesRepository sexesRepository)
        {
            _sexesRepository = sexesRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _sexesRepository.GetAllAsync());
        }

        public async Task<IActionResult> Get(long id)
        {
            return View(await _sexesRepository.GetAsync(id));
        }

        public async Task<IActionResult> Update(long id)
        {
            return View(await _sexesRepository.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(Sex sex)
        {
            if (ModelState.IsValid)
            {
                await _sexesRepository.UpdateAsync(sex);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(long id)
        {
            return View(await _sexesRepository.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Sex sex)
        {
            await _sexesRepository.DeleteAsync(sex.Id);
            return RedirectToAction("Index");
        }
    }
}