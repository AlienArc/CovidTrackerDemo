# Covid Tracker

## About the App

This application was created to show off basic competency with .NET Blazor. The code and architecture are clean and allow easy future maintenance. This application was completed in a few hours and should not be considered production ready. There was only enough time allotted to show some basic techniques and core competencies. 

A running demo of the code can be found hosted on [Azure](https://swcovidtracker-gxgdh4embkbshshy.centralus-01.azurewebsites.net): 

## Notable Features:

### Architecture
The application is broken into two projects separating the UI parts from the backend parts. It could easily be broken up further if the complexity so demanded. Thought was given to how each piece could be tested and how to maintain separation of concerns. In general best practices were applied for this level of application.

### Unit Tests
The site code was architected to make it easy to unit test by using interfaces and avoiding hard coded dependencies. There were some basic unit tests created for for the UI and the backend components. Best practices were used as well as many third party libraries to minimize the amount of code needed.

### CI/CD
The site is being deployed to an Azure Web App on Linux via GitHub actions.

### Data Caching
Since the data for the site is stable and the endpoints available can return multiple MB in a single call, the returned data is being cached using Microsoft's memory cache.

### App Configuration
While the application doesn't have a real need for configuration options, a cache timeout parameter was added to demonstrate how configurations could be managed using the IOptions pattern.

### Routing with parameters
The statistics page can accept optional routing parameters.
Here is a link for [6/4/2020](https://swcovidtracker-gxgdh4embkbshshy.centralus-01.azurewebsites.net/2020-06-04)

### Custom Filter Component
A custom MudDataGrid column filter template for a better filtering experience. You can see the component code [here](https://github.com/AlienArc/CovidTrackerDemo/blob/main/CovidTracker.Web/Components/DataGridStringFilterTemplate.razor)

### Third Party Libraries
Where it made sense, third party libraries were used to keep from re-inventing the wheel.
The libraries used include:

#### UI & Application
* [MudBlazor](https://mudblazor.com) - A Material Design component framework for Blazor.
* [AutoMapper](https://automapper.org) - A library for getting rid of code that mapped one object to another.

#### Unit Testing
* [nUnit](https://nunit.org) - A testing framework for all .Net languages.
* [bUnit](https://bunit.dev) - A testing library for blazor components.
* [AutoFixture](https://github.com/AutoFixture/AutoFixture) - A library to simplify the `Arrange` phase of unit testing.
* [nSubstitute](https://nsubstitute.github.io) - Another unit-testing framework for easier mocking.
* [Fluent Assertions](https://fluentassertions.com) - Yet another unit-testing framework for easier asserting.







