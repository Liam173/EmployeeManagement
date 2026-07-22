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

# Question 23:

If anyone can decode a JWT, why is it still considered secure?

# Answer:

A JWT is secure because, although its contents can be decoded by anyone, it is digitally signed using a secret key known only to the server. 

If someone modifies the payload—for example, changing their role from 'Employee' to 'Admin'—the signature becomes invalid. 

When the API recalculates the signature using its secret key, it no longer matches the one in the token, so the request is rejected.

-----------------------------------------------------------------------

# Question 24:

Why not just put the entire User object inside the JWT?

# Answer:

JWTs are sent with every authenticated request, so they should remain as small as possible and only contain claims that are frequently needed for 
authentication or authorization. 

Large or sensitive data should remain in the database.

-----------------------------------------------------------------------

# Question 25:

What problem does caching solve?

# Answer:

Caching reduces repeated access to expensive resources, such as databases or external services, 
by storing frequently requested data in memory or another fast storage layer. 

This improves response times, reduces system load, and increases application scalability.

-----------------------------------------------------------------------

# Question 26:

Should we cache every endpoint?

# Answer:

No. Caching should be driven by the read-to-write ratio, the cost of retrieving the data, how frequently it changes, 
and whether the application can tolerate temporarily stale data.

-----------------------------------------------------------------------

# Question 27:

When should you invalidate a cache?

# Answer:

Whenever the underlying data changes. The cache should no longer be trusted once the source of truth has been updated.

-----------------------------------------------------------------------

# Question 28:

Cache removed, then suddenly 500 users call the GET endpoint at the exact same time. What problem might this cause?

# Answer:

When many requests encounter an empty cache at the same time, they can all query the database simultaneously. 

This is known as a cache stampede. To prevent it, we can coordinate cache population using a lock or semaphore so that only one request rebuilds the cache while 
the others wait and then read the populated value.

-----------------------------------------------------------------------
# Question 29:

Why don't we cache everything?

# Answer:

Because caching introduces stale data, memory usage, invalidation complexity, and synchronization challenges. 

Whether data should be cached depends on how frequently it changes, how expensive it is to retrieve, and whether the business can tolerate temporarily stale information.

-----------------------------------------------------------------------

# Question 30:

When would you use absolute expiration?

# Answer:

When data has a predictable lifetime or when cache invalidation events aren't guaranteed. Absolute expiration ensures stale data is eventually removed, 
even if no explicit invalidation occurs.

-----------------------------------------------------------------------

# Question 31:

When would you use sliding expiration?

# Answer:

When frequently accessed data should remain in memory while inactive data naturally expires, helping manage memory usage.

-----------------------------------------------------------------------

# Question 32:

Why use a message queue instead of making HTTP calls directly?

# Answer:

A message queue decouples the producer from downstream services. The API can complete its primary responsibility and return immediately, 
while secondary operations such as notifications, payroll, or auditing are processed asynchronously. This improves responsiveness, resilience, and scalability.

-----------------------------------------------------------------------

# Question 33:

Why are queues more resilient than direct HTTP calls?

# Answer:

Queues decouple systems. If a downstream service is temporarily unavailable, the message can remain in the queue and be processed later instead of 
causing the original request to fail.

-----------------------------------------------------------------------

# Question 34:

Why must consumers be idempotent?

# Answer:

Because message brokers generally provide at-least-once delivery. A consumer may receive the same message multiple times, 
so processing it repeatedly must not produce duplicate or inconsistent results.

-----------------------------------------------------------------------

# Question 35:

Why isn't checking the database enough?

# Answer:

Because of race conditions. Two consumers could both check before either inserts the record. 

A database unique constraint ensures only one insert can succeed, making the operation truly safe.

-----------------------------------------------------------------------

# Question 36:

Why are event-driven systems easy to extend?

# Answer:

Because publishers are decoupled from consumers. The publisher only emits an event and doesn't know who processes it. 

New consumers can subscribe to the event without modifying the publishing service, making the architecture open for extension but closed for modification.

-----------------------------------------------------------------------

# Question 37:

Why use a Dead Letter Queue?

# Answer:

A Dead Letter Queue stores messages that couldn't be processed successfully after the configured retry policy. 

This prevents poison messages from blocking the main queue while allowing failed messages to be investigated and replayed later.

-----------------------------------------------------------------------

# Question 38:

How do you decide what to verify in unit tests?

# Answer:

I verify interactions that represent part of the business behaviour of the method. 

If removing a dependency call would introduce a bug while still allowing the method to compile and run, I want my unit tests to catch that.

-----------------------------------------------------------------------

# Question 39:

What is the difference between a Unit Test and an Integration Test?

# Answer:

Unit tests verify a single unit of behaviour in isolation by mocking external dependencies such as repositories, event publishers, or external services. 

Their goal is to test the business logic of a component without being affected by the database or other parts of the system. They are fast, deterministic, 
and ideal for testing individual methods.

Integration tests verify that multiple components work together correctly. 

They exercise the real application pipeline—including controllers, dependency injection, middleware, validation, AutoMapper, repositories, 
and the database—with little or no mocking. Although they are slower, they provide confidence that the application functions correctly as a whole.

-----------------------------------------------------------------------

# Question 40:

Why don't you verify repository calls in an integration test?

# Answer:

Because integration tests verify the behaviour of the application as a whole rather than the interactions between individual components. 

The repository is a real implementation, so instead of verifying that Add() was called, I verify the observable outcome—for example, 
that the API returns a 201 Created response and that the employee actually exists in the database.

-----------------------------------------------------------------------

# Question 41:

Why don't you verify repository calls in integration tests?

# Answer:

Because integration tests verify application behaviour rather than implementation details. 

I care that the employee exists in the database and the API returns the correct response, not whether the repository's Add() method was called.

-----------------------------------------------------------------------

