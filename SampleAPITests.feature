Feature: SampleTestsAPI

Scenario: Verify GET Users request
	Given I am a user
	When I get the list of users
	Then there are '10' users returned

Scenario: Verify GET User request by Id
	Given I am a user
	When I get user with id '8'
	Then user 'Nicholas Runolfsdottir V' is returned

Scenario: Verify POST Users request
	Given I am a user
	When I post a user
	Then user is created
