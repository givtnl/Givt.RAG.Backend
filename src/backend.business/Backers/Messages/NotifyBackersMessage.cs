using System;
using backend.business.Backers.Models;
using backend.business.Events.Models;
using backend.business.Participants.Models;

namespace backend.business.Backers.Messages
{
    public class NotifyBackersMessage
    {
        public BackerListModel Backer { get; set; }
        public ParticipantDetailModel Participant { get; set; }
        public EventDetailModel Event { get; set; }
        public NotifyBackersMessageDistance Distance { get; set; }
        public NotifyBackersMessageDuration Duration { get; set; }
    }

    public class NotifyBackersMessageDistance
    {
        public decimal Meters { get; set; }
        public decimal Kilometers { get; set; }
        public decimal Miles { get; set; }

        public NotifyBackersMessageDistance()
        {
            
        }
        public NotifyBackersMessageDistance(decimal distanceInMeters)
        {
            Meters = distanceInMeters;
            Kilometers = decimal.Round(distanceInMeters / 1000,2);
            Miles = decimal.Round(Kilometers / 1.6m, 2);
        }
    }

    public class NotifyBackersMessageDuration
    {
        public double Hours { get; set; }
        public double Minutes { get; set; }
        public double Seconds { get; set; }
        
        public string FormattedTime {get; set;}
        public NotifyBackersMessageDuration()
        {
            
        }

        public NotifyBackersMessageDuration(TimeSpan duration)
        {
            Hours = Math.Round(duration.TotalHours,2);
            Minutes = Math.Round(duration.TotalMinutes,2);
            Seconds = duration.TotalSeconds;
            FormattedTime = duration.ToString("hh\\:mm\\:ss");
        }

    }
}