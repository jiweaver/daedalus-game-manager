/* $Id$
 *
 * Description: Skeleton for a concrete implementation of an AIClient.
 *
 * Copyright (c) 2010-2011, Team Daedalus (Mathew Bergt, Jason Buck, Ken Kelley, and 
 * Justin Weaver).
 * 
 * Distributed under the BSD-new license. For details see the BSD_LICENSE file 
 * that should have been included with this distribution. If the source you 
 * acquired this distribution from incorrectly removed this file, the license 
 * may be viewed at http://www.opensource.org/licenses/bsd-license.php.
 */

public class SkeletonAIClient extends AIClient
{		
	public SkeletonAIClient(String ipString)
	{
		super(ipString);
	}
	
	/**
     * Stub method to check for version compatability.  Please refer to the
	 * Daedalus Game Manager protocol document for the format of the version 
	 * string.
	 *
     * @param message
     */
    public void checkVersion(String message)
    {
        // Insert code to parse the manager's version info.
        System.out.println(message);
    }
	
	/**
     * Stub method to display a chat message received from the other player.
	 * Please refer to the Daedalus Game Manager protocol document for the 
	 * format of the chat message.
	 *
     * @param message
     */
    public void displayChatMessage(String message)
    {
        // Insert code to parse and display the chat message.
        System.out.println(message);
    }
	
    /**
     * Set the local data structure for the game board based on the board state
	 * received from the game server. Please refer to the Daedalus Game Manager
	 * protocol document for the format of the game state string.
	 *
     * @param board
     */
    public void setupBoard(String board)
	{
		System.out.println(board);
	}
	
    /**
     * Make the opponent's move and update the game state.
     * Please refer to the Daedalus Game Manager protocol document for the
	 * format of the game move string.
	 *
     * @param move The move message received from the game server.
     */
    public void opponentMove(String move)
    {
		System.out.println(move);
	}
	
    /**
     * Returns a string representing the AI's move selection. Please refer to 
	 * the Daedalus Game Manager protocol document for the format of the game 
	 * move string.
	 *
	 * @return A string representing the AI's move in the protocol format.
     */
    public String getMove()
	{
		System.out.println("Sending Move: Move{}");
		return "Move{}";
	}

    /**
     * Main method which gets the command line argument (the IP address) and
     * creates a new instance of the game client.
	 *
     * @param args The command line arguments.
     */
    public static void main(String[] args)
    {
        // Instantiate client.
        SkeletonAIClient client;
        // Check if the command line arguments are the right size.
        if (args.length != 1)
            client = new SkeletonAIClient("127.0.0.1");
        else
        	client = new SkeletonAIClient(args[0]);

		if(client.isConnected())
		{
			System.out.println("Connected.");

			// Play the Game.
			client.playGame();

			// Clean up.
			client.disconnectClient();
		}
		else
			System.out.println("Connection to specified host could not be established");

		// Exit.
		System.exit(0);
    }
}