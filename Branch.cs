using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git
{
    public sealed class Branch
    {
        public string Name { get; set; }

        private List<Branch> _branches = new List<Branch>();

        private List<Commit> _commits = new List<Commit>();

        public Branch()
        {
        }

        private Branch(string branchName)
        {
            Name = branchName;
        }

        public override string ToString()
        {
            return Name;
        }

        public void Create(string branchName)
        {

            if (_branches.Any(r => r.Name.ToLowerInvariant() == branchName.ToLowerInvariant()))
            {
                Console.WriteLine($"Error! Branch with name {branchName} already exist");
            }
            else
            {
                _branches.Add(new Branch(branchName));
                Console.WriteLine("Branch with name {0} created!", branchName);
            }
        }

        public Branch GetDefault()
        {
            return Get("master");
        }

        public Branch Get(string branchName)
        {
            if (!_branches.Any(r => r.Name.ToLowerInvariant() == branchName.ToLowerInvariant()))
            {
                throw (new KeyNotFoundException($"Error! Branch with name {branchName} doesn`t exist"));
            }
            else
            {
                return _branches.Find(r => r.Name == branchName);
            }
        }

        public void ShowAllBranches()
        {
            Console.WriteLine();
            Console.WriteLine("LIST OF BRANCHES");
            _branches.ForEach(b => Console.WriteLine($"Branch name '{b.Name}'"));
            Console.WriteLine();
        }

        public void Commit(string message, User user)
        {
            _commits.Add(new Commit(message, user));
            Console.WriteLine($"Commit with message '{message}' created by user {user.Email}!");
        }

        public void GetAllCommits()
        {
            if (_commits.Count == 0)
            {
                Console.WriteLine();
                Console.WriteLine($"There are no commits in branch '{Name}' yet!");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("LIST OF COMMITS");
                _commits.ForEach(c => Console.WriteLine("{0}, {1}, {2}, {3}", c.User.Name, c.User.Email, c.Message, c.Date));
                Console.WriteLine();
            }
        }

        public int CommitsCount()
        {
            return _commits.Count;
        }

        public List<Commit> GetCommits()
        {
            return _commits;
        }
    }
}
