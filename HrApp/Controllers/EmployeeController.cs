using HrApp.Data;
using HrApp.Models;
using HrApp.Services.Interfaces;
using HrApp.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HrApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _service;

        public EmployeeController(IEmployeeRepository service)
        {
            _service = service;
        }

        // GET: Employee
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await _service.GetAll();
            return View(employees);
        }

        // GET: Employee/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            var employee = await _service.GetById(id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        // GET: Employee/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,FirstName,LastName")] Employee employee)
        {
            if (!ModelState.IsValid)
                return View(employee);

            await _service.Add(employee);
            return RedirectToAction(nameof(Index));
        }

        // GET: Employee/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var employee = await _service.GetById(id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,FirstName,LastName")] Employee employee)
        {
            if (!ModelState.IsValid)
                return View(employee);

            await _service.Update(employee);
            return RedirectToAction(nameof(Index));
        }

        // GET: Employee/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            var employee = await _service.GetById(id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _service.GetById(id);

            if (employee == null)
                return NotFound();

            await _service.Delete(employee);
            return RedirectToAction(nameof(Index));
        }
    }
}
