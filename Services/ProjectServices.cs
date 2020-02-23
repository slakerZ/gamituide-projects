using ProjectsApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace ProjectsApi.Services
{
    public class ProjectService
    {
        private readonly IMongoCollection<Project> _Projects;

        public ProjectService(IProjectsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _Projects = database.GetCollection<Project>(settings.ProjectsCollectionName);
        }

        public List<Project> Get() =>
            _Projects.Find(Project => true).ToList();

        public Project Get(string id) =>
            _Projects.Find<Project>(Project => Project.Id == id).FirstOrDefault();

        public Project Create(Project Project)
        {
            _Projects.InsertOne(Project);
            return Project;
        }

        public void Update(string id, Project ProjectIn) =>
            _Projects.ReplaceOne(Project => Project.Id == id, ProjectIn);

        public void Remove(Project ProjectIn) =>
            _Projects.DeleteOne(Project => Project.Id == ProjectIn.Id);

        public void Remove(string id) => 
            _Projects.DeleteOne(Project => Project.Id == id);
    }
}