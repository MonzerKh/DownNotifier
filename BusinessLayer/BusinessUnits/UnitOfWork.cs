using AccessLayer.Data;
using AutoMapper;
using BusinessLayer.BackServices;
using BusinessLayer.Repositories;
using BusinessLayer.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModelsLayer.Business;
using ModelsLayer.Dtos;
using ModelsLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLayer.BusinessUnits
{
    public interface IUnitOfWork
    {
        public ISystemUserService Users { get; }

        public ITargetApplicationReprository TargetApplication { get; }

        public IAppCheckHistoryReprository ApplicationHistory { get; }

        Task<bool> Complete();
        bool HasChanges();
        public void CheckApplicationUrl(object state);
        public Task AddApplication(int application_Id);
        void RemovApplication(int id);
        void SendEmail(string Title, string Message, string to);
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMapper _mapper;
        private readonly EmailHelper _emailHelper;
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context, IMapper mapper, EmailHelper emailHelper)
        {
            _context = context;
            _mapper = mapper;
            _emailHelper = emailHelper;
        }

        public ISystemUserService Users => new SystemUserService(_context, _mapper);
        public ITargetApplicationReprository TargetApplication => new TargetApplicationReprository(_context, _mapper);
        public IAppCheckHistoryReprository ApplicationHistory => new AppCheckHistoryReprository(_context, _mapper);

        public void CheckApplicationUrl(object state)
        {
            TargetApplicationDto targeApp = (TargetApplicationDto)state;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(targeApp.URL);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode < HttpStatusCode.OK || response.StatusCode >= HttpStatusCode.Ambiguous)
                {
                    // send notification message
                    Console.WriteLine("A notification message has been sent due to a response code other than 2XX for URL: " + targeApp.URL);
                    WriteStatus(targeApp.Id, false).GetAwaiter().GetResult();
                }
                else
                {
                    Console.WriteLine("The URL request was successful with a response code of 2XX for URL: " + targeApp.URL);
           
                    WriteStatus(targeApp.Id, true).GetAwaiter().GetResult();
                }
            }
            catch (Exception ex)
            {
                // send notification message
                Console.WriteLine("A notification message has been sent due to an error in the URL request for URL: " + targeApp.URL + " - " + ex.Message);
            }
        }


        public async Task  WriteStatus(int AppId,bool Status)
        {
            var AppCheck = new AppCheckHistoryInsert()
            {
                TargetApplication_Id = AppId,
                ExecuteTime= DateTime.Now,
                IsUp = Status
            };

            var unitapp = ServiceActivator.GetScope().ServiceProvider.GetRequiredService<IUnitOfWork>();
            await unitapp.ApplicationHistory.Add(AppCheck);
            
            if (Status == false)
            {
                var GetApp = await unitapp.TargetApplication.GetByIdAsync(AppId);

                unitapp.SendEmail(
                    Title: GetApp.Name ,
                    Message : $"The Application has been Stopped in :{AppCheck.ExecuteTime.ToShortTimeString()}" ,
                    to: GetApp.Email
                    );
            }
        }

        public async Task AddApplication(int application_Id)
        {
            var url = await TargetApplication.GetByIdAsync(application_Id);
            Timer timer = new Timer(CheckApplicationUrl, url, TimeSpan.Zero, TimeSpan.FromMilliseconds(url.Interval * 1000));
            MonitorBackground._urlTimers.TryAdd(url.Id, new MonitorUrls() { Application = url, TimerCall = timer });
        }
        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            _context.ChangeTracker.DetectChanges();
            var changes = _context.ChangeTracker.HasChanges();
            return changes;
        }

        public async void RemovApplication(int application_Id)
        {
            var url = await TargetApplication.GetByIdAsync(application_Id);
            MonitorUrls value;
            if (MonitorBackground._urlTimers.TryRemove(url.Id, out value))
            {
                value.TimerCall.Change(Timeout.Infinite, 0);
            }
        }

        public void SendEmail(string Title, string Message, string to)
        {
            _emailHelper.Send(Title, Message, to);
        }
    }
}
