-----------------------------------------------------------------------

# Question 1:

Why use DTOs instead of returning entities?

# Answer:

DTOs decouple the API contract from the database model. 

They prevent exposing sensitive data, reduce the amount of data sent over the network, allow different data shapes for different endpoints, 
and make it possible to evolve the database without breaking API clients.

-----------------------------------------------------------------------

# Question 2:

Why not just return Employee from the repository all the way to the controller?

# Answer:

The repository returns domain models because that's what the application works with internally. 

The service maps those domain models to DTOs before returning them to the controller. 

This keeps the API contract independent from the database model, prevents exposing internal or sensitive data, 
and allows the internal model to evolve without breaking API consumers.

-----------------------------------------------------------------------

# Question 3:

What problem does AutoMapper solve?

# Answer:

AutoMapper eliminates repetitive object-to-object mapping code by automatically copying matching properties between types. 

It keeps services cleaner, reduces boilerplate, and centralizes mapping configuration, making the codebase easier to maintain.

-----------------------------------------------------------------------

# Question 4:

Why shouldn't controllers contain AutoMapper code?

# Answer:

The controller's responsibility is handling HTTP requests and responses. 

Mapping between DTOs and domain models is part of the application's business flow, so it belongs in the service layer. 

Keeping mapping out of controllers keeps them thin, easier to maintain, and follows the Single Responsibility Principle.

-----------------------------------------------------------------------

# Question 5:

ORM stands for?

# Answer:

Object Relational Mapper.

-----------------------------------------------------------------------

# Question 6:

What are Entity Framework Migrations?

# Answer:

Entity Framework Migrations are version-controlled changes to the database schema. 

They allow the database to evolve alongside the application's code by generating incremental changes—such as creating tables, adding columns, 
or modifying relationships—without losing existing data. 

Migrations ensure the database structure remains synchronized with the application's entity models across different environments.

-----------------------------------------------------------------------

# Question 7:

What does SaveChanges() do?

# Answer:

SaveChanges() commits all tracked changes made through the DbContext to the database. 

Entity Framework Core tracks entities that have been added, modified, or deleted, generates the appropriate SQL statements, 
and executes them within a transaction to ensure the changes are applied atomically.

-----------------------------------------------------------------------

# Question 8:

Why AddDbContext() and not AddScoped()?

# Answer:

AddDbContext() actually registers the ApplicationDbContext as a Scoped service behind the scenes, but it also configures EF Core with the database provider (SQL Server), 
connection string, and other EF-specific options.

So although a DbContext has a scoped lifetime, we don't register it manually with AddScoped(). We let EF Core do it correctly.

-----------------------------------------------------------------------

# Question 9:

Why do we inject ILogger<EmployeeService> instead of just ILogger?

# Answer:

ILogger<T> automatically associates log entries with the class that created them. 

This provides a logging category, making it much easier to filter, search, and troubleshoot logs in production. 

It also avoids manually specifying class names in every log message and keeps logging consistent across the application.

-----------------------------------------------------------------------

# Question 10:

Why should we use placeholders instead of string interpolation with ILogger?

# Answer:

Using placeholders enables structured logging. Instead of logging only a formatted string, the logger captures named properties like EmployeeId, 
allowing log aggregation tools to filter, search, and analyze logs by those values. 

It produces richer, more queryable logs while still generating the same readable message.

-----------------------------------------------------------------------

# Question 11:

Why would you use IOptions<T> instead of IConfiguration?

# Answer:

IOptions<T> binds a specific configuration section to a strongly typed class, making the code easier to read, test, and maintain. 

It also limits a class to only the configuration it needs, instead of giving it access to the entire application configuration through IConfiguration.

-----------------------------------------------------------------------

# Question 12:

Why return a DTO instead of a primitive type?

# Answer:

A DTO provides a stable API contract that can be extended without changing the endpoint's return type. 

It also makes the response self-describing and allows additional metadata to be included in the future.

-----------------------------------------------------------------------

# Question 13:

Why is DbContext registered as Scoped?

# Answer:

DbContext represents a unit of work for a single HTTP request.

A scoped lifetime ensures that all repositories and services participating in that request share the same context for change tracking and transactions, 
while keeping requests isolated from one another.

Using a singleton would cause thread-safety and state-sharing issues, and using a transient lifetime would break change tracking across operations.

-----------------------------------------------------------------------

# Question 14:

Why do we remove the cache instead of immediately updating it?

# Answer:

Removing the cache keeps the cache simple and avoids maintaining duplicate state. 

The next request repopulates the cache with the latest data from the database, 
ensuring the cache reflects the source of truth while avoiding unnecessary work if the data is never requested again.

-----------------------------------------------------------------------

# Question 15:

Should everything be cached?

# Answer:

No. I would cache data that's expensive to retrieve, requested frequently, and doesn't change often. 

I wouldn't cache highly dynamic data like timestamps or random values because the cache would quickly become stale or defeat the purpose of the endpoint.

-----------------------------------------------------------------------

# Question 16:

What is the Cache-Aside Pattern?

# Answer:

The Cache-Aside Pattern is a caching strategy where the application first checks the cache.

If the requested data exists, it is returned directly.

If it does not exist, the application retrieves the data from the database, stores it in the cache, and then returns it.

When data is updated or deleted, the relevant cache entry is removed so that the next request reloads fresh data from the database.

-----------------------------------------------------------------------

# Question 17:

What is loose coupling?

# Answer:

Loose coupling means one component has little or no knowledge of the implementation details of another component. 

Instead of calling services directly, components communicate through abstractions such as interfaces or events. 

This makes the system easier to extend, test, and maintain because new functionality can be added without modifying existing code.

-----------------------------------------------------------------------

# Question 18:

What's the difference between backward compatibility and forward compatibility?

# Answer:

New API versions don't break existing clients. 
Ex. v1 client -> v2 API

Older APIs can tolerate requests or data from newer clients. 
Ex. v2 client -> v1 API
This is harder to achieve and isn't always possible, but designing requests to ignore unknown fields can help.

-----------------------------------------------------------------------

# Question 19:

Why do we version APIs instead of just changing the existing endpoints?

# Answer:

Because an API is a contract with its consumers. 

Breaking that contract can cause production failures in client applications. 

Versioning allows us to introduce new functionality and evolve the API while maintaining backward compatibility for existing consumers until they 
have time to migrate.

-----------------------------------------------------------------------

# Question 20:

Why do we need integration tests if we already have unit tests?

# Answer:

Unit tests verify that individual classes behave correctly in isolation. 

Integration tests verify that the application's components work together correctly, including routing, middleware, dependency injection, authentication, 
serialization, and other framework features that unit tests don't exercise.

-----------------------------------------------------------------------

# Question 21:

What makes a good automated test?

# Answer:

- Independent (doesn't rely on other tests)
- Repeatable (same result every time)
- Fast
- Easy to understand
- Self-contained (arranges its own data)
- Tests one behavior

-----------------------------------------------------------------------

# Question 22:

Why not just keep retrying forever instead of using a circuit breaker?

# Answer:

Because once we've established that a dependency is unhealthy, continuing to send requests wastes resources, increases latency for our users, 
and puts additional load on the failing service. 

A circuit breaker fails fast, protects both systems, and periodically checks whether the dependency has recovered before allowing normal traffic again.

-----------------------------------------------------------------------