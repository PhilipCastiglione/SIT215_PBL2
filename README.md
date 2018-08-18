# Knight's Tour

This repository contains a solver for the Knight's Tour problem for Deakin Artificial and Computational Intelligence PBL Task 2.

The solver is a Visual Studio solution with a project that is a C# (.NET Core 2.0) implementation of, at a high level, Directed Acyclic Graph search.

## Solution Details

The project contains a representation of the Knight's Tour problem in a class called Board. This class represents the problem as a single board state that knows information about itself and what legal board states are can be reached from this state.

The problem representation permits board sizes of any m * m, open or closed tours, and choice of starting position for the night. The board state is represented as an unrolled (1d) array of cells.

The project contains an abstract Search class, which supports state space search through Directed Acyclic Graphs using a stack, an explored node collection and a frontier. Search operates on any INode, an interface which Board implements.

There are two search implementations in the repository.

### Depth First Search

A typical DFS implementation that can solve the problem, but for non-trivial board sizes (say larger than 5x5) is very slow. DFS is uninformed and so has to take many wrong turns over a large search space. DFS is at least superior to, say, BFS, because we know there are exactly m * m steps to reach any solution, so BFS must explore almost the entire search space to complete.

### Warnsdorfs Rule

This subclass, implements [this heuristic](https://en.wikipedia.org/wiki/Knight%27s_tour#Warnsdorf's_rule) for efficiently traversing the Knight's Tour state space. A version of backtracking is also implemented to manage the traversal process, since we have a strong heuristic for choosing the next move. This is functionally similar to greedy best first search but not identical as calculation of the heuristic involves looking one step ahead, which affords some optimisation opportunities.

## Usage

To use this solution:

1. Download the code from the [GitHub repository](https://github.com/PhilipCastiglione/SIT215_PBL2)
2. Open the solution in Visual Studio (built in Community 2017 edition, anything that supports .NET Core 2.0 will do)
3. Open the file in the only project, called `Program.cs` - this is the entry point
4. Choose values for the variables at the top of the `Main` method (startIndex is optional)
5. Run the project

#### Running Notes

Currently, the chosen search is the Warndorf's Rule class. 

Not all configurations result in a solution. For example, odd board dimensions have no closed tours.

*WARNING*: some configurations result in very slow search times. For example if you use the Depth First Search on e.g. an 8x8 board, it may never (pragmatically) complete.

Variables:

- `dimensions`: these are the (square) dimensions for the board
- `openTour`: true if the tour should be an open tour, false if it should be closed
- `startIndex`: this is an optional variable that can be used to choose a specific starting location for the Knight. note that you will need to change the call to `new Board` to use this variable. The index 0 refers to the topleft cell, (dimensions * dimensions - 1) would refer to the bottom right cell. Unexpected behaviour will result for indices not on the board.
