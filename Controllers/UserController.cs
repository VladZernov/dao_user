using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAOUserProject.DAL.Entity;
using DAOUserProject.DAL;

namespace DAOUserProject.Controllers
{
    public class UserController : Controller
    {
        private IDAO<User> userDAO;

        public UserController(IDAO<User> userDAO)
        {
            this.userDAO = userDAO;
        }

        // GET: User
        public IActionResult Index()
        {
            return View(userDAO.GetAll());
        }

        // GET: User/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = userDAO.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,CreatedAt,Delivery,EMail,FirstName,Icq,LastName,Login,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                userDAO.Create(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: User/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = userDAO.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,CreatedAt,Delivery,EMail,FirstName,Icq,LastName,Login,Password")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Console.WriteLine(user.Login);
                userDAO.Update(user);
                return RedirectToAction("Index");
            }
            user.Password = null;
            return View(user);
        }

        // GET: User/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = userDAO.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var user = userDAO.Get(id);
            userDAO.Delete(user);
            return RedirectToAction("Index");
        }

    }
}
