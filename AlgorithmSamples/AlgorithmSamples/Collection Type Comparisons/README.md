# Collection Type Comparisons
Collections are a corner stone of programming. There are a handful of common collection
implementations that are typically available in the standard library of any given language.
Each implementation is applicable in certain situations. This article will discuss:
 - Order Oriented Collections
   - Stack
   - Queue
 - Access Oriented Collections
   - Array
   - Map
## Stack VS Queue
Lets consider a collection of action delegates, this a very common scenario
where stacks and queues are useful.
```cs
Queue<Action> actionsQueue = new Queue<Action>();
Stack<Action> actionsStack = new Stack<Action>();
```
### Queue Use Case
If we are running a continuous system we might use a queue to serve actions in the
order that they come in. Tasks comes in:
```cs
actionsQueue.Enqueue(() => { MockAction(); Console.WriteLine("First Task Complete"); });
actionsQueue.Enqueue(() => { MockAction(); Console.WriteLine("Second Task Complete"); });
actionsQueue.Enqueue(() => { MockAction(); Console.WriteLine("Third Task Complete"); });
```
Then we handle the tasks:
```cs
while (actionsQueue.Count > 0)
{
    // This will always give priority to the latest task
    actionsQueue.Dequeue()();
    // If we wanted to use multi-threading to handle a large
    // queue of tasks we could use ConcurrentQueue instead.
}
```
### Stack Use Case
Stacks in contrast are used in situations where the most recent collection element is prioritized.
One common application of the stack is to facilitate the command pattern. This is common for
implementing undo/redo buttons in programs. Suppose the user executes commands in a graphics editor:
```cs
ICommand drawCommand = new DrawCommandMock();
drawCommand.Do();
actionsStack.Push(drawCommand);
ICommand splineCommand = new SplineCommandMock();
splineCommand.Do();
actionsStack.Push(splineCommand);
```
The stack gives us direct access to the most recent command if we want to undo it:
```cs
actionsStack.Pop().Undo();
```
If we wanted redo functionality we could move this popped action onto a seperate stack
(this stack would have to be cleared once the user executed a new command causing a branching timeline).
### Bad Use Cases for Stack and Queue
Stacks and Queues should not be used in situations where you need to access elements randomly;
they are designed specifically for problems oriented by the front or back of the collection.
## Array VS Map
In C# land we have the following collections that correspond to arrays and maps:
```cs
string[] namesArray = new string[0];
List<string> namesList = new List<string>();
Dictionary<int, string> namesDictionary = new Dictionary<int, string>();
Hashtable namesHashtable = new Hashtable();
```
A List can be thought of as an Array that resizes itself.
A Hashtable is a typeless map, and a Dictionary is a typed map.
### Why Use Maps?
Maps solve a common problem in programming; they allow you to create collections
that are not indexed by number, but instead indexed by any type that can be ran
through a hashing function (converts the object passed into a key). Maps also
allow for gaps in the access keys. In a dictionary where an int index is used
as the key, having an element at dictionary[5000] does not require 5000 prior
empty slots to be occupying memory (which it would in the case of an array or list).
In addition to these benefits, maps have constant access time as they simply need
to run the object through the hashing function to find the values destination.
### Why Not Use Maps for Everything?
Maps are brilliant but they come with some overhead. If you know that your data
is strictly ordered and can be accessed by an integer index, maps really are not
worth it (except maybe in some cases where you are working with massive data sets
that might be too big to cram into a single segment of memory).
### List Use Case
Lists are useful for keeping track of data that is not associated to other data.
You might use it for a mailing list, where the list could contains email addresses.
```cs
namesList.Add("david@gmail.com");
...
foreach (string email in namesList)
    SendEmailMock(email, "buy buy buy!");
```
There is no complex relationship in our data, so the List is the way to go.
### Dictionary Use Case
If you were making a clock-in system for a company you may want to track the employees
using an ID to make sure that employees with the same name are not mistaken. While we
could just start numbering employees from zero onwards, this would not be very secure.
Enter the Dictionary:
```cs
namesDictionary.Add(1114432, "mikey");
...
Console.WriteLine($"{namesDictionary[ReadIDCardMock()]} clocked in at {DateTime.Now.ToShortTimeString()}");
```
The dictionary allows us to associate the ID data to the employee. We could even extend our functionality
by having value type be a class that stores more information about the employee.
