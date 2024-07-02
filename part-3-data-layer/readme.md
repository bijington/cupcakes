# Part 3 - Data layer

In this part we are going to add a data layer for this we will make use of the repository pattern and hide our data access behind it. We will also make use of an SQLite database.

## Include the Sqlite-pcl-net NuGet package



## Adding our CustomerRepository

The first step will be to create our repository class, let's do this by adding a new `class` file to the `Customers` folder and call it `CustomerRepository`.

> Note that quite often you might find that developer will create an interface for something like this, having an interface can sometime make it easier to setup unit testing but sometimes it can just create additional overhead. The decision whether to or not is not always clearly defined but I would urge that you really consider whether you need the interface, if not just stick with the class.

