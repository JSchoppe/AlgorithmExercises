using System;
using System.Collections.Generic;
using System.Threading;

namespace Samples.Collection_Type_Comparisons
{
    public static class StackVSQueue
    {
        public static void Demo()
        {
            // Stacks and Queues are special collections that are
            // designed to be used in specific scenarios. Under
            // the hood they are typically implemented using
            // linked lists.

            // Lets consider a collection of action delegates,
            // this a very common scenario where stacks and
            // queues are useful.
            Queue<Action> actionsQueue = new Queue<Action>();
            Stack<ICommand> actionsStack = new Stack<ICommand>();

            // If we are running a continuous system we might
            // use a queue to serve actions in the order that
            // they come in.
            // Tasks comes in:
            actionsQueue.Enqueue(() => { MockAction(); Console.WriteLine("First Task Complete"); });
            actionsQueue.Enqueue(() => { MockAction(); Console.WriteLine("Second Task Complete"); });
            actionsQueue.Enqueue(() => { MockAction(); Console.WriteLine("Third Task Complete"); });
            // We handle the tasks:
            while (actionsQueue.Count > 0)
            {
                // This will always give priority to the latest task
                actionsQueue.Dequeue()();
                // If we wanted to use multi-threading to handle a large
                // queue of tasks we could use ConcurrentQueue instead.
            }

            // Stacks in contrast are used in situations where the most
            // recent collection element is prioritized. One application
            // of the stack is to facilitate the command pattern. This
            // is common for implementing undo/redo buttons in programs.
            // User executes commands in a graphics editor:
            ICommand drawCommand = new DrawCommandMock(); drawCommand.Do();
            actionsStack.Push(drawCommand);
            ICommand splineCommand = new SplineCommandMock(); splineCommand.Do();
            actionsStack.Push(splineCommand);
            // User decides they want to undo the last spline they placed.
            // We simply undo the last command and pop it off the stack.
            actionsStack.Pop().Undo();
            // If we wanted redo functionality we could move this popped action
            // onto a seperate stack (this stack would have to be cleared once
            // the user executed a new command causing a branching timeline).

            // Stacks and Queues should not be used in situations where
            // you need to access elements randomly; they are designed
            // specifically for problems oriented by the front or back
            // of the collection.
        }

        #region Mock Items
        private interface ICommand
        {
            void Do();
            void Undo();
        }

        private class DrawCommandMock : ICommand
        {
            public void Do()
            {
                Console.WriteLine("Drew Box");
            }
            public void Undo()
            {
                Console.WriteLine("Erased Box");
            }
        }
        private class SplineCommandMock : ICommand
        {
            public void Do()
            {
                Console.WriteLine("Drew Spline");
            }
            public void Undo()
            {
                Console.WriteLine("Erased Spline");
            }
        }

        private static void MockAction()
        {
            // Do some stuff that takes time.
            Thread.Sleep(3000);
        }
        #endregion
    }
}
