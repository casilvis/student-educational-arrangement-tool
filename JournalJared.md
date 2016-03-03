# Journal #

12/5/09 - Added a dirty flag that is set with events for properties changing.  The title bar of the program is changed accordingly to indicate an unsaved file.  The ObservableCollections are now tied to methods that intercept the changes.  When a student is removed from a RoomRoster he will also be removed from any seat.  A student can now only be manually placed in a single seat and double clicking on a seat will remove a student from it.  The student's name in their seat is now updated via the property notification event. (3 hours)

12/4/09 - Brought the SEAT project up to StyleCop compliance excluding the requirement for documentation headers.  Refactored code that it it will be easier to maintain. (2 hours)

11/30/09 - Added an about box to the main application; Configured the installer; implemented the ctrl+o/s/n shortcut keys; began implementation of the csv parser to import student rosters (3 hours)

11/29/09 - Investigated design options for placement algorithms (1 hour)

10/27/09 - Corrected the style of the SEATLibrary using StyleCop so that the code will be more maintainable. (1.5 hours)

10/19/09 - Implemented INotifyPropertyChanged event for objects that are used in the GUI.  Added necessary XAML to access these properties in the interface.  The GUI now properly reflects the underlying model. (1 hour)

10/7/09 - Changed all collections to ObservableCollections so that the interface will properly be updated as the underlying data is modified.  Some slight improvements to the formatting of the interface. (1 hour)

10/5/09 - Implemented ability to save an open a room template.  Added necessary hooks for the GUI application to call the save and open commands in the data model. (1 hour)

9/30/09 - Minor changes to layout of application (30 minutes)

9/29/09 - Added classes that will implement placement algorithm using a visitor design pattern. (30 minutes)

9/28/09 - Experimented with saving data to XML.  Worked on Homework 2. (2 hours)

9/27/09 - Minor enhancements to the TestProgram (2 hour)

9/23/09 - Finished implementation of rudimentary SEATLibrary.  Began implementation of TestProgram which is a command line program that uses the SEATLibrary. (2 hours)

9/23/09 - Began simple design/implementation of several objects (chair, student, room) (1 hour)

9/11/09 - Designed first draft of the database schema. (1 hour)


# Total Effort So Far #
Hours: 22.5

# Effort Required #
Hours: 52