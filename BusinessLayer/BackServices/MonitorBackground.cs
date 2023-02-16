using AccessLayer.Data;
using AutoMapper;
using BusinessLayer.BusinessUnits;
using BusinessLayer.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModelsLayer.Business;
using ModelsLayer.Dtos;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace BusinessLayer.BackServices
{
    public class MonitorBackground : BackgroundService
    {
        private IUnitOfWork unitOfWork;

        public static  ConcurrentDictionary<int, MonitorUrls> _urlTimers;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private IServiceScope scope;

        public MonitorBackground(IServiceScopeFactory serviceScopeFactory)
        {
            //  this.unitOfWork = unitOfWork;
            //context, IMapper mapper, EmailHelper emailHelper
            _urlTimers = new ConcurrentDictionary<int, MonitorUrls>();
            //this.unitOfWork = new UnitOfWork(new ApplicationDbContext(), mapper, emailHelper);
            _serviceScopeFactory = serviceScopeFactory;
            scope = serviceScopeFactory.CreateScope();
            unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var urls =  this.unitOfWork.TargetApplication.GetAsync().GetAwaiter().GetResult();
            foreach (var url in urls)
            {
                Timer timer = new Timer(this.unitOfWork.CheckApplicationUrl, url, TimeSpan.Zero, TimeSpan.FromMilliseconds(url.Interval * 1000));
                _urlTimers.TryAdd(url.Id, new MonitorUrls() { Application = url, TimerCall = timer });
            }

            return Task.CompletedTask;
        }

      

    }
}
