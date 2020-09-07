#Service Tracker Project 
##Summary
Service tracker solution contains the source code for web application used for Schlumberger SIS-SCA Financial Forecasts and accountability control 
to keep track of all the services provided to different clients, employees involved and administrators.

Its target is to provide an easy way to manage and control the information in a collaborative environment to collectively feed and gather information stored in the database system.

The solution consists of three different projects:

### 1.- ServiceTracker_Webapp
	Web Application that serves as the data interface between The database and web Browser.
	#### Specifications:
		The application uses React.js Framework as a Front-end with different plugins to allow user interaction
		As data interface / Web server uses .NET core 3.0
#### 2. - ServiceTracker_DAL
	Is the DATA Access Layer of Service Tracker
	It contains the representation of the database model
	Uses .NET core Entity Framework 3.0.
	The benefit of this technology is that the model can be built in the code and later on export new classes and properties created into the database to keep track and control of the database model.
	It is closely related to ServiceTracker_WebApp
#### 3. - ServiceTracker_DB
	Is a pivot project, will contain SQL Scripts / Views or functions that allow in the future the database to be easily manipulated nad keep track of the SQL changes performed to the database.
	