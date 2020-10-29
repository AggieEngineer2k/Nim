# NIM

### Summary
This project is an example of a .NET Core web application hosted in Azure.  
https://nim.azurewebsites.net

### Composition
#### Nim.Solver
This class library provides a method for solving a game board for the next move, which will contain the zero-based heap index and the number of objects to remove from it.
``` c#
// A winning position.
var heaps = new[] {3, 5, 7};
var nextMove = NextMove.Solve(heaps);
// Returns: Heap = 0, Number = 1
```
If no move is found, `null` will be returned.
``` c#
// A losing position.
var heaps = new[] {1, 1, 1};
var nextMove = NextMove.Solve(heaps);
// Returns: null
```

#### Nim.Solver.Tests
Unit tests for the Nim solver.

#### Nim.Web
A .NET Core 3.1 web application. User experience is written in TypeScript.

The solver is hosted as RESTful API endpoint. The computer player's AI and the hint system both use the endpoint.

Here is an example of a winning position.
```
 Request: POST https://nim.azurewebsites.net/api/nextmove {heaps: [3,5,7]}
Response: HTTP 200 {heap: 0, number: 1}
```

Here is an example of a losing position.
```
 Request: POST https://nim.azurewebsites.net/api/nextmove {heaps: [1,1,1]}
Response: HTTP 204
```