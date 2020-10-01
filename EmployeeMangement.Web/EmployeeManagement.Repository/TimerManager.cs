﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Repository
{
    public class TimerManager
    {
        private Timer _timer;
        private AutoResetEvent _autoResetEvent;
        private Action _action;
        public DateTime TimerStarted { get; }
        public TimerManager(Action action)
        {
            _action = action;
            _autoResetEvent = new AutoResetEvent(false);
            _timer = new Timer(Execute, _autoResetEvent, 2000, 2000);
            TimerStarted = DateTime.Now;
        }
        public void Execute(object stateInfo)
        {
            _action();
            if ((DateTime.Now - TimerStarted).Seconds > 120)
            {
                _timer.Dispose();
            }
        }
    }
}