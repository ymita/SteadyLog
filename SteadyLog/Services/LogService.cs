using SteadyLog.Data;
using SteadyLog.Models;
using SteadyLog.Models.LogViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteadyLog.Services
{
    public class LogService
    {
        private readonly ApplicationDbContext _context;
        private readonly ActivityService _activityService;
        public LogService(
            ApplicationDbContext context,
            ActivityService activityService
            )
        {
            this._context = context;
            this._activityService = activityService;
        }

        public List<LogViewModel> GetLogViewModels()
        {
            // Log list
            var logs = _context.Logs.ToList();
            // Activity list
            var activities = _context.Activities.ToList();
            // Merge them into one
            List<LogViewModel> logViewModels = new List<LogViewModel>();
            

            for(var i = 0; i < logs.Count(); i++)
            {
                var log = logs[i];
                var activity = activities.Find(x => x.Id == log.ActivityId);

                LogViewModel logViewModel = new LogViewModel();
                logViewModel.Id = log.Id;
                logViewModel.ActivityId = activity.Id;
                logViewModel.ActivityTitle = activity.Title;
                logViewModel.Amount = log.Amount;
                logViewModel.CreatedOn = log.CreatedOn;
                logViewModel.Comment = log.Comment;

                logViewModels.Add(logViewModel);
            }

            return logViewModels;
        }

        public LogViewModel GetLogViewModelById(int? id)
        {
            var log = _context.Logs.Find(id.Value);
            var activity = _context.Activities.Find(log.ActivityId);
            var logViewModel = new LogViewModel {
                Id = log.Id,
                ActivityId = activity.Id,
                ActivityTitle = activity.Title,
                Amount = log.Amount,
                CreatedOn = log.CreatedOn,
                Comment = log.Comment
            };
            return logViewModel;
        }

        public void UpdateViewModel(LogSaveViewModel logSaveViewModel)
        {
            var log = _context.Logs.Find(logSaveViewModel.Id);
            log.Amount = logSaveViewModel.Amount;
            log.ActivityId = logSaveViewModel.ActivityId;
            log.Comment = logSaveViewModel.Comment;

            this._context.Update(log);
            this._context.SaveChanges();
        }

        public void AddViewModel(LogSaveViewModel logSaveViewModel)
        {
            var log = new Log();
            
            log.ActivityId = logSaveViewModel.ActivityId;
            log.Amount = logSaveViewModel.Amount;
            log.CreatedOn = DateTime.Now;
            log.Comment = logSaveViewModel.Comment;

            this._context.Add(log);
            this._context.SaveChanges();
        }

        public List<LogSaveViewModel> GetLogSaveViewModels()
        {
            List<LogSaveViewModel> logSaveViewModels = new List<LogSaveViewModel>();

            var vms = GetLogViewModels();
            var activities = _activityService.GetActivities();
            for (int i = 0; i < vms.Count; i++)
            {
                var vm = vms[i];
                var svm = new LogSaveViewModel {
                    Id = vm.Id,
                    ActivityId = vm.ActivityId,
                    Activities = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                        activities,
                        "Id",
                        "Title"
                    ),
                    Amount = vm.Amount,
                    CreatedOn = vm.CreatedOn,
                    Comment = vm.Comment
                };
                logSaveViewModels.Add(svm);
            }
            
            return logSaveViewModels;
        }

        public LogSaveViewModel GetLogSaveViewModelById(int? id)
        {
            var log = _context.Logs.Find(id.Value);
            var activity = _context.Activities.Find(log.ActivityId);
            var logSaveViewModel = new LogSaveViewModel {
                Id = log.Id,
                ActivityId = activity.Id,
                Activities = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                    _context.Activities.ToList(),
                    "Id",
                    "Title"
                ),
                //_context.Activities.ToList(),
                Amount = log.Amount,
                CreatedOn = log.CreatedOn,
                Comment = log.Comment
            };
            return logSaveViewModel;
        }

        public LogSaveViewModel GetNewLogSaveViewModel()
        {
            var vm = new LogSaveViewModel();
            vm.Activities = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                _context.Activities.ToList(),
                "Id",
                "Title"
            );

            return vm;
        }
    }
}
