# Assignment: Course Sign-up System

Below assignment consists of 3 parts. 

The assignment is intentionally made too big, we suggest you spend a maximum of 5 hours on it. We would like you to demonstrate the parts you did finish, preferably on your own laptop so you can debug live if necessary. Please prepare a 15-20 minute of presentation, explaining what you did and especially how you would take it further to completion: we want you to not only demonstrate how you code, but also how you transfer knowledge. The rest of the time (40-45 minutes) we will talk about the assignment, how you did it and what are other ways of approaching the problem.

We will evaluate your skills in the following areas:
- Dependency injection
- Writing testable code
- Exception handling and logging
- Asynchronous code (async/await)
- Asynchrony through messaging (events, commands)
- Web API (and OWIN)
- Storage technologies available on the Azure platform (SQL, Table Storage, Cosmos DB, ...)
- Code organisation (modularity, dependencies between modules, naming etc)
- Domain model design (OOP, DDD concepts etc)
- Handling concurrency and scaling out your solution

Please upload the code of the assignment and the presentation at least 24 hours before you present it. 

## Case description

You start working at a company that offers online courses.

For each of the courses, there is one teacher/lecturer, and for each of the courses
there is a maximum number of students that can participate. 

To sign up, students need to supply their name and their age.

## Part 1: API for signing up

Create an API endpoint with which students can sign up for a course. 

If a course is full, it should not be possible to sign up any more.

The endpoint's response should indicate whether signing up was successful.

## Part 2: Scaling out

After few months, the company's courses grow wildly successfull, business is 
booming. There are many courses and millions of sign ups, and your synchronous API 
cannot handle the load any more.

Create a new endpoint for your API that defers the actual processing to a 
worker process: signing up is processed asynchronously.

This works as follows. The API puts a command message on a queue, and the 
message is picked up by the worker process. The worker process tries to sign 
up the student; it then sends an e-mail to inform the student whether signing 
up succeeded.

You can implement "sending an email" with a mock implementation that logs 
success or failure. 

## Part 3: Querying

For analysis purposes, the company needs to know per course the minimum age, the
maximum age and the average age of students that signed up for the courses.
Consider that this needs to keep working efficiently when there are millions 
of sign-ups per day: calculating this information at every request is unfeasable.

Create two API endpoints:
- GET list: which returns a list with the above information for each course, plus
the course total capacity and current number of students
- GET details: which returns the above information for a single course, plus
the teacher and the list of registered students

## Wrapping up
Prepare few slides for your presentation.

Please attach the presentation file (pdf, ppt, keynote, google slides link) inside your project/repository test.

Required:
- Explore your architecture decision
- What tools and technologies you used (libraries, framework, tools etc)
- Why did you decide to use technology X and not Y
- How you solve each step (API, Scaling out and Querying)

Bonus:
- The problems and challenges that you have faced
- What you think that it can be improved and how
