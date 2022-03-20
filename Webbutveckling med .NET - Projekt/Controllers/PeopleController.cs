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
            if (ModelState.IsValid)
            {
                var dog = _context.Dog.Find(person.DogId);
                dog.Reserved = true;
                person.Dog.Add(dog);
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("");
        }

        // GET: People/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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
            if (id != person.PersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
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
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.PersonId == id);
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
            var person = await _context.Person.FindAsync(id);
            _context.Person.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.PersonId == id);
        }

        [Authorize]
        public IActionResult Reserved()
        {
            var filteredList = (from person in _context.Person.Include(x => x.Dog).ToList()
                                from dog in person.Dog
                                where dog.Reserved
                                select person).ToList();

            return View("Index",filteredList);
        }

        [Authorize]
        public IActionResult Adopted()
        {
            var filteredList = (from person in _context.Person.Include(x => x.Dog).ToList()
                                from dog in person.Dog
                                where dog.Adopted
                                select person).ToList();
            return View("Index", filteredList);
        }
    }
}
