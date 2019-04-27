[![Build status](https://ci.appveyor.com/api/projects/status/oef9812i20ct8gi7?svg=true)](https://ci.appveyor.com/project/mkokabi/marsroverakka)

# MarsRoverAkka
This is a sample based on *Mars Rover Challenge* story.  
I have decided to implement it using [Akka.net](https://getakka.net/index.html) to offload the distribution and concurrency problems. 

Rover is the main actor derived from *ReceivePersistentActor*. 
The other actor in this scenario is Console which is derived from *UntypedActor*. 

The interaction between **Rover** and **Console** is by sending messages as following types from Console:
* Plateau (should be first and only once)
* Initial position (should be after defining plateau)
* Move (could be multiple time)

Rover responses with following *OperationResults*:
* PlateauSetOperationResult
* InitialPositionSetOperationResult
* MovedOperationResult 
* FailOperationResult

The first 3 results derived from SuccessOperationResult which itself is derived OperationResult but the last one is directly derived from OperationResult.

## Persistence
To make the sample application easier to run, persistence is an optional feature which can is enabled by passing **persist** argument to the application. 
To use the persistence feature the database (SQL) connection should be defined in the Sql.conf file. 
The format of this file is [HOCON](https://getakka.net/articles/concepts/configuration.html) (Human-Optimized Config Object Notation).
The persistence model currently used is *Journaling*, assuming replaying all the messages to the *Rover* is required to get to the same state. 
In other cases *Snapshot* model can be used (or even combined with *Journaling*) to save the storage and speed up retreiving to the initial stage. 

## Tests
The code has been covered with over 50 unit tests. 
There is an automated build setup on [appveyor](https://www.appveyor.com/) 
