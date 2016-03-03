# Assignment of Tasks #
| **Task 1:** | Jared |
|:------------|:------|
| **Task 2:** | Nick  |
| **Task 3:** | Brian |
| **Task 4:** | Justin |

# Task 1: #
**a. Outline a High level appropriate Design or Software Architecture for your project. How do the requirements match with the chosen architecture?**

SEAT will be implemented using a model-view-controller architecture pattern.  The model will be implemented in a C# library.  This library, named SEATLibrary, and will implement all of the objects used by the program and a manager that is used to coordinate the various objects.  The controller and the model will be implemented within the GUI, which will be a separate C# project that uses the SEATLibrary.  For testing purposes, the SEATLibrary will also be used in TestingApplication, a command line program, which will provide most of the functionality that will be found in the GUI.

The requirements for the project work well with MVC.  However, the choice of MVC is primarily for the purpose of making development easier and producing code that is more maintainable.  The main part of the model, implemented in the SEATLibrary, can be quickly implemented.  This allows for the development of the GUI to continue while the model is enhanced as additional features are added.  As long as these two groups communicate, the development can proceed on parallel tracks.


**b. Identify the types of design goals or tasks that solve specific problems. From these identify the software patterns that will be useful in your project.**

The primary goal in the design of SEAT is to produce code that is maintainable.  As this product will be in use after it has been developed, bugs and enhancements need to be easy.  Releasing the code under an open source license helps with this goal, but without a maintainable design the program may become unusable over time.  Tasks for the project will be divided in such a way the the components can be developed in a modular fashion.  Having the program be loosely coupled will force each component to be implemented in such a way that does not allow for complicated dependencies.

**c. Prepare a complete UML based Software Design, with the following diagrams:**

  * **A Class diagram. ( Are the good classes in Task 1 in here? Explain.)**

<table><tr><td><a href='http://picasaweb.google.com/lh/photo/n_Cdmo5HJKwLUDSKCANQaQ?feat=embedwebsite'><img src='http://lh4.ggpht.com/_l4Rv5VGKzLQ/SsIsFI0lFiI/AAAAAAAAAWg/Eqnp85NslOU/s400/ClassDiagram.png' /></a></td></tr><tr><td>From <a href='http://picasaweb.google.com/jjhatf02/GoogleCode?feat=embedwebsite'>GoogleCode Photo Gallery</a></td></tr></table>

The main classes used by the application are the student class, the chair class, and the room class.  The SeatManager class functions as the manager overseeing the collection of students and collections of rooms that are stored in memory.  Additionally, the SeatManager class will have the responsibility of writing and reading the save file to the disk.  Lastly, the SeatManager will be responsible for making and managing the template files for rooms that are created.

The AssignmentVisitor is an implementation of the visitor design pattern that will implement an algorithm for placing students into seats in a room.  Currently, the only implementation of AssignmentVisitor is AssignmentBestEffort, but other algorithms may be constructed that have a different algorithm for placing students into the chairs of a classroom.  The AssignmentVisitor operates on the Room class and will modify the instance of the room that is passed to it.  Various algorithms may have errors that will cause them to not be executed properly.  This will need to be implemented so that the results of the running of the algorithm can be returned to the user.

The object oriented structure of the GUI is not documented above.  This design will be based on the default patterns that are used when constructing a WPF application.  In part 4 of this document the GUI is documented and much of the class design work will directly reflect these GUI layouts.  The GUI will implement the view in the MVC architecture.  The responsibility of the controller will be divided between the SeatManager and the interface exposed by the GUI elements.

  * **Use cases and corresponding Use Case diagrams**

The use cases for this design are implemented in the model and can stand alone without a GUI implementation.  This is made possible because of the loose coupling between the model and the view.  This application only has one user level so the use cases are the same no matter who is using SEAT.  Because this application is so simple, the various uses cases are almost identical to


  * **Sequence Diagrams**

| **Step** | **Description** |
|:---------|:----------------|
| 1        | Import student roster |
| 2        | Update student information (handedness & vision impairment) |
| 3        | Add Room (import template or create new NxM room) |
| 4        | Customize room  |
| 5        | Add students to room |
| 6        | Manually place students in seats (optional) |
| 7        | Fill in seats using algorithm |
| 8        | Print a report  |

  * **State Charts or Dynamic Diagrams**

This project is not well suited to state diagrams.  The overall functionality of the application does not have distinct states that are moved between.  The best analogy to the states is as follows:

