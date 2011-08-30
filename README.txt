Daedalus Game Manager README
Copyright 2010 (c) Mathew Bergt, Jason Buck, Ken Kelley, and Justin Weaver.


In This Directory:
--------------------------------------------------------------------------------
* DaedalusAIClient: This is a skeleton AI client (currently Java only).  The 
  networking and most of the protocol related code are provided so that someone 
  wishing to write an AI can largely focus on that task without being bothered 
  with the details of communicating with DGM.
* DaedalusGameManager: This is the actual game manager itself. 
* DaedalusGameProtocol: This is an implementation of the protocol used to 
  communicate with DGM.  See documentation defining this protocol in the Docs 
  directory.
* Docs: This is where you will find all of this project’s documentation 
  (including this file).
* DaedalusGUIClient: This is a fully functional graphical client that connects 
  to DGM.  It was designed both as an example of how a client could be 
  implemented, and as a way to test DGM.  It was mostly built reusing assets 
  from DGM.
* DaedalusTextClient: This includes C# and Java command-line clients.  The 
  command line clients act as simpler demonstrations than the GUI client for how 
  to use the protocol. 
* Games: This is where the implementations of currently supported games are 
  stored and where all future game implementations should be added.  There is 
  also a skeleton game project to act as a starting-off point for adding support
  for a new game.

  
Basic Info:
--------------------------------------------------------------------------------
To compile and run the game manager, open the DaedalusGameManager solution in
Microsoft Visual Studio 2010.  Press F5 to compile and run the program. The
manager will start up and listen for new connections.  The first client to 
connect is player one, and the second is player 2.  Once two clients have 
connected to the server, each is then assigned a player number, and the game 
begins.

See the DGM Documentation archive (or the 'Docs' directory of the source code 
repository) for more information, including details about how clients interact 
with the server.


Miscellaneous Notes:
--------------------------------------------------------------------------------
* Tzaar Board graphic (GreenTzaarBoard.jpg) was designed by and is the property
of Wil Lau (http://www.willau.ca). (Used with permission).