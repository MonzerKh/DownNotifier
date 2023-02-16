using AutoMapper;
using BusinessLayer.BackServices;
using BusinessLayer.BusinessUnits;
using DownNotifierMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelsLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DownNotifier.Controllers
{
    public class TargetApplicationController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TargetApplicationController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ActionResult> Index()
        {
            return View(await unitOfWork.TargetApplication.GetAsync());
        }

        public ActionResult Create()
        {
            return View();
        }

        public async Task<ActionResult> Details(int id)
        {
            // Retrieve the target application with the specified ID from the database
            var historyDtos = await unitOfWork.ApplicationHistory.GetByApplicationIdAsync(id);

            return View(historyDtos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("URL,Name,Interval")] TargetApplicationInsert targetApplication)
        {
            if (ModelState.IsValid)
            {
                var user = await unitOfWork.Users.GetAsync();
              
                targetApplication.SystemUser_Id = user.Id;
                // Add the target application to the database
                int Application_Id = await unitOfWork.TargetApplication.Add(targetApplication);

                await unitOfWork.AddApplication(Application_Id);

                return RedirectToAction(nameof(Index));
            }

            return View(targetApplication);
        }

        // GET: TargetApplications/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            // Retrieve the target application with the specified ID from the database
            var targetApplication = await unitOfWork.TargetApplication.GetByIdAsync(id);

            return View(mapper.Map<TargetApplicationUpdate>(targetApplication));
        }

        // POST: TargetApplications/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("Id,URL,Name,Interval,IsActive")] TargetApplicationUpdate targetApplication)
        {
            

            if (ModelState.IsValid)
            {
                // Update the target application in the database
                bool check = await unitOfWork.TargetApplication.Update(targetApplication);

                return RedirectToAction(nameof(Index));
            }

            return View(targetApplication);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var targetApplication = await unitOfWork.TargetApplication.GetByIdAsync(id.GetValueOrDefault());

            return View(targetApplication);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            // Delete the target application from the database
            unitOfWork.RemovApplication(id);

            await unitOfWork.TargetApplication.Delete(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
