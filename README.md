# GymLifts - Mobile App
### Author:
> Sakarias Lilja

### Table of Contents:
	1. Idea of application
	2. Data explanation
	3. Data storage
	4. Functionality
	5. Other

### 1. Idea of application
The application functions as a personal project to develop and showcase my skills.

The program is written in C#, utilising the .NET Framework and .NET MAUI (~ 
a newer Xamarin) and SQLite to produce a multi-platform mobile application from a single 
code-base. The app utilises a Shell navigation system and employs a model-view-
viewmodel architecture. This is a tried and true architecture, which emphasises
system security through layers, where functionality is split between layers and 
the communication between the layers is restricted only allowing for communication
to occur with layers directly below.

The application's primary use case is for its users to track
and record their lifts. This can help the user to better visualise and track 
their progress in the gym.

### 2. Data explanation
Data is visualised with graphs. The graph's nodes can be accessed to view more 
information about the data point. 

The data can also be viewed in a list view, 
which also provides additional control over the data points, such as deleting 
the data point.

Each data point is composed of 7 values:

1. The name of the exercise
1. The weight lifted (in kg)
1. The number of repetitions - reps - performed during the set
1. RPE - rate of perseived exertion - of the set
1. Time and date of the lift
1. The category of the exercise, i.e. dumbbell, barbell, machine, etc.
1. The targeted muscle group of the exercise

### 3. Data storage
Data is stored in multiple ways. The recorded lifts are stored in a local database
utilising SQLite. This allows for fast I/O operations as databases operate in
log(n) time with most of their operations. 

Currently, some other functionalities are stored in JSON and CSV files, but will
be migrated to online databases and accessed with API:s. 

### 4. Functionality
The user can navigate through the app with the help of both buttons and tabs. 
Each page has a distinct purpose, whether that is to register a new exercise or
to view the recorded lifts of an exercise.

The main functionality of the application is the ability to view the progression
with a graph. Each node in the graph can be selected, displaying additional information
regarding the data point.

The graph is drawn using LiveCharts, that builds upon SkiaSharp to draw figures on
the display. The graph is dynamic and supports e.g. scrolling and zooming.

The application also has an optional dark mode, that changes the look of the app
to a more dark tone, making it better suited for when used in darker surroundings.

### 5. Other
The application utilises for the most part lists, as no sorting of the elements
is needed. Additionally, this data format is required by the LiveCharts graphing
tool. 

For navigation between pages, the application uses a shell. This is an URI-based
navigation system, that allows for simple navigation stacks and methods. It's also
quite performant.