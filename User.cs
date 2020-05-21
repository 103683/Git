using Git.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git
{
    public sealed class User
    {
        public string Name { get; private set; }

        public string Email { get; private set; }

        private Repository _repository;

        private Branch _workBranch;

        public User(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public void CreateRepository(string name, string uri, string description)
        {
            Repository.Create(name, uri, description);
            this._repository = Repository.Get(name);
            _workBranch = this._repository.GetDefaultBranch();
        }

        public void GetRepository(string name)
        {
            try
            {
                this._repository = Repository.Get(name);
                _workBranch = this._repository.GetDefaultBranch();
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine($"Repository with name {name} does not exist.");
            }
        }

        public void ShowAllRepositories()
        {
            _repository.ShowAllRepositories();
        }

        public void ChangeWorkBranch(string branchName)
        {
            var branch = this._repository.GetBranch(branchName);
            if (branch != null)
                _workBranch = branch;
        }

        public void ShowCurrentBranch()
        {
            Console.WriteLine($"Current branch is {_workBranch.ToString()}");
        }

        public void CreateBranch(string branchName)
        {
            _repository.CreateBranch(branchName);
            _workBranch = _repository.GetBranch(branchName);
        }

        public void ShowAllBranches()
        {
            _repository.ShowBranchList();
        }

        public void Commit(string message)
        {
            _workBranch.Commit(message, this);
        }

        public void ShowAllCommits()
        {
            _workBranch.GetAllCommits();
        }

        public void CreatePullRequest(User assignee, string description, string branchReceiver, Guid id, string[] label = null)
        {
            if (_workBranch.CommitsCount() == 0)
            {
                Console.WriteLine("Error! There are no commits can be merged!");
            }
            else
            {
                var receiverBranch = _repository.GetBranch(branchReceiver);
                _repository.CreatePullRequest(this, assignee, description, _workBranch, receiverBranch, id, label);

            }
        }

        public void ChangePullRequestStatus(Guid id, Status status)
        {
            _repository.ChangePullRequestStatus(id, status);
        }

        public void ShowAllPullRequests()
        {
            _repository.ShowAllPullRequests();
        }
    }
}