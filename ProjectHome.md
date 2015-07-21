# Daedalus Game Manager #

## What is it? ##
Daedalus Game Manager (DGM) is a C# .NET program designed to host 2-player board games.  Currently it contains very basic Tzaar and Owari/Mancala game modules.

The primary purpose of DGM is to allow two AI clients to play against each other, but the included GUI client program allows human players to participate as well.  DGM uses a simple, documented text protocol to interact with the players (clients), which allows clients to be written in almost any programming language (e.g., Java).  DGM also provides support for user-designed games!

## Where Do I Start? ##
There are several intents with which one might want to use DGM.

  1. **Fix, Improve, and Contribute to DGM:** You will need Visual Studio 2010, and you will want to familiarize yourself with the entire project before digging in and writing some code.  A good place to start is with the class diagrams included with each component.
  1. **Add support for new games to DGM:**  You will need Visual Studio 2010, and you will want to take a look at the interface class diagrams in the DaedalasGameProtocol project.  Also, familiarize yourself with the Tzaar, Owari, and skeleton implementations in the _Games_ directory.
  1. **Write an AI-driven client for DGM:** This is what this project was truly designed for.  You will want to have at least a basic familiarity with the DGM protocol. So, download the documentation archive (or check out the _Docs_ directory in the root of the source tree).  If you will be writing your AI in Java, then we have provided an AI client skeleton, which should serve as an excellent starting point.  You don’t need to worry about the DGM source code; just check out the binaries on the homepage.

## What is in the DGM source code repository? ##
The DGM repository includes all of the source files for the DGM project; it is mostly intended for developers of new games.  In the root directory of the source code tree, you should find several directories, a README.txt file, and a copy of the BSD\_LICENSE.