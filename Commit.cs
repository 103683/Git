using System;
using System.Collections.Generic;
using System.Text;

namespace Git
{
    public sealed class Commit
    {
        public string Message { get; set; }

        public DateTime Date { get; set; }

        public User User { get; set; }

        public Commit(string message, User user)
        {
            Message = message;
            Date = DateTime.Now;
            User = user;
        }
    }
}
