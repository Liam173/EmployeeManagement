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
