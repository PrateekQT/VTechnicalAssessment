Feature: Weather check
	To check the weather of Bournemouth


Scenario: To verify that the Maximum temperature of Bournemouth is greater than minimum temperature
	Given User open the BBC weather application
	When User searches the "Bournemouth" city
	Then verifies that the maximum temperature is greater than minimum temperature