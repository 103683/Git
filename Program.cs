using Git.Enum;
using System;
using System.Threading.Tasks;

namespace Git
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var alex = new User("Alex", "Alex@email.com");
            var taras = new User("Taras", "Taras@email.com");
            var vlad = new User("Vlad", "Vlad@email.com");

            //repo
            alex.GetRepository("lolo");
            alex.CreateRepository("EngineProcessor", "https://github.com", "repo for some abstract engine processor");

            taras.GetRepository("EngineProcessor");
            taras.ShowAllRepositories();
            taras.CreateBranch("master");

            vlad.GetRepository("hello");
            vlad.GetRepository("EngineProcessor");
            vlad.ShowAllBranches();
            vlad.CreateBranch("dev");
            vlad.ChangeWorkBranch("dev");

            taras.ShowAllBranches();
            taras.ChangeWorkBranch("feature/b1");

            vlad.ShowCurrentBranch();
            vlad.CreateBranch("feature/b1");
            vlad.ChangeWorkBranch("feature/b1");

            taras.ShowCurrentBranch();
            taras.ShowAllBranches();

            alex.Commit("first commit");

            vlad.ShowAllCommits();
            vlad.ChangeWorkBranch("master");
            vlad.ShowAllCommits();

            var prId = Guid.NewGuid();
            taras.CreatePullRequest(vlad, "test pull request", "master", prId, new string[] { "tests", "EngineProcessor"});

            alex.ShowAllPullRequests();

            vlad.ChangePullRequestStatus(prId, Status.Merged);

            taras.ChangeWorkBranch("feature/b1");
            taras.Commit("Commit in feature/b1 branch");
            var tarasPullRequestId = Guid.NewGuid();
            taras.CreatePullRequest(vlad, "pro pull request", "master", tarasPullRequestId, new string[] {"taras", "normas"});

            vlad.ChangePullRequestStatus(tarasPullRequestId, Status.Merged);
            vlad.ChangeWorkBranch("master");
            vlad.ShowAllCommits();

            alex.ShowAllCommits();

            vlad.CreateRepository("ApiTests", "https://github.api.com", "repo for some abstract api tests");

            taras.ShowAllRepositories();
            taras.GetRepository("ApiTests");
            taras.ShowAllBranches();
            taras.ShowAllCommits();
            taras.ShowAllPullRequests();
        }
    }
}
