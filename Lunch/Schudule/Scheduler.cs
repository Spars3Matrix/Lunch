using System;

namespace Lunch.Schedule
{
    public class Scheduler 
    {
        private readonly DateTime Moment = DateTime.Now;

        public Scheduler() {}

        public Scheduler(DateTime moment)
        {
            Moment = moment;
        }

        public DateTime Start
        {
            get
            {
                return Moment.AddDays((int) Moment.DayOfWeek * -1);
            }
        }

        public DateTime End
        {
            get
            {
                return Moment.AddDays(6 - (int) Moment.DayOfWeek);
            }
        }
    }
}