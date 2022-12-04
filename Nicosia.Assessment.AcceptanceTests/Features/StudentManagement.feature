Feature: Student Management

    Scenario Outline: Create Read Edit Delete Student
        When user creates a student with the following details
        | ID | FirstName | LastName | Email        | PhoneNumber   | DateOfBirth | BankAccountNumber    |
        | 1  | John      | Doe      | john@doe.com | +989121234567 | 01-JAN-2000 | AT483200000012345864 |
		Then user can lookup all students and filter by Email of "john@doe.com" and get "1" records
		When user creates a student with the following details
		| ID | FirstName | LastName | Email        | PhoneNumber   | DateOfBirth | BankAccountNumber    |
        | 1  | John      | Doe      | Different@email.com | +989121234567 | 01-JAN-2000 | AT483200000012345864 |
		Then system must respond with error code of "400"
        And system response contains "Duplicate student by First-name, Last-name, Date-of-Birth"
        When user creates a student with the following details
		| ID | FirstName | LastName | Email        | PhoneNumber   | DateOfBirth | BankAccountNumber    |
        | 1  | Jane      | Lee      | john@doe.com | +989121234567 | 01-JAN-2010 | AT483200000012345864 |
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
          | ID | FirstName | LastName | Email        | PhoneNumber   | DateOfBirth | BankAccountNumber    |
          | 1  | JOHN      | DOE      | john@doe.com | +989121234567 | 01-JAN-2000 | AT483200000012345864 |
        When user creates a student with the following details
          | ID | FirstName | LastName | Email        | PhoneNumber   | DateOfBirth | BankAccountNumber    |
          | 1  | John      | Doe      | joh2@doe.com | +989121234577 | 01-JAN-2000 | AT483200000012345864 |
        Then system must respond with error code of "400"
        And system response contains "Duplicate student by First-name, Last-name, Date-of-Birth"
     

    Scenario Outline: Mobile Validation Criteria in the system
        When user creates a student with the following details
        | ID | FirstName | LastName | Email        | PhoneNumber   | DateOfBirth | BankAccountNumber    |
        | 1  | John      | Doe      | john@doe.com | +982188228866 | 01-JAN-2000 | AT483200000012345864 |
        | 1  | jane      | Doe      | jane@doe.com | +316123456789 | 01-JAN-2000 | AT483200000012345864 |
		Then system must respond with error code of "400" 
        And system response contains "Invalid Mobile Number"
        
    Scenario Outline: Bank Account Number Validation Criteria in the system
        When user creates a student with the following details
        | ID | FirstName | LastName | Email        | PhoneNumber    | DateOfBirth | BankAccountNumber |
        | 1  | John      | Doe      | john@doe.com | +981212345678  | 01-JAN-2000 | 12000000000000001 |
		Then system must respond with error code of "400" 
        And system response contains "Invalid Bank Account Number"
        
    Scenario Outline: Email Validation Criteria in the system
        When user creates a student with the following details
        | ID | FirstName | LastName | Email        | PhoneNumber    | DateOfBirth | BankAccountNumber    |
        | 1  | John      | Doe      | johndoe.com  | +981212345678  | 01-JAN-2000 | AT483200000012345864 |
		Then system must respond with error code of "400" 
        And system response contains "Invalid Email address"

