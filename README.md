This repository aims to present a CRUD using MongoDB and Asp.Net.

The focus of this project was the implementation of some **MongoDB** functions in **Asp.Net**, in addition to this explanation below you can consult the project files to see the configuration and implementation of the other components of the Api.

## Docker use

To build and run only api with docker you can use these commands in the same folder as the **Dockerfile** file :

    docker build --rm -t myapp .

    docker run --rm -p 5000:5000 myapp

Or you can use Docker Compose with the command:
    
    docker compose up

## Packages
    dotnet add package MongoDB.Driver

## Config

File that will be used as a base for MongoDB configuration information.

    public class MongoOptions
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }

The data that is stored in these class fields will be found in the project's "appsettings.json" or "appsettings.Development.json" file (if mongo is installed locally).

    {
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft.AspNetCore": "Warning"
        }
      },
      "AllowedHosts": "*",
      "MongoConnection": {
        "ConnectionString": "mongodb://mongo:27017",
        "Database": "Tasks"
      }
    }

Below is setup used to register components and options

    public static class DependencyInjection
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IMongoContext, MongoContext>();

            services.AddScoped<ITodoTaskReadRepository, TodoTaskReadRepository>();
            services.AddScoped<ITodoTaskWriteRepository, TodoTaskWriteRepository>();
        }

        public static void RegisterOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoOptions>(configuration.GetSection("MongoConnection"));
        }
    }

The methods of the **DependencyInjection** class are used in the **Startup** class

    public void ConfigureServices(IServiceCollection services)
    {
        ...
        services.RegisterOptions(Configuration);
        services.RegisterRepositories();
        services.RegisterServices();
    }

## Context class implementation

The **MongoContext** class will represent our context.

    public class MongoContext : IMongoContext
    {
        private readonly MongoOptions _mongoOptions;
        private readonly MongoClient _mongoClient;

        public MongoContext(IOptions<MongoOptions> options)
        {
            _mongoOptions = options.Value;
            _mongoClient = new MongoClient(_mongoOptions.ConnectionString);
        }

        public IMongoDatabase GetDatabase() => _mongoClient.GetDatabase(_mongoOptions.Database);
    }

    public interface IMongoContext
    {
        IMongoDatabase GetDatabase();
    }

## Model class implementation

When creating our entity that will be used in the database, we need to define the identifier, in this case I defined it with the tag **[BsonId(IdGenerator = typeof(GuidGenerator))]**, there are other ways to set the id.

    public class TodoTask
    {
        [BsonId(IdGenerator = typeof(GuidGenerator))]
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public IEnumerable<SubTask>? Chields { get; set; }
    }
    public class SubTask
    {
        public string Description { get; set; }
        public IEnumerable<SubTask>? Chields { get; set; }
    }

## Repository class implementation

Here in our repository implementation, I first needed to get the **Tasks** collection in the constructor. In the following methods, I used the methods of the **IMongoCollection** class to perform the operations of searching, updating, inserting and deleting items in the database.

    public interface ITodoTaskReadRepository
    {
        Task<Domain.TodoTask> GetAsync(Guid id);
        Task<IEnumerable<Domain.TodoTask>> GetAsync();
    }

    public interface ITodoTaskWriteRepository
    {
        Task<Domain.TodoTask> CreateAsync(Domain.TodoTask task);
        Task UpdateAsync(Domain.TodoTask task);
        Task UpdateAsync(Guid id, DateTime finishTask, bool isDone);
        Task DeleteAsync(Guid id);
    }


    public class TodoTaskReadRepository : ITodoTaskReadRepository
    {
        private readonly IMongoCollection<Domain.TodoTask> _taskRepository;

        public TodoTaskReadRepository(IMongoContext context)
        {
            _taskRepository = context.GetDatabase().GetCollection<Domain.TodoTask>("Tasks");
        }

        public async Task<Domain.TodoTask> GetAsync(Guid id) =>
            await _taskRepository.Find(a => a.Id == id).SingleOrDefaultAsync();


        public async Task<IEnumerable<Domain.TodoTask>> GetAsync() =>
            await _taskRepository.Find(a => true).ToListAsync();

    }

    public class TodoTaskWriteRepository : ITodoTaskWriteRepository
    {
        private readonly IMongoCollection<Domain.TodoTask> _taskRepository;

        public TodoTaskWriteRepository(IMongoContext context)
        {
            _taskRepository = context.GetDatabase().GetCollection<Domain.TodoTask>("Tasks");
        }

        public async Task<Domain.TodoTask> CreateAsync(Domain.TodoTask task)
        {
            await _taskRepository.InsertOneAsync(task);
            return task;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _taskRepository.DeleteOneAsync(a => a.Id == id);
        }

        public async Task UpdateAsync(Domain.TodoTask task)
        {
            await _taskRepository.ReplaceOneAsync(a => a.Id == task.Id, task);
        }
        public async Task UpdateAsync(Guid id, DateTime finishTask, bool isDone)
        {
            var filter = Builders<Domain.TodoTask>.Filter.Eq(nameof(Domain.TodoTask.Id), id);
            var update = Builders<Domain.TodoTask>.Update
                .Set(nameof(Domain.TodoTask.IsDone), isDone)
                .Set(nameof(Domain.TodoTask.FinishDate), finishTask);

            await _taskRepository.UpdateOneAsync(filter, update);
        }
    }


## This project was built with
* [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
* [MongoDb](https://www.mongodb.com/pt-br)
* [Swagger](https://swagger.io/)

## My contacts
* [LinkedIn](https://www.linkedin.com/in/henry-saldanha-3b930b98/)