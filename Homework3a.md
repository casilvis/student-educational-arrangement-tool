# Homework 3a #

HOMEWORK 3A DUE not later than Oct 28, 2009

## Question 1 ##
**1. Define a Fishbone Diagram (Ishikawa diagram, Cause-Effect diagram) appropriate to your project. Mark on this diagram priorities for different use cases.
Create operational profiles for software components or modules or subsystems. Also identify where normal use, misuse and abuse cases could arise and hence enable
test case planning.**

<img src='http://img.photobucket.com/albums/v252/Ziptnf/fishbone.jpg'></img>



&lt;hr /&gt;


## Question 2 ##
**2. Write out your test plans in terms of documented test suites, appropriate test data sets. Identify these plans for unit, integration and system tests. Mark the Most Important
Tests.**

The test plan for SEAT consists of unit testing for each component.  The software architecture implements MVC which separates the underlying logic from the user interface.  In parallel to a graphical user interface, a command line application is being developed which is designed to do the unit testing.  The command line interface does not expose all of the functionality of the program, but is able to perform all of the main functions.

The integration testing is primarily the communication between the underlying library and the graphical user interface.  Much of the logic of the program is implemented in the GUI to produce the proper interfaces for the user and execute the correct actions.

The most important task will be performing all of the various actions that can be taken from the application.  These include
  * Adding a student
  * Updating a student
  * Importing a student roster
  * Adding a room
  * Updating room
  * Saving a room as a template
  * Creating a room from a template
  * Editing a chair in a room
  * Adding a student to a room roster
  * Adding a student to a specific chair in a room



&lt;hr /&gt;


## Question 3 ##
**3. Create or revisit and revise (if created in Homework 2), a list of major error categories for your project. (Recall these are not just the compiler errors. These are design errors, process mismatches, plan changes and mismatches, documentation faults, version changes, architectural glitches etc. Think of the various life cycle phases.) Track these errors and log these as these errors occur. Note their categories (types). Also, note when these errors were removed. Note if the integrity of the specific test plan was adhered to or violated. Were major code changes made as a result of testing and if so, did these change the nature of your test plans? Specifically. for each defect:**<br>
<b>a. - A description of each defect(if not already given)</b><br>
<b>b. - The location of the defect in terms of the class or method in which the defect occurred(if not already given)</b><br>
<b>c. - An estimate of the time it took to fix the defect. Try to estimate within 5 or 10 minutes if you can. If not, mark the time estimate for the defect ‘unknown’.</b><br>

<b>a)</b> room rows and columns switched<br>
<b>b)</b> room<br>
<b>c)</b> 20-30 min<br>
<br>
<b>a)</b> binding from one class to another<br>
<b>b)</b> seatmanager and window1<br>
<b>c)</b> 1-3hrs<br>
<br>
<b>a)</b> null pointers to students<br>
<b>b)</b> seatmanager students<br>
<b>c)</b> 20 min<br>
<br>
<b>a)</b> having seat and chair interact<br>
<b>b)</b> seat and chair<br>
<b>c)</b> 20min<br>
<br>
<b>a)</b> listboxes noticing change<br>
<b>b)</b> seatmanager and window1<br>
<b>c)</b> 1-3hrs<br>
<br>
<b>a)</b> corrected style of the code using StyleCop<br>
<b>b)</b> SEATLibrary1<br>
<b>c)</b> 1 hour<br>
<br>
<br>
<br>
<br>
<hr /><br>
<br>
<br>
<h2>Question 4</h2>
<b>4. Calculate the McCabe complexity. From the Program graphs (object diagrams, dynamic models, functional models, CRC graphs etc. ), generate the minimal number of test cases<br>
for each use case. Identify these test cases as functional or structural, black box, white box etc. For white box testing identify the paths through the modules. Relate the number<br>
of these test cases to the design complexity. Document your testing plans and estimate the testing effort. Based on your Most Important Tests, declare a target coverage for the<br>
testing effort. Use McCabe Software or equivalent form open sources.</b>
<br>
The total McCabe/Cyclomatic Complexity for our GUI part of the project is 223, for the library created is 154, and for the command line version is 74.refer to <a href='http://student-educational-arrangement-tool.googlecode.com/files/SoftwareMetrics.xls'>Full Printout From Visual Studio</a> or look at this image to better understand.<br>
<a href='http://student-educational-arrangement-tool.googlecode.com/files/SoftwareMetrics.JPG'>http://student-educational-arrangement-tool.googlecode.com/files/SoftwareMetrics.JPG</a>
<br>
The ones with # are what would be White-Box testing<br>
The ones with \ are what would be Black-Box testing<br>
The ones with % are what would be Functional testing<br>
The ones with ^ are what would be Structural testing<br>

Import student Roster:<br>
<ul><li>^%User imports Roster<br>
</li><li>#User gives a bad file path<br>
</li><li>\User gives a bad file format/corrupt file</li></ul>

Update student information:<br>
<ul><li>#User updates info and clicks submit<br>
</li><li>%\User updates and exits<br>
</li><li>^User doesn't update</li></ul>

