namespace TimeHelper
{
    public class Alarm
    {
        private Timer _timer;
        private DateTime _date;
        private TimeSpan? _repeatInterval;
        private object? _state;
        public event TimeHasComeHandler TimeHasCome;

        public delegate void TimeHasComeHandler(object sender, DateTime date, object? state); 

        public Alarm(DateTime date, object? state) : this(date, state, null) { }

        public Alarm(DateTime date, object? state, TimeSpan? repeatInterval)
        {
            if (date < DateTime.Now) throw new Exception("Date cannot be less than current date");
            _repeatInterval = repeatInterval;
            _date = date;
            _state = state;
            _timer = new Timer(CheckStatus, state, date - DateTime.Now, repeatInterval ?? TimeSpan.Zero);
        }

        private void CheckStatus(object? state)
        {
            TimeHasCome?.Invoke(this, _date, _state);
            if (_repeatInterval == null) _timer.Dispose();
        }
    }
}
