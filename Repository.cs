using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Git.Enum;

namespace Git
{
    public sealed class Repository
    {
        private string Name { get; set; }

        private string Uri { get; set; }

        private string Description { get; set; }

        private static List<Repository> _repository = new List<Repository>();

        private List<PullRequest> _pullRequests = new List<PullRequest>();

        private Branch Branch;

        private Repository(string name, string uri, string description) 
        {
            Name = name;
            Uri = uri;
            Description = description;

            Branch = new Branch();
            Branch.Create("master");
        }

        public static void Create(string name, string uri, string description)
        {
            if (_repository.Any(r=>r.Name.ToLowerInvariant() == name.ToLowerInvariant()))
            {
                Console.WriteLine($"Error! Repository with name {name} already exist");
            }
            else
            {
                _repository.Add(new Repository(name, uri, description));
                Console.WriteLine("Repository with name {0}, uri {1}, description {2} created!", name, uri, description);
            }
        }

        public static Repository Get(string name)
        {
            if (!_repository.Any(r => r.Name.ToLowerInvariant() == name.ToLowerInvariant()))
            {
                throw (new KeyNotFoundException($"Error! Repository with name {name} doesn`t exist"));
            }
            else
            {
                return _repository.Find(r => r.Name == name);
            }
        }

        public void ShowAllRepositories()
        {
            Console.WriteLine();
            Console.WriteLine("LIST OF REPOSITORIES");
            _repository.ForEach(r => Console.WriteLine("{0}, {1}, {2}", r.Name, r.Uri, r.Description));
            Console.WriteLine();
        }

        public void CreateBranch(string branchName)
        {
            Branch.Create(branchName);
        }

        public void ShowBranchList()
        {
            Branch.ShowAllBranches();
        }
        
        public Branch GetBranch(string branchName)
        {
            try
            {
                return Branch.Get(branchName);
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine($"Error! Branch with name {branchName} doesn`t exist");
                return null;
            }
        }

        public Branch GetDefaultBranch()
        {
            return Branch.GetDefault();
        }

        public void CreatePullRequest(User user, User assignee, string description, Branch source, Branch receiver, Guid id, string[] label = null)
        {
            if (source.Name == receiver.Name)
            {
                Console.WriteLine($"Error! Source branch {source.Name} == Receiver branch {receiver.Name}!");
            }
            else
            {
                _pullRequests.Add(new PullRequest()
                {
                    Author = user,
                    Assignee = assignee,
                    Id = id,
                    Status = Status.Open,
                    Description = description,
                    Label = label,
                    BranchReceiver = receiver,
                    BranchSource = source
                });
                Console.WriteLine("Pull request: Author {0}, Assignee {1}, SourceBranch {2}, ReceiverBranch {3}, Status {4} - Created!", user.Email, assignee.Email, source.Name, receiver.Name, Status.Open.ToString());
            }   
        }

        public void ChangePullRequestStatus(Guid id, Status status)
        {
            var pr = _pullRequests.Find(r => r.Id == id);

            if (pr == null)
            {
                Console.WriteLine($"Error! There are no PullRequest with id '{id}'");
                return;
            }

            if (status == Status.Merged)
            {
                pr.Status = status;
                var sourceBranchCommits = pr.BranchSource.GetCommits();
                var receiverBranchCommits = pr.BranchReceiver.GetCommits();
                receiverBranchCommits.AddRange(sourceBranchCommits);
            }
            else
            {
                pr.Status = status;
            }

            Console.WriteLine($"Pull Request id '{id}' status changed! New status: {status.ToString()}");
        }

        public void ShowAllPullRequests()
        {
            if(_pullRequests.Count == 0)
            {
                Console.WriteLine();
                Console.WriteLine("There are no pull requests in this repository!");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("PULL REQUESTS");
                _pullRequests.ForEach(p => Console.WriteLine("Author {0}, Assignee {1}, SourceBranch {2}, ReceiverBranch {3}, Status {4}", p.Author.Email, p.Assignee.Email, p.BranchSource.Name, p.BranchReceiver.Name, p.Status.ToString()));
                Console.WriteLine();
            }
        }

    }
}
