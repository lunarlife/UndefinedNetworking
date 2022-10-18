using System;
using UndefinedNetworking.Exceptions;

namespace UndefinedNetworking.Core
{
    public class Timer
    {
        private DateTime? _startTime;
        private DateTime? _timeRemain;
        
        public Timer(bool autoStart = true)
        {
            if(autoStart) Start();
        }

        public void Start() => _startTime = _startTime is null && _timeRemain is null ? DateTime.Now : throw new TimerException("timer is started");

        public void Stop()
        {
            if(_startTime is null) throw new TimerException("timer is not started");
            _timeRemain = new DateTime((DateTime.Now - _startTime!.Value).Ticks);
        }

        public void Reset()
        {
            _startTime = null;
            _startTime = null;
        }

        public override string ToString()
        {
            if(_timeRemain is null) throw new TimerException("timer is not stopped");
            return $"{_timeRemain:t}";
        }
    }
}