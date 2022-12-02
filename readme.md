Software Developer (C#)

For this assignment, there are certain objectives that we would like to achieve. However, as there is no time limit for how long you should spend on it, feel free to add what you see fit and treat it like a production system. You should make a public repository on github for this assignment and provide us with the link.
For this assignment, the focus is on setting up an API as a whole. You are asked to implement some endpoints to serve data around students and their classes.

Core Data 
We will have 2 groups of users: students and lecturers. For both groups we will have their credentials (to be used for authentication) as well as their first name, last name, personal phone, and personal email. In case of lecturers we will also store their social insurance number.
Aside from users, we will also have a list of courses that we teach that are defined with a code and a title (e.g. COMP-123 Introduction to Programming)
We also have a list of academic periods that run across the year. Each academic period is defined with a name, start date, and an end date.

Then for the classes the students take in each academic period, we will have sections, for instance:
	- We will have Spring 2023 and Summer 2023 running through certain period
	- Spring will have 3 different sections for COMP-123 and Summer will have 2 sections of COMP-123 and a

section of ENGL-324
	- Each section has at least 1 lecturer, but it can have more.
	- Note: Each section is essentially a course in an academic period with a section number.

Requirements
The following is a list of requirements you are asked to implement:
	- We need to authenticate the user and know what they will have access to. Implement it in the most optimal way, assuming that we store user credentials in our system(instead of using 3rd party services like Google, Microsoft, etc.)
	- An endpoint that shows a list of students:
		- For Lecturers: They will have full access to student’s information, granted that the student has taken a class with them
		- For students: They will see list of students’ first name and last names, granted they’ve had a class together.
	- An endpoint to show list of classes with the option to only display a certain academic period and/or taken classes. For each section, corresponding lecturer and academic period’s details should be provided. This will be used by students and lecturers.
	- An endpoint to show list of all classes (available to all, but for lecturers, it will include number of students)
	- An endpoint for a student to request approval from a lecturer to send a message to the rest of the class. Naturally, we will need an endpoint for lecturers to see list of requests (approved/not) with the option to filter through them. On student’s side, there is no need to have an endpoint to show 1) approved requests or 2) approved announcements.
	
General Remark: For the endpoints you create, feel free to add appropriate options such as sorting, pagination, etc.