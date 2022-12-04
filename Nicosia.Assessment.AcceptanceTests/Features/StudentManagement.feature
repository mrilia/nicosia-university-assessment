Feature: Student Management

    Scenario Outline: Create Read Edit Delete Student
        When user creates a student with the following details
        | StudentId                               | FirstName | LastName | Email        | PhoneNumber   | DateOfBirth | Password    |
        | 0FA42003-A30E-42EB-B6B7-F3B318EDB5FA	  | John      | Doe      | john@doe.com | +989121234567 | 2000-01-01 | P@S$W0rD    |
		Then user can lookup all students and filter by Email of "john@doe.com" and get "1" records
		When user creates a student with the following details
		| StudentId                               | FirstName | LastName | Email        | PhoneNumber   | DateOfBirth | Password    |
        | 0FA42003-A30E-42EB-B6B7-F3B318EDB5FA	  | John      | Doe      | Different@email.com | +989121234567 | 2000-01-01 | P@S$W0rD |
		Then system must respond with error code of "400"
        And system response contains "Duplicate student by First-name, Last-name, Date-of-Birth"
        When user creates a student with the following details
		| StudentId                               | FirstName | LastName | Email        | PhoneNumber   | DateOfBirth | Password    |
        | D2C47736-743E-400F-9D7E-C85F40DBF43D	  | Jane      | Lee      | john@doe.com | +989121234567 | 2000-01-01 | P@S$W0rD |
        Then system must respond with error code of "400" 
        And system response contains "Duplicate student by Email address"
		When user edit student who has email of "john@doe.com" with new email of "new@email.com"
        Then user can lookup all students and filter by Email of "john@doe.com" and get "0" records
        And user can lookup all students and filter by Email of "new@email.com" and get "1" records
        When user delete student by Email of "new@email.com"
        Then user can lookup all students and filter by Email of "new@email.com" and get "0" records
        And user can lookup all students and filter by Email of "john@doe.com" and get "0" records

    Scenario: Create a duplicate student by firstname, lastname and date of birth
        Given system has existing student
          | StudentId                                 | FirstName | LastName | Email        | PhoneNumber   | DateOfBirth | Password    |
          | D2C47736-743E-400F-9D7E-C85F40DBF43D	  | JOHN      | DOE      | john@doe.com | +989121234567 | 2000-01-01 | P@S$W0rD |
        When user creates a student with the following details
          | StudentId                                 | FirstName | LastName | Email        | PhoneNumber   | DateOfBirth | Password    |
          | 6998C1B0-A356-451E-B06C-8BFD996AA5F7	  | John      | Doe      | joh2@doe.com | +989121234577 | 2000-01-01 | P@S$W0rD |
        Then system must respond with error code of "400"
        And system response contains "Duplicate student by First-name, Last-name, Date-of-Birth"
     

    Scenario Outline: Mobile Validation Criteria in the system
        When user creates a student with the following details
        | StudentId                               | FirstName | LastName | Email        | PhoneNumber   | DateOfBirth | Password    |
        | 6998C1B0-A356-451E-B06C-8BFD996AA5F7	  | John      | Doe      | john@doe.com | +982188228866 | 2000-01-01 | P@S$W0rD |
        | D2C47736-743E-400F-9D7E-C85F40DBF43D	  | jane      | Doe      | jane@doe.com | +316123456789 | 2000-01-01 | P@S$W0rD |
		Then system must respond with error code of "400" 
        And system response contains "Invalid Mobile Number"
        
    Scenario Outline: Email Validation Criteria in the system
        When user creates a student with the following details
        | StudentId                               | FirstName | LastName | Email        | PhoneNumber    | DateOfBirth | Password    |
        | 6998C1B0-A356-451E-B06C-8BFD996AA5F7	  | John      | Doe      | johndoe.com  | +981212345678  | 2000-01-01 | P@S$W0rD |
		Then system must respond with error code of "400" 
        And system response contains "Invalid Email address"

