using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestTaskPTMK.Models;
using TestTaskPTMK.Repositories.Interfaces.Base;

namespace TestTaskPTMK.Controllers
{
    public class PeopleController : Controller
    {
        private readonly IPeopleRepository _peopleRepository;
        private readonly ISexesRepository _sexesRepository;

        public PeopleController(IPeopleRepository peopleRepository, ISexesRepository sexesRepository)
        {
            _peopleRepository = peopleRepository;
            _sexesRepository = sexesRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _peopleRepository.GetOrderedAsync());
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Sexes = new SelectList(await _sexesRepository.GetAllAsync(), "Id", "Name");
            return View();
        }

        public async Task<IActionResult> FillRandomly()
        {
            List<string> lastNames = new List<string>() { "Alekseev", "Borisov", "Charovnikov", "Dariushkin", "Evgeniev", "Fedorov", "Grigoriev", "Haritonov", "Ivanov", "Jorikov", "Kostin", "Ladushkin", "Mariin", "Novikov", "Orechov", "Promov", "Rogov", "Setin", "Timofeev", "Ugriumov", "Xamarin", "Yulin", "Zeverov" };
            List<string> firstNames = new List<string>() { "Aleksandr", "Alexsei", "Dmitri", "Fyodor", "Igor", "Ivan", "Leonid", "Marat", "Anatoly", "Arseny", "Boris", "Dmitri", "Gavriil", "Gennady", "Grigory", "Iosif", "Kiril", "Lev", "Matvey", "Mikhail" };
            List<string> middleNames = new List<string>() { "Aleksandrovich", "Alexseevich", "Dmitrievich", "Fyodorovich", "Igorevich", "Ivanovich", "Leonidovich", "Maratovich", "Anatolievich", "Arsenievich", "Borisovich", "Gavriilovich", "Gennadievich", "Grigorievich", "Iosifovich", "Kirilovich", "Levovich", "Matveevich", "Mikhailovich" };

            Random random = new Random();

            foreach (var lastName in lastNames)
            {
                foreach (var firstName in firstNames)
                {
                    foreach (var middleName in middleNames)
                    {
                        await _peopleRepository.CreateAsync(new Person { LastName = lastName, FirstName = firstName, MiddleName = middleName, BirthDate = new DateTime(random.Next(1970, 2004), random.Next(1, 12), random.Next(1, 28)), SexId = random.Next(0, 1) });
                    }
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Create(Person person)
        {
            if (ModelState.IsValid)
            {
                await _peopleRepository.CreateAsync(person);
                return RedirectToAction("Index");
            }
            return View(person);
        }

        public async Task<IActionResult> Get(long id)
        {
            return View(await _peopleRepository.GetAsync(id));
        }

        public async Task<IActionResult> Get(string start)
        {
            return View(await _peopleRepository.GetByLastName(start));
        }

        public async Task<IActionResult> Update(long id)
        {
            return View(await _peopleRepository.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(Person person)
        {
            if (ModelState.IsValid)
            {
                await _peopleRepository.UpdateAsync(person);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(long id)
        {
            return View(await _peopleRepository.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Person person)
        {
            await _peopleRepository.DeleteAsync(person.Id);
            return RedirectToAction("Index");
        }
    }
}