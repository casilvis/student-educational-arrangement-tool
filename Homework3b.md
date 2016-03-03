# Homework 3b #

HOMEWORK 3B DUE Nov 30, 2009

**6. Plot these tracking metrics using EXCEL or other spread sheet programs as appropriate. Plot errors discovered by date. This will give you a graph to compute the error rate. Also, plot the rate of removal of errors. Calculate the mean and variance of the delays in defect removal. Explain extreme cases as part of the data-mining task.**

Sep 30th Incorrect scope for classes prevented project from compilign-critical<br>
Oct 05th error infinate loop in code fixed that day-critical<br>
Oct 07th fixed design flaw where collections were incorrectly used in place of ObservableCollections-serious<br>
Oct 16th rows and columns were switched-fixed the day it was found-serious<br>
Oct 19th GUI ListBoxes now properly updated (INotifyPropertyChanged)-serious<br>
Oct 26th StyleCop warnings fixed that day-'silly'<br>
Oct 27th syntax error fixed that day-'silly'<br>
Oct 28th error fixed that day-serious<br>
mean time to failure = MTTF=24x(30+31+30)/Ec(t)=364hours=15days 4hours<br>
Ec(t) = Ed(t), Er(t) = ET - Ec(t)<br>
Ec(t)=errors corrected<br>
Ed(t) errors detected at time t in project<br>
Er(t)=errors left<br>
Mean and Variance are: (1+1+1+1+1+1+1+1)/8=1 day and (1x1+1x1+1x1+1x1+1x1+1x1+1x1+1x1)/8=1 day<br>


<a href='http://student-educational-arrangement-tool.googlecode.com/files/Errors.xlsx'>http://student-educational-arrangement-tool.googlecode.com/files/Errors.xlsx</a>

<b>7. From your testing data, mark on the Fishbone diagram, the actual statistics you followed to determine faults/failures. For the Function Points of your projects, determine the corresponding errors. From your log books determine the effort spent in development for each function point. Did you predict this testing effort in February? Did you use any (formal) model to predicted effort? Which one and how? The Fishbone diagram helps in the datamining of the Software faults data. What other software engineering knowledge discoveries can you note?</b>

<img src='http://img.photobucket.com/albums/v252/Ziptnf/fishbone2.jpg'></img>

In each fault, we predicted the effort that would be used for each bug to be about 1 hour a piece.  We used Use Case Analysis to predict our effort and concluded that fixing each bug should not be that difficult.<br>
<br>
<br>
<b>8. Design and note all test-stub drivers for test instrumentation. Write the extent to which you have test coverage. What types of coverage do you have for the object oriented part of your design? Give an overall measure based on these measurements for the quality of testing. Discuss which tool you used, such as JUNIT, or NUNIT or some other Testing tool.</b>

Test coverage has been growing side by side with the implemented features list. Our final two milestones, the seating algorithms and the printable report, have yet to be completed, and have yet to be tested. Unfortunately these are both very significant parts of our project, the both of them together covering a large portion of our features list. They are too large to write stub code for, but the rest of the program is fully testable without them. We would peg the quality of the testing to be at about 80% of the current features, which will jump up when all of the planned features are implemented. There is a analytics tool in Visual Studio we use to get a report on our code.<br>
<br>
<br>
<b>9. Calculate the total effort spent by Nov 30, 2009 from your log books. Use appropriate Object Oriented Software Metrics. Relate your effort to the time spent on the project and the testing effort. Plot these by month and by individuals in the team. Add up the size of all test-stubs and drivers. Express it as a percentage of the total delivered code. What was the size of the maximum code When did it occur?</b>

<h3>Total Effort by Month for Entire Group:</h3>

<ul><li>September = 12hr (Justin) + 9hr (Jared) = 21hr (total)<br>
</li><li>October = 15.5hr (Justin) + 4.5 (Jared) + 4hr (Nick) + 3hr (Brian) = 27hr (total)<br>
</li><li>November = 1hr (Jared) + 2hr (Nick) + 2hr (Justin) =5hr (total)</li></ul>

<img src='http://chart.apis.google.com/chart?chtt=Effort+by+Month&chts=000000,12&chs=300x150&chf=bg,s,ffffff&cht=p&chd=t:21,27,5&chl=September|October|November&chco=0000ff,ff0000&nonsense=something.png' />


<h3>Total Effort by Person:</h3>

<ul><li>Justin= 29.5hr<br>
</li><li>Jared = 14.5hr<br>
</li><li>Brian = 3hr<br>
</li><li>Nick  = 6hr</li></ul>

