# Game-of-Life

  An attempt at creating Conway's Game of Life mostly from scratch in C#.  I came across this idea recently and thought it would be a fun challenge to 
try to implement a representation of Life by doing my own research rather than following any comprehensive tutorials.  I like to get better at game development
and this project felt like a good way to practice creating intuitive GUIs, handling user inputs, and representing game models.  

## Conway's Game of Life

  Conway's Game of Life (or Life) is a zero-player game created by mathematician John Horton Conway.  The game is played with cells placed on an infinite grid, where each generation of cells depends on the following set of rules:
  
  1. Any live cell with fewer than two live neighbors dies, as if by underpopulation.
  2. Any live cell with two or three live neighbors lives on to the next generation.
  3. Any live cell with more than three live neighbors dies, as if by overpopulation.
  4. Any dead cell with exactly three live neighbors becomes a live cell, as if by reproduction.

Each generation, these conditions are applied to every cell in the game and then the game is updated to the next generation.  The resulting patterns created by these rules can become quite interesting and complex.  The user "plays'' the game by defining an initial set of cells on the grid and then starting the simulation.  Once the simulation has started, the user can only observe the ensuing generations which may die off quickly or persist indefinitely.

## Controls

  {W A S D}      --   Pan Up / Left / Down / Right Respectively
  {Z}            --   Zoom Out
  {X}            --   Zoom In
  {Left Mouse}   --   Draw Cells (before staring game)
  {Right Mouse}  --   Erase Cells (before starting game)

  I plan on adding zoom/pan functionality to mouse scroll wheel and mouse click + drag eventually

## Goals

  **Required**
  - An interactable GUI for viewing the game
  - Have a (pseudo) infinite grid on which to play the game
  - Zoom and pan functionality
  - Allow user to create and edit input patterns before starting the game
  - In-game cells adhere to the rules of the game
	
  **Extra**
  - Customizable colors of "living" and "dead" cells
  - An option to pause the game
  - Display current cell count and generation
  - Generate random starting patterns
  - Save and load an ongoing simulation

## Resources

https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life



