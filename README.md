[![Build status](https://ci.appveyor.com/api/projects/status/oef9812i20ct8gi7?svg=true)](https://ci.appveyor.com/project/mkokabi/marsroverakka)

# MarsRoverAkka
This is a sample based on *Mars Rover Challenge* story.  
I have decided to implement it using [Akka.net](https://getakka.net/index.html).

Rover is the main actor derived from *ReceivePersistentActor*. 
The other actor in this scenario is Console which is derived from *UntypedActor*. 
To make the sample application easier to run, persistence is an optional feature which can be enabled by enabled by passing **persist** argument to the application. 
To use the persistence feature the database (SQL) connection should be defined in the Sql.conf file. 
The format of this file is [HOCON](https://getakka.net/articles/concepts/configuration.html) (Human-Optimized Config Object Notation)

The code has been covered with over 50 unit tests.
