# GymLifts
### Author:
> Sakarias Lilja

### Table of Contents:
	1. Idea of application
	2. Data explanation
	3. Data storage

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