# RiverTechTest

## Overview
RiverTechTest is a test automation project containing two separate test suites:

- **API Tests**: Uses LightBDD, FluentAssertions, NUnit, Newtonsoft.Json, and RestSharp to test API endpoints.
- **UI Tests**: Uses Selenium WebDriver, LightBDD, FluentAssertions, and NUnit to automate UI testing.

## Clone the Repository

To start, clone the repository using the following commands:

`git clone https://github.com/LuisNavarroP/RiverTechTest.git`

`cd RiverTechTest`

## Restoring the packages

Before running the tests, restore the required NuGet packages using the following command:

`dotnet restore`

## Running Tests
To run the tests, list Projects in the Solution and select the one you want to run using the following command:

`dotnet sln list`

## Run UI Tests
Execute the UI tests using:

`dotnet test UI/UI.csproj`

## Run API Tests
Execute the API tests using:

`dotnet test API/API.csproj`

## Test Reports
Test execution reports are generated in the following directories:

- API Test Reports: `RiverTechTest/API/bin/Debug/net6/Reports`
- UI Test Reports: `RiverTechTest/UI/bin/Debug/net6/Reports`

