#Weather Station

The weather station app is built as part of a coding test. The app is a simple and traditional web app on ASP.NET MVC that pulls weather data from OpenWeatherMap.org. 

##Assumptions

The design or the app and UI are based on several assumptions
 * The number of cities shown will remain small
 * The cities to be shown are not user controllable (they are however configuration driven)
 * Metric units are preferred


##MVC rather than single page app

A viable appraoch would have been to use the jsonp services exposed by OpenWeatherMap.org and build the app entirely as a single page app consisting only of html and javascript. This would normally have been my approach however it bypasses the requirement to demonstrate object orientated design practices so have opted to build the app in a more traditional approach to give a more substantial set of code to be evaluated and better demonstrate backend design practices. 

##Infrastructure

Some infrastructure classes were used from previous personal projects, including depenedency injection through StructureMap and the use of IStartupTasks for performing initial configuration and setup.

##Async

The OpenWeatherMap proxy classes make all requests using async methods, freeing up web worker threads to process additional requests.

##Caching

At the moment caching is done entirely with output caches with a timeout of 5 minutes. It is assumed that weather will not change significantly within the 5 minute period. Further improvements could include adding caching at the service level, this would prevent column sorting from being cached seperatly. 

##Tests

Unit tests exist for the controller actions, these cover data mapping, sorting and error handling. 

Tests also exist for the OpenWeatherMap proxy classes to test that the request of data from the service is performed correctly, although these border on integration tests they are important for covering API calling and data serialization.


##Responsive User Interface

The site is designed to be responsive and works equally well in both a desktop, tablet or mobile enviornments.
