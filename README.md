# GymLifts
### Author:
> Sakarias Lilja

### Table of Contents:
	1. Idea of application
	2. Data explanation
	3. Data storage
	4. Functionality
	5. Other

### 1. Idea of application
The application's primary use case is for its users to track
and record their lifts.

This can help the user to better visualise thier efforts and achievements.

### 2. Data explanation
Each data point requires 7 distinct data values.

1. The name of the exercise
1. The weight lifted (in kg)
1. The number of repetitions - reps - performed of the exercise
1. RPE - rate of perseived exertion - of the set
1. Time and date of the lift (acquired automatically)
1. The category of the exercise, i.e. dumbbell, barbell, machine, etc.
1. The targeted muscle group of the exercise

### 3. Data storage
The data is stored in many different places. The names, categories and targeted
muscle groups are stored in one file, accessible by all users, while the individual
lifts are separated by lift name and username.

This allows each application to run have multiple users at once, e.g. a user could
separate their lifts into their generics and their strength focused ones.

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