/* $Id: AIClient.java 832 2011-01-03 13:44:43Z piranther $
 *
 * Description: An abstract client that connects to the Daedalus Game Manager 
 * and allows an AI to send Daedalus Game Manager Protocol messages.
 *
 * Copyright (c) 2010, Team Daedalus (Mathew Bergt, Jason Buck, Ken Kelley, and 
 * Justin Weaver).
 * 
 * Distributed under the BSD-new license. For details see the BSD_LICENSE file 
 * that should have been included with this distribution. If the source you 
 * acquired this distribution from incorrectly removed this file, the license 
 * may be viewed at http://www.opensource.org/licenses/bsd-license.php.
 */

import java.io.*;
import java.net.*;
import java.util.StringTokenizer;
import java.util.Scanner;

public abstract class AIClient
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
	// Are we player 1, player 2, or unassigned (player 0)?
	private int playerNumber = 0;

    /**
     * Constructor initializes a new DaedalusTextClient object. Sets up  
	 * networking components and connects to the server.
	 *
     * @param ipString The specified address of the Daedalus Game Manager.
     */
    public AIClient(String ipString)
    {
        this.tokens = new StringTokenizer(ipString, ".", false);
        this.ipAddress = null;
        this.socket = null;
        this.br = null;

        // Attempt to initialize the instance variables.
        try
        {
            this.ipAddress = InetAddress.getByName(ipString);
            this.socket = new Socket(ipAddress, AIClient.PORT);
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
    public void playGame()
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
                out.println(this.getMove());
			else if (messageType.equals("GameOver"))
            {
                System.out.println(message);
                return;
            }
            else if (messageType.equals("YourPlayerNumber"))
                this.setPlayerNumber(message);
            else
            	System.out.println("Received unknown message : " + messageType);
        }
    }
	
	/**
     * Method to set the color of this player based on the message received from
     * the game server.
	 *
     * @param color The message received from the game server indicating the
     * color of this player.
     */
    private void setPlayerNumber(String playerNumber)
    {
		System.out.println(playerNumber);
		this.playerNumber = (playerNumber.charAt(17) == 'O') ? 1 : 2;
    }
	
	public int playerNumber()
	{
		return playerNumber;
	}

	/**
     * Check for version compatability.  Please refer to the Daedalus Game 
	 * Manager protocol document for the format of the Version message.
	 *
     * @param message
     */
    public abstract void checkVersion(String message);
	
	/**
     * Display a chat message received from the other player.  Please refer to
	 * the Daedalus Game Manager protocol document for the format of the chat
	 * message.
	 *
     * @param message
     */
    public abstract void displayChatMessage(String message);
	
    /**
     * Set the local data structure for the game board based on the board state
	 * received from the game server. Please refer to the Daedalus Game Manager
	 * protocol document for the format of the game state string.
	 *
     * @param board
     */
    public abstract void setupBoard(String board);

    /**
     * Make the opponent's move and update the game state.
     * Please refer to the Daedalus Game Manager protocol document for the
	 * format of the game move string.
	 *
     * @param move The move message received from the game server.
     */
    public abstract void opponentMove(String move);
    

    /**
     * Returns your AI's move selection.
     */
    public abstract String getMove();
}