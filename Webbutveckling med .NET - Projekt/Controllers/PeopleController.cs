#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Webbutveckling_med_.NET___Projekt.Data;
using Webbutveckling_med_.NET___Projekt.Models;

namespace Webbutveckling_med_.NET___Projekt.Controllers
{
    public class PeopleController : Controller
    {
        private readonly DogContext _context;

        public PeopleController(DogContext context)
        {
            _context = context;
        }

        // GET: People
        [Authorize]
        public async Task<IActionResult> Index()
        {
            //Include dog in the person model
            return View(await _context.Person.Include(x => x.Dog).ToListAsync());
        }

        // GET: People/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Find person by id
            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.PersonId == id);

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create(int id)
        {
            //Viewbags with data
            ViewBag.Dog = _context.Dog.Find(id);
            ViewBag.DogId = id;

            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,Firstname,Lastname,PhoneNr,Email,City,DogId,Description")] Person person)
        {
            //Check if the model is valid
            if (ModelState.IsValid)
            {
                //Find the dog by the dogid in person model and save in variable
                var dog = _context.Dog.Find(person.DogId);

                //Set the dog to reserved
                dog.Reserved = true;

                //Add the dog to person model
                person.Dog.Add(dog);

                //Save
                _context.Add(person);
                await _context.SaveChangesAsync();

                //Return "thanks" view and person model
                return View("Thanks", person);
            }
            
            return View("OurDogs");
        }


            // GET: People/Edit/5
            [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Find person by id
            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonId,Firstname,Lastname,PhoneNr,Email,City")] Person person)
        {
            //Check if id doesn't match personId
            if (id != person.PersonId)
            {
                return NotFound();
            }

            //Check if the model is valid
            if (ModelState.IsValid)
            {
                //Try code and catch exception
                try
                {
                    //Update person
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //If person doesn't exist
                    if (!PersonExists(person.PersonId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: People/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            //Check if id is null
            if (id == null)
            {
                return NotFound();
            }

            //Include Dog in Person model
            var person = await _context.Person.Include(x => x.Dog)
                .FirstOrDefaultAsync(m => m.PersonId == id);

            //Check if person is null
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //Include dog in person model and find person by id
            var person = await _context.Person.Include(x => x.Dog).FirstOrDefaultAsync(m => m.PersonId == id);

            //Find the dog that's attached to the person
            var dog = _context.Dog.Find(person.Dog.FirstOrDefault().DogId);

            //Set that dog is not adopted
            dog.Adopted = false;

            //Set that dog is not reserved
            dog.Reserved = false;

            //Delete person and save
            _context.Person.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.PersonId == id);
        }

        //GET - Reserved
        [Authorize]
        public IActionResult Reserved()
        {
            // Get all people that has reserved a dog and the dog in question
            var filteredList = (from person in _context.Person.Include(x => x.Dog).ToList()
                                from dog in person.Dog
                                where dog.Reserved
                                select person).ToList();

            return View("Index",filteredList);
        }

        //GET - Adopted
        [Authorize]
        public IActionResult Adopted()
        {
            //Get all people that has adopted a dog and the dog in question
            var filteredList = (from person in _context.Person.Include(x => x.Dog).ToList()
                                from dog in person.Dog
                                where dog.Adopted
                                select person).ToList();
            return View("Index", filteredList);
        }
    }
}
