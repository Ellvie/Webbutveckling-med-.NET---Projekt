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
    public class DogsController : Controller
    {
        private readonly DogContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public DogsController(DogContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Dogs
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dog.Include(x => x.Person).ToListAsync());
        }

        // GET: Dogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dog = await _context.Dog
                .FirstOrDefaultAsync(m => m.DogId == id);
            if (dog == null)
            {
                return NotFound();
            }

            return View(dog);
        }

        // GET: Dogs/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DogId,Name,Age,Breed,Gender,Upload,Description,Reserved,Adopted")] Dog dog)
        {
            if (ModelState.IsValid)
            {
                //Check if picture is uploaded
                if (dog.Upload != null)
                {
                    //Set filepath for pics folder in wwwroot
                    var picsFilePath = Path.Combine(webHostEnvironment.WebRootPath, "pics");
                    //Set picture path
                    var picture = Path.Combine(picsFilePath, dog.Upload.FileName);
                    //Check if uploaded picture already exists
                    if (!System.IO.File.Exists(picture))
                    {
                        //Saves picture if upload doesn't exists
                        dog.Upload.CopyTo(new FileStream(picture, FileMode.Create));
                    }

                    //Set the picture i model
                    dog.Pic = dog.Upload.FileName;
                }
                else
                {
                    //If no picture is uploaded set default picture
                    dog.Pic = "default.png";
                }

                _context.Add(dog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dog);
        }

        // GET: Dogs/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dog = await _context.Dog.FindAsync(id);
            if (dog == null)
            {
                return NotFound();
            }
            return View(dog);
        }

        // POST: Dogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DogId,Name,Gender,Age,Breed,Upload,Description,Reserved,Adopted,Pic")] Dog dog)
        {
            if (id != dog.DogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (dog.Upload != null)
                    {
                        //Set filepath for pics folder in wwwroot
                        var picsFilePath = Path.Combine(webHostEnvironment.WebRootPath, "pics");
                        //Set picture path
                        var picture = Path.Combine(picsFilePath, dog.Upload.FileName);
                        //Check if uploaded picture already exists
                        if (!System.IO.File.Exists(picture))
                        {
                            //Saves picture if upload doesn't exists
                            dog.Upload.CopyTo(new FileStream(picture, FileMode.Create));
                        }

                        //Set the picture i model
                        dog.Pic = dog.Upload.FileName;
                    }
                    _context.Update(dog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DogExists(dog.DogId))
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
            return View(dog);
        }

        // GET: Dogs/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dog = await _context.Dog
                .FirstOrDefaultAsync(m => m.DogId == id);
            if (dog == null)
            {
                return NotFound();
            }

            return View(dog);
        }

        // POST: Dogs/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dog = await _context.Dog.FindAsync(id);
            _context.Dog.Remove(dog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DogExists(int id)
        {
            return _context.Dog.Any(e => e.DogId == id);
        }
    }
}
