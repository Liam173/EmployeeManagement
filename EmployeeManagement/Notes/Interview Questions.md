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