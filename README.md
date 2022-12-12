# ChatHistory

Dear User,

To run the project please follow the below steps:

  1. Get the code locally
  2. Run all unit test in Test Explorer
  3. Run the solution by clicking Visual Studio's start button
  4. A Swagger page will appear containing an endpoint
  5. Enter the required parameters:<br />
		- <b>date:</b><br /> 
				- format: <b>'yyyy-MM-dd'</b><br /> 
				- example: <b>2022-12-05</b><br />
				- data is currently available for December 5th and 6th. If necessary, additional data can be added by modifying <b>DbContextGenerator.cs</b>
		- <b>aggregation Level</b> - dropdown: select either of the values<br /> 
				- value 0: display all events separately<br /> 
				- value 1: display all events for the selected date grouped by event type<br /> 