_Student_: Add student, remove student (Students can not be removed from the roster, rather they are flagged as not being enrolled anymore.  A student can be added or removed from a room or an individual chair.  If a student is removed from a room, they should also be removed from a chair.

_Room_: Add room, update room, remove room (It is possible to add, update, and delete a room.  The update process of the room is best suited to a state diagram as the updating of a room is done in two parts, first the room configuration and then students are placed in chairs.  However, the program does not place restriction on what order the user performs specific tasks other than keeping the internal model consistent.

  * **Collaboration Diagrams (optional)**

Not applicable.

  * **Timing Diagrams (Optional)**

Not applicable.

  * **Flow Charts for major complex methods (where needed)**

See the above sequence diagram for the flow of the application.

**d. Mark the portions of the diagram where the Voice of the Customer ( in requirements or otherwise) is addressed.**

Our requirements require that our program accurately model a class roster and a classroom.   The VOC is mainly represented in how the room is implemented and how the placement algorithm will operate.

The other component of the VOC is how the GUI will operate.  Thank to a MVC design pattern, these aspects of the program can be developed independent from each other.

**e. Organize the Software Design Document to follow the IEEE standards and augment the same with the various UML diagrams, by reference or link. Are other libraries, packages used in your design? If you have referred, used other work on the internet that directly bears on your work, do note the same so that credit is given to existing open work. Note how your work will differ from theirs.**

Our design and implementation is not based off of any other work.  Any library used in our design and implementation is part of the .NET framework.

# Task 2: #
**Note how you intend to address Test Driven Design/Implementation/Development.**

**a. Which of the above diagrams are candidates for testing during the development? Are there aspects of design that will never be tested? Mark these as potentially for future work and agility.**

Almost every portion of the class diagram is a candidate for testing during the development process.  The most important objects that need to be tested are obviously the most complex classes, such as the Student, the Room, and the Chair.  All types of different students need to be considered to be placed in a room, and in a chair.  This all requires passage through the SeatManager, so extensive testing must be done on this class.  The main feature of SEAT is going to be placing a list of students, so all possible combinations of student types need to be tested in order to ensure that the software's functionality is correct.

**b. Create a list of major error categories for your project. (Recall these are not just the compiler errors. These are design errors, process mismatches, plan changes and mismatches, documentation faults, version changes, architectural glitches etc. Think of the various life cycle phases.) Track these errors and log these as these errors occur. Note their categories (types).**

Our major error categories will most likely be Design Flaws, Functional Errors, and Plan Changes.  We will keep track of all major problems.

Errors may include placing a student in a chair multiple times in the same room, not placing a student in a chair that was assigned to the room, not removing a student from a chair if they are removed from a room, and assigning a student to a chair that is not suited to their needs.

**c. Address risk analysis for Use, Misuse and Abuse scenarios. Document the types of design/coding defects that could be great risks. Note in a table form the major types of risk handling you could implement.**

One might consider the student's information to be a privacy problem for misuse, but other than that, this project should not have any serious risks.  Since the task that this software must complete is relatively simple and only arranges students around a grid in a room, it is hard to find ways to misuse this software.

With regard to file saving and opening, it would be possible for a user to manually manipulate the save file which could result in an invalid state which could cause unexpected behavior in the application.  For example, the user could remove a chair or manually place a student in a chair without checking for consistency. This is not a major concern as we expect our users to not manually modify the file.

**d. Address future changes that stakeholders would seek For each stakeholder, list the possible use, misuse and abuses. What design measures do you have to address these risks?**

Plan changes are part of every large software project, but ideas that would detract the software from its original purpose may not be considered, for example, a request to keep track of student's grades or homework assignments are not part of the main usage, so they would probably be rejected.  Any design change would need to be debated and discussed by all the team members before determining the correct method of implementing the new change.


# Task 3: #
**Generate an Operational Profile of your design, for each of your major Use Cases. Recall that an Operational Profile prioritizes various portions of your design (by modules, classes, subsystems as appropriate). These priorities may be based on criticality, frequency of usage, and the effort needed to have a valid set.**

The operational profile for our project attempts to prioritize testing efforts by ranking each use case based on how often it will be used by the user. As a whole, SEAT is an application that expects its users to follow a loose set of tasks in order to generate a class seating chart, so the odds of any one task occurring more than the others is somewhat low, except when the user decides to edit individual seats/students or other options, so these tasks have been assigned higher priority in terms of testing because of their increased likelihood of being used at any given time.

The order and % associated with each task have been estimated based on their chance of happening in generating a given seating chart. These values will become more accurate as testing reveals more authenticate use case patterns.

| # | Task | % |
|:--|:-----|:--|
| 1 | Seating Placement Algorithms | .35 |
| 2 | Manually Selecting Student Seat Placement | .2 |
| 3 | Customize Room Dimensions/Seat Locations | .15 |
| 4 | Add/Update Student Information | .15 |
| 5 | Import Student Information (Roster) | .1 |
| 6 | Print Chart/Information | .05 |

It is also worth mentioning that the GUI itself is a major part of the project, but because of its nature it will be tested throughout the project.

# Task 4: #
**For each stakeholder, layout the GUI as appropriate. Discuss the layout  complexity/simplicity. Allocate review responsibilities as appropriate.**

[Documentation of GUI](http://student-educational-arrangement-tool.googlecode.com/files/cecs550_hw2.pdf)

**Calculate Design metrics such as the McCabe’s Complexity, other Object Oriented Metrics, Cohesion metrics etc. based on your design diagrams. Note partial process metrics that will help the project development. Later, you will plot these at the end of the project.**

McCabe's Cyclone Complexity
| N=5 |
|:----|
| E=6 |
| P=1 |
| M=E-N+2P |
| M=6-5+2\*1 |
| M=3 |

This says that the McCabe's Complexity is a 3. Because this is below 11 there is a lot better chance to not be fault-prone.

Cohesion Metrics(this is an Object Oriented Metric) states that each class has one function. If this isn't true then it should probably become two classes.
| SeatManager | holds the rooms and student |
|:------------|:----------------------------|
| Student     | has all information on a single student |
| Room        | has all information on a room including chairs |
| Chair       | has all the chair properties |

Each of our classes have one function so they are cohesive classes.

Partial Process Metrics
  1. Make the program easy to use
  1. Be able to import class rosters
  1. Be able to both save and load old files
  1. Be able to print out both a roster for a room and the room layout with students in seats