using Git.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git
{
    public sealed class PullRequest
    {
        public Guid Id { get; set; }

        public Status Status { get; set; }

        public string Description { get; set; }

        public Branch BranchSource { get; set; }

        public Branch BranchReceiver { get; set; }

        public User Author { get; set; }

        public User Assignee { get; set; }

        public string[] Label { get; set; }
    }
}