<img src='http://chart.apis.google.com/chart?chtt=Time+Spent+Since+November+30&chts=000000,12&chs=300x200&chf=bg,s,ffffff|c,s,ffffff&chxt=x,y&chxl=0:|Justin|Jared|Brian|Nick|&cht=bvs&chd=t:29.5,14.5,3.0,6.0&chco=ff0000&chbh=50&nonsense=something.png' />



<b>10. Discuss effort estimation at different milestones in the project. Analyze and discover the relations between the metrics of different phases of the software. Identify the configuration items needed in your project.. Tabulate the number of revisions, versions or changes you made. Check your logs and document how the timeline of changes actually occurred. Explain how the target test coverage was met adequately or not. What were the software engineering reasons?</b>

The outline for the project included four major milestones.  The first milestone consisted of the design of the classes and a database schema.  The second major milestone was the implementation of the GUI and the classes outlined in the first milestone.  Both of these milestones have been completed by late October and early November.  A simplification to the design that stored all of the data by reading and writing an XML file as a single entity dramatically reduced the amount of time expected to be spent on saving information stored in the application.<br>
<br>
The third and forth milestones have not been completed yet and are currently in progress.  The third milestone is an implementation of a placement algorithm that assigns students to seats based on the information about the room and each particular student.  While the solution to this problem is subjective, there is a range of solutions which may be better than others.  The fourth and final milestone has not been started yet and it is the generation of a printable report.  This task requires all previous tasks be completed before it can be started.  However, with all of the previous tasks completed the effort required to complete this task is reduced since it builds on the existing code base.<br>
Our metrics worked very well as the first two milestones progressed.  However, with the final two milestones, the project has not followed the original schedule due to members not being able to complete the requirements as quickly.<br>
<br>
<img src='http://lh3.ggpht.com/_l4Rv5VGKzLQ/SxG21GFlolI/AAAAAAAAAXE/C-c448zwbso/550-Commits.png' />

Not all aspects of the program have been able to be tested because some of the components are still being created.  The components that have been tested include the ability to write to and read from a file.  A bug in this algorithm was identified and removed.  Additionally, the model that represents a room is one of the most complex components in the application, specifically how it interfaces with the GUI and a variety of bugs and usability enhancements have occurred throughout development.<br>
<br>
The testing still needs to cover the placement of the students.  With this aspect of the code still in development it has not been tested yet.  The placement of students inside of a classroom is potentially the most error prone aspect of the problem and is deserving of the most extensive testing.  This component is specifically the business logic that is encompassed by the application and the result produced by this algorithm cannot provide a definitively best solution since in most cases the solution is subjective and many solutions exist.  However, it is possible to avoid a bad seating arrangement and the algorithm at a minimum will avoid these configurations.<br>
<br>
<br>
<b>11. Identify critical stages in the process and discuss the process management issues in your project. Do you have unfinished work that can be easily taken over by another team? Is your documentation supportive of this task? What maintenance, security and upgrading measures are in the finished product? What is the estimate of potential maintenance work as you conclude your project and hand it over to the next team or phase? How would you characterize the reliability of your product?</b>

Critical stages:<br>
When we got the database end working for students.<br>
When we got the command line version of the room generator working. <br>
When we got the GUI part of the room generator working.  <br>
^When the students can be added to classrooms.  <br>
^When the students are actually placed in seats.  <br>
^When we can print out room rosters and desk places. <br>
<blockquote><br>
^=not yet reached  <br>
<br>
We haven't been able to get students into rooms yet which hinders us from being able to print out stuff. This could be taken over but we are hoping to atleast complete the students in seats and students in classrooms thing before we present. The documentation is helpful, but will need some work to allow for the concepts to be passed along. We do have the requirements written out pretty nicely though so unless these change, it shouldn't be hard to do. The maintenance once it is done should be more likely just upgrading. Security doesn't exist so if people want the class information to be safer, there could be some encryption put into the save and load files. Upgrading will simply just be adding extra features or trying to make the program run faster than it does now. Potential maintenance work as stated is more  likely upgrading as the system calls for it. Given this, it means that it should be close to nothing. The reliability of the program in command mode is very bad. This was created mostly for testing, so there aren't try catch statements which means it is very likely to crash. Since we are actually working on the GUI this is ok. The GUI is much better and more stable from what we can see. It seems to do what we expected from the system, so unless we have missed parts, the code written executes well. If you save when things are created, then there should be no problem restoring the files. So the reliability of the GUI is good, but the command line is bad.