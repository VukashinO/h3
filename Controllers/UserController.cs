using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaHut.Models;
using PizzaHut.Models.IRepositories;
using PizzaHut.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PizzaHut.Controllers
{
    public class UserController : Controller
    {
        //dependency injection
        private IUserRepository users;
        public UserController(IUserRepository repository)
        {
            users = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.TotalCount = users.GetAll().Count();
            ViewData.Add("TotalCount2", users.GetAll().Count());
            ViewData["TotalCount3"] = users.GetAll().Count();
            var model = users.GetAll();
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateUserViewModel model)
        {
            if(ModelState.IsValid)
            {
                User user = new User();
                
                user.Name = model.Name;
                user.Email = model.Email;
                user.Address = model.Address;
                user.City = model.City;
                user.Phone = model.Phone;
                users.Add(user);
                RedirectToAction("index");
             }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            User user = users.Get(id);
            if (user == null)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel
                {
                    RequestId = id.ToString()
                };
                return View("Error", errorViewModel);
            }

            CreateUserViewModel userViewModel = new CreateUserViewModel
            {
                Id = user.ID,
                Name = user.Name,
                Address = user.Address,
                Phone = user.Phone,
                Email= user.Email,
                City = user.City
                
            };
            return View(userViewModel);

        }

        [HttpPost]
        public IActionResult Edit(CreateUserViewModel model)
        {
            User user = users.Get(model.Id);
            if (ModelState.IsValid)
            {
                user.Name = model.Name;
                user.Phone = model.Phone;
                user.Address = model.Address;
                user.City = model.City;
                user.Email = model.Email;

                users.Update(user);
                return View("index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var model = users.Get(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(User user)
        {
            if (ModelState.IsValid)
            {
                users.Delete(user.ID);
                return RedirectToAction("index");
            }
            return View();
        }
    }
}
