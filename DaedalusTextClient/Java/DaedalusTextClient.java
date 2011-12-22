/* $Id$
 *
 * Description: A simple text based client that connects to the Daedalus Game
 * Manager and allows the user to manually type and send Daedalus Game Manager 
 * Protocol messages.
 *
 * Copyright (c) 2010-2011, Team Daedalus (Mathew Bergt, Jason Buck, Ken Kelley, and Justin Weaver).
 * 
 * Distributed under the BSD-new license. For details see the BSD_LICENSE file that should
 * have been included with this distribution. If the source you acquired this distribution
 * from incorrectly removed this file, the license may be viewed at 
 * http://www.opensource.org/licenses/bsd-license.php.
 */

import java.io.*;
import java.net.*;
import java.util.StringTokenizer;
import java.util.Scanner;

public class DaedalusTextClient
{
    // To parse the command line argument.
    private StringTokenizer tokens;
    // The specified IP address.
    private InetAddress ipAddress;
    // The port number.
    private static final int PORT = 2525;
    // The socket.
    private Socket socket;
    // The input stream.
    private BufferedReader br;
    // The output stream.
	private PrintWriter out;
	// Are we connected?
	private boolean connected = false;

    /**
     * Constructor initializes a new DaedalusTextClient object. Sets up 
	 * networking components and connects to the server.
	 *
     * @param ipString The specified address of the Daedalus Game Manager.
     */
    public DaedalusTextClient(String ipString)
    {
        this.tokens = new StringTokenizer(ipString, ".", false);
        this.ipAddress = null;
        this.socket = null;
        this.br = null;

        // Attempt to initialize the instance variables.
        try
        {
            this.ipAddress = InetAddress.getByName(ipString);
            this.socket = new Socket(ipAddress, DaedalusTextClient.PORT);
            this.br = new BufferedReader(new InputStreamReader(socket.getInputStream()));
			this.out = new PrintWriter(socket.getOutputStream(), true);
        } catch (Exception e)
        {
            System.out.println("Exception caught while trying to open the connections: " + e.getMessage());
            System.exit(1);
        }

		connected = true;
	}

	public boolean isConnected()
	{
		return connected;
	}

	/**
	 * Method to shut down the networking components.
	 */
	public void disconnectClient()
	{
        try
        {
			this.out.close();
            this.br.close();
        }
		catch (Exception e)
        {
            System.out.println("Exception caught while trying to close the connection:	" + e.getMessage());
            System.exit(1);
        }
	}

    /**
     * Method to get data from the Daedalus Game Manager and choose the
     * appropriate action based on the message code received.
     */
    private void playGame()
    {
        String message = "";
        while (true)
        {
            // Read the data from the input stream.
            try
            {
				message = this.br.readLine();
				if(message.equals(""))
					continue;
            } catch (Exception e)
            {
                System.out.println("Exception caught while trying to readLine():	" + e.getMessage());
				e.printStackTrace();
				return;
            }
			
            int index = message.indexOf("{");
            String messageType = message.substring(0, index);
			System.out.println("Message received : " + messageType);

            // Call the appropriate method based on the code.
            if (messageType.equals("Version"))
                this.checkVersion(message);
            else if (messageType.equals("Chat"))
                this.displayChatMessage(message);
			else if (messageType.equals("BoardState"))
                this.setupBoard(message);
            else if (messageType.equals("Move"))
                this.opponentMove(message);
            else if (messageType.equals("YourTurn"))
                this.getMove();
			else if (messageType.equals("GameOver"))
            {
                System.out.println(message);
                return;
            }
            else if (messageType.equals("YourPlayerNumber"))
                this.setColor(message);
            else

            	System.out.println("Received unknown message : " + messageType);
        }
    }
	
	/**
     * Stub method to check for version compatability.  Please refer to the
	 * Daedalus Game Manager protocol document for the format of the version 
	 * string.
     * @param message
     */
    private void checkVersion(String message)
    {
        // Insert code to parse the manager's version info.
        System.out.println(message);
    }
	
	/**
     * Stub method to display a chat message received from the other player.
	 * Please refer to the Daedalus Game Manager protocol document for the 
	 * format of the chat message.
     * @param message
     */
    private void displayChatMessage(String message)
    {
        // Insert code to parse and display the chat message.
        System.out.println(message);
    }
	
    /**
     * Stub method to set the local data structure for the game board based
     * on the board state received from the game server. Please refer to the
     * Daedalus Game Manager protocol document for the format of the game state 
	 * string.
     * @param board
     */
    private void setupBoard(String board)
    {
        // Insert code to parse message and setup data structure for board.
        System.out.println(board);
    }

    /**
     * Stub method to make the opponent's move and update the game state.
     * Please refer to the Daedalus Game Manager protocol document for the
	 * format of the game move string.
     * @param move The move message received from the game server.
     */
    private void opponentMove(String move)
    {
        // Insert code to make the specified move for the opponent in your data structure.
        System.out.println(move);
    }

    /**
     * Method to prompt the user to make a move or pass. Careful, this method
     * allows a user to pass on any move, which is violation of the rules of
     * the game. The server will automatically notify the user of a game loss
     * in the case of an illegal move.
     */
    private void getMove()
    {
        Scanner kb = new Scanner(System.in);
        String input = null;
        // Prompt user for pass or move.
        try
        {
            System.out.println("\nIt's your turn. Please enter a move.");
            input = kb.nextLine();
            this.out.println(input);
        }
        catch (Exception e)
        {
            System.out.println("Error: please enter an integer value.");
        }
    }

    /**
     * Method to set the color of this player based on the message received
     * from the game server.
     * @param color The message received from the game server indicating the
     * color of this player.
     */
    private void setColor(String color)
    {
        System.out.println(color);
    }

    /**
     * Main method which gets the command line argument (the IP address) and
     * creates a new instance of the game client.
     * @param args The command line arguments.
     */
    public static void main(String[] args)
    {
        // Instantiate client.
        DaedalusTextClient client;
        // Check if the command line arguments are the right size.
        if (args.length != 1)
            client = new DaedalusTextClient("127.0.0.1");
        else
        	client = new DaedalusTextClient(args[0]);

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