Add room:<br>
<ul><li>^User exits before room is created<br>
</li><li>%User creates room<br>
</li><li>%User loads room<br>
</li><li>#User loads with bad file path<br>
</li><li>\User loads with bad file format/currupt file</li></ul>

Customize room:<br>
<ul><li>^User clicks done<br>
</li><li>^User exits<br>
</li><li>%User edits then exits<br>
</li><li>%User edits then clicks done<br>
</li><li>%\User edits then saves then exits<br>
</li><li>#User edits then saves then clicks done</li></ul>

Add students to room:<br>
<ul><li>%User adds new student to room<br>
</li><li>\User adds same student to room<br>
</li><li>#User adds more students than seats to room</li></ul>

Manually place students in seats:<br>
<ul><li>%User selects new seat and new student<br>
</li><li>^User selects used seat and new student<br>
</li><li>^User selects new seat and used student<br>
</li><li>#\User selects used seat and used student</li></ul>

Fill in seats using algorithm:<br>
<ul><li>%Clicks button<br>
</li><li>%#\Chooses type of fill then clicks button</li></ul>

Print a report:<br>
<blockquote>%#\Clicks button</blockquote>

The white-box testing paths:<br>
<ul><li>#User gives a bad file path			not implemented yet<br>
</li><li>#User updates info and clicks submit		StudentAdd,SeatManager,Window1<br>
</li><li>#User loads with bad file path			btnBrowse_Click,error<br>
</li><li>#User edits then saves then clicks done		frmGrid,Seatmanager,Room,Window1<br>
</li><li>#User adds more students than seats to room	not implemented yet<br>
</li><li>#\User selects used seat and used student	not implemented yet<br>
</li><li>%#\Chooses type of fill then clicks button	not implemented yet<br>
</li><li>%#\Clicks button				not implemented yet<br>
<br>
The black-box tests and the white-box tests should cover most of our code. Because of this we should be able to get a significant amount of our errors out before we are done. This should cover about 80-90% of our code.</li></ul>

<br>
<br>
<br>
<hr /><br>
<br>
<br>
<h2>Question 5</h2>
<b>5. Calculate the total effort spent by October 28, 2009 from your log books. Use appropriate Object Oriented Software Metrics. Relate your effort to the time spent on the<br>
project and the testing effort. Plot these by month and by individuals in the team.</b>

<h3>Total Effort by Month for Entire Group:</h3>

<ul><li>September=12hr (Justin) + 9hr (Jared) = 21hr (total)<br>
</li><li>October=15.5hr (Justin) + 4.5 (Jared) = 20hr (total)</li></ul>

<img src='http://chart.apis.google.com/chart?chtt=Effort+by+Month&chts=000000,12&chs=300x150&chf=bg,s,ffffff&cht=p&chd=t:51.21,48.78&chl=September|October&chco=0000ff,ff0000&nonsense=something.png' />

<h3>Total Effort by Person:</h3>
<ul><li>Justin= 27.5hr<br>
</li><li>Jared = 13.5hr<br>
</li><li>Brian =<br>
</li><li>Nick  = 4 hrs</li></ul>

<img src='http://chart.apis.google.com/chart?chtt=Time+Spent+Since+October+28&chts=000000,12&chs=300x200&chf=bg,s,ffffff|c,s,ffffff&chxt=x,y&chxl=0:|Justin|Brian|Jared|Nick|1:|0.00|13.75|27.50&cht=bvs&chd=t:100.00,0.00,49.09,15.00&chco=ff0000&chbh=50&nonsense=something.png' />

<h3>Object Oriented Software Metrics</h3>
<img src='http://chart.apis.google.com/chart?chtt=SEAT:+Lines+of+Code&chts=000000,12&chs=400x150&chf=bg,s,ffffff|c,s,ffffff&chxt=x,y&chxl=0:|App|ClassOpen|frmGrid|Seat|StudentAdd|Window1|1:|0.00|162.50|325.00&cht=bvs&chd=t:1.23,15.69,100.00,8.92,16.30,22.76&chco=0000ff&chbh=50&nonsense=something.png' />

<img src='http://chart.apis.google.com/chart?chtt=SEATLibrary:+Lines+of+Code&chts=000000,12&chs=400x150&chf=bg,s,ffffff|c,s,ffffff&chxt=x,y&chxl=0:|Chair|Room|SeatManager|Student|1:|0.00|59.00|118.00&cht=bvs&chd=t:35.59,100.00,96.61,60.16&chco=ff0000&chbh=50&nonsense=something.png' />

<img src='http://chart.apis.google.com/chart?chtt=TestingApplication:+Lines+of+Code&chts=000000,12&chs=400x150&chf=bg,s,ffffff|c,s,ffffff&chxt=x,y&chxl=0:|CLInterface|Program|1:|0.00|65.00|130.00&cht=bvs&chd=t:100.00,47.69&chco=00ff00&chbh=50&nonsense=something.png' />