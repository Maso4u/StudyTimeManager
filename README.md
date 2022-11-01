# StudyTimeManager

A project meant to assist students manage their time with the modules that they are doing in a semester. This project has WPF application built following MVVM architecture with the help of the Microsoft MVVM community toolkit and makes use of Entity Framework Core for the database connection. Further development will be made in the project to add an ASP.NET Web App.

The project currently provides users with the following functionalities:
- Create an account with a username and password (to be hashed in the database)
- Login to account with the credentials used to create an account
- User can create a semester by giving the start date and number of weeks in the semester.
- User can then add a module to the created semester with the following information:
  - Module code
  - module name
  - Number of credits for the module
  - Class hours per week
- Calculate the required weekly self study hours for the module created.
- The addded module is displayed in a list upon its creation.
  - When a module in the list is clicked, a list of the weeks in the semester for the module,
- User can record a study session for a module for a given day.
  - The required weekly self-study hours are updated for the selected module in the week of the date that the study session is created for
