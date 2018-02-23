using System;

namespace FileWatcherService.Models
{
    public class Message
    {
        public int? Id { get; set; }

        public string Title { get; set; }

        public Uri Uri { get; set; }

        public DateTime? ChangedDate { get; set; }        
    }
}