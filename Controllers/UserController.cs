using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAOUserProject.DAL.Entity;
using DAOUserProject.DAL;
using System.Data.SqlClient;

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
                try {
                    userDAO.Create(user);
                }
                catch(Exception ex)
                {
                    Console.Write(ex.ToString());
                    if(ex.ToString().Contains("Duplicate entry") && ex.ToString().Contains("login"))
                    {
                        Console.Write("!!!");
                        ModelState.AddModelError("Login", "Duplicate entry detected!");
                        
                    } 
                    else if (ex.ToString().Contains("Duplicate entry") && ex.ToString().Contains("e-mail"))
                    {
                        ModelState.AddModelError("EMail", "Duplicate entry detected!");
                    }
                    return View(user);
                }
                
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
        public IActionResult Edit(int id, [Bind("Id,Delivery,EMail,FirstName,Icq,LastName,Login,Password")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }
            User oldUser = userDAO.Get(id);
            user.Password = oldUser.Password;
            user.CreatedAt = oldUser.CreatedAt;
            userDAO.Detach(oldUser);

            if (ModelState.IsValid)
            {
                userDAO.Update(user);
                return RedirectToAction("Index");
            }
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
