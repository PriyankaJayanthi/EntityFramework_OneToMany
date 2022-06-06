using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC_ViewModels_Data.Models;
using MVC_ViewModels_Data.Data;
using Microsoft.EntityFrameworkCore;

namespace MVC_ViewModels_Data.Controllers
{
    public class PeopleController : Controller
    {
        IPeopleService _peopleService;
        ExDbContext _context;

        public PeopleController(IPeopleService peopleService, ExDbContext context)
        {
            _peopleService = peopleService;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Person> people = _context.Person.Include(i => i.City).ToList();
           
            return View(_peopleService.All());
        }


        [HttpPost]
         public IActionResult Index(PeopleViewModel peopleviewModel)
         {
             peopleviewModel = _peopleService.FindBy(peopleviewModel);
             return View(peopleviewModel);
         }

        [HttpGet]
        public IActionResult CreatePerson()
        {
            List<City> List = _context.cities.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult CreatePerson(CreatePersonViewModel personViewModel)
        {
                 
                _peopleService.Add(personViewModel);
                return RedirectToAction(nameof(Index));
        }


        public IActionResult DeletePerson(int id)
        {
            _peopleService.Remove(id);

            return RedirectToAction(nameof(Index));
        }
    

    }
}
