# 3.1 External interface requirements #
## 3.1.1 User interfaces ##
  1. Create classroom template
  1. Import class roster
  1. Modify class roster
  1. Add students into a classroom
  1. Generate seating chart

## 3.1.2 Hardware interfaces ##
  * None.

## 3.1.3 Software interfaces ##
  1. Access database used to store all of the classroom information for a specific class.
  1. Classroom templates used to store premade classroom configuration.

## 3.1.4 Communications interfaces ##
  * None.

# 3.2 System features #

## 3.2.1 System Feature 1 ##
The ability to represent a model of the classroom
### 3.2.1.1 Introduction / Purpose of Feature ###
The classroom model needs to be able to place individual students in seats located inside of real classrooms.  The model needs to approximate seating locations in the classroom.
### 3.2.1.2 Stimulus / Response sequence ###
A basic knowledge of classroom layout is needed.
### 3.2.1.3 Associated functional requirements ###
#### 3.2.1.3.1 Functional requirement 1 ####
  * Seat can be specified as left handed
  * Priority: Critical
#### 3.2.1.3.2 Functional requirement 2 ####
  * Seat can be flagged as being in the front, middle, or back of the room
  * Priority: Critical
#### 3.2.1.3.3 Functional requirement 3 ####
  * Seat can be flagged as being in the left, middle, or rights side of the room
  * Priority: Medium
#### 3.2.1.3.4 Functional requirement 4 ####
  * Seat can be flagged as not being allowed to be used
  * Priority: High
#### 3.2.1.3.5 Functional requirement 5 ####
  * Seat can be flagged as not being an actual seat (aisle ways and missing seats)
  * Priority: Low
#### 3.2.1.3.6 Functional requirement 6 ####
  * The actual seat number specified in the classroom
  * Priority: High


## 3.2.2 System Feature 2 ##
A class roster representing students enrolled in the class
### 3.2.2.1 Introduction / Purpose of Feature ###
Represent students who will be placed in a classroom as specified by enrollment.
### 3.2.2.2 Stimulus / Response sequence ###
  * Class roster with students enrolled in the class.
### 3.2.2.3 Associated functional requirements ###
#### 3.2.2.3.1 Functional requirement 1 ####
  * Data stored needs to be student last name, student first name, student id, handedness flag, vision impairment flag, and section number
  * Priority: Critical
#### 3.2.2.3.2 Functional requirement 2 ####
  * Student list needs to be sortable by student last name then first name
  * Priority: Medium
#### 3.2.2.3.3 Functional requirement 3 ####
  * Student list needs to be group by section then stored by last name then first name
  * Priority: Critical
#### 3.2.2.3.4 Functional requirement 4 ####
  * Student roster can be imported from a CSV file.  This includes the students first name, last name, student ID, and section number.
  * Priority: Medium
#### 3.2.2.3.5 Functional requirement 5 ####
  * Student handedness and vision is settable through the user interface
  * Priority: Critical
#### 3.2.2.3.6 Functional requirement 6 ####
  * Students can be added, edited, and removed from the class roster through the user interface
  * Multiple students can be removed from the class roster at the same time.
  * Priority: Critical

## 3.2.3 System Feature 3 ##
Assigning students in roster to seats in specified classroom
### 3.2.3.1 Introduction / Purpose of Feature ###
Make a best attempt at fulfilling all student and classroom requirements while assigning a specific seat to everyone on the roster.
### 3.2.3.2 Stimulus / Response sequence ###
  * A complete room roster
  * A valid classroom model
  * The number of available seats must be greater than or equal to the number of students.
### 3.2.3.3 Associated functional requirements ###
#### 3.2.3.3.1 Functional requirement 1 ####
Algorithm must attempt to meet student requirements
  * Left handed students should be placed in left handed desks
  * Students with vision impairment should be placed towards the front of the classroom
  * Priority: Critical
#### 3.2.3.3.2 Functional requirement 2 ####
Students should be able to be placed in multiple rooms for different days or meetings times for the same class.
  * Priority: High
#### 3.2.3.3.3 Functional requirement 3 ####
Multiple filling algirothms are employed in order to make a best attempt at filling the classroom
  1. Front to back
  1. Every other seat
  1. Prioritized students first (left handed and vision impairment first)
  1. Manual placement of students
  * Priority: High
#### 3.2.3.3.4 Functional requirement 4 ####
Seating assignments must be saved so they can be recalled later.
  * Priority: Critical

## 3.2.4 System Feature 4 ##
Generation of seating assignment reports
### 3.2.4.1 Introduction / Purpose of Feature ###
Provide a printable version of seating assignments for professor and students.
### 3.2.4.2 Stimulus / Response sequence ###
A seating chart must have been successfully generated placing all students in seats.
### 3.2.4.3 Associated functional requirements ###
#### 3.2.4.3.1 Functional requirement 1 ####
Graphical Representation
  1. Provides a visual approximation of where students will be located in the classroom.
  1. Places all students name in a grid that matches the classroom layout
  * Priority: Low
#### 3.2.4.3.2 Functional requirement 2 ####
Textual Representation
  1. Provides a text based list of seating assignments
  1. Report broken down by section then alphabetized by first then last name
  * Priority: Critical



# 3.3 Performance requirements #
  * Seating assignments must be calculated in under 5 minutes.

# 3.4 Design constraints #
  * Classroom template files must be stand alone for each different classroom configuration.
  * Class file must be a single file which can easily opened and closed.

# 3.5 Software system attributes #
  * Standalone C# windows application

# 3.6 Other requirements #
  * Software must be able to be used by a non-technical user.