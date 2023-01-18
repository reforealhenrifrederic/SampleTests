Feature: SampleTestsUI

Scenario: Verify you can navigate to Payees page using the navigation menu
	Given I am in the Demo page
	When I navigate to the Payees page
	Then Verify Payees is loaded

Scenario: Verify you can add new payee in the Payees page
	Given I am in the Demo page
	When I navigate to the Payees page
	And I add a payee
	And I input payee 'John Doe' with account number '12-3456-7890123-001'
	And I submit
	Then the new payee is successfully added

Scenario: Verify payee name is a required field
	Given I am in the Demo page
	When I navigate to the Payees page
	And I add a payee
	And I submit
	Then errors are shown indicating that Payee Name is required
	When I input payee 'John Doe' with account number '12-3456-7890123-001'
	Then the errors indicating that Payee Name is required is not visible anymore

Scenario: Verify that payees can be sorted by name 
	Given I am in the Demo page
	When I navigate to the Payees page
	Then payeees are assorted in ascending order
	When I click the Name header
	Then payeees are assorted in descending order

Scenario Outline: Navigate to Payments page
	Given I am in the Demo page
	And I get the original amount of 'Everyday' and 'Bills'
	When I navigate to the Payments page 
	And I transfer '<Amount>' from 'Everyday' to 'Bills'
	Then the amount is successfully transferred

	Examples: 
	| Amount |
	| 500    |
	| 500    |
	| 500    |




	