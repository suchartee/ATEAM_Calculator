//
//  ChutesAndLaddersGame.cpp
//

#include <iostream>
#include <string>

#include "ChutesAndLaddersGame.hpp"
#include "GameBoard.hpp"
#include "Player.hpp"

using namespace std;

// TODO: implement the constructor with all your team members
// constructor with the default value of a minimum players
// Each of your team member is enqueued and waits for her / his turn to play the game.
ChutesAndLaddersGame::ChutesAndLaddersGame(int nPlayers) : winner("no winner") {
   // TODO: implement this function properly
	team = new ArrayQueue<Player>(nPlayers);
	
	Player member1("Alice"), member2("Justin"), member3("Andrea");
	team->enqueue(member1);
	team->enqueue(member2);
	team->enqueue(member3);

	firstname = member1.getName();
}

// TODO: implement the destructor
// destructor - dequeue players from the queue
ChutesAndLaddersGame::~ChutesAndLaddersGame() {
   // TODO: implement this function properly
	for (int i = 0; i < team->size(); i++) {
		team->dequeue();
	}
	delete team;
}

// TO DO: implement this function properly
// reset the game - rebuild the list of players
//        (i.e., the list should be the same as in the constructor).
//        Place all players at the figurative square zero
void ChutesAndLaddersGame::resetGame() {
   // TODO: implement this function properly
	Player temp;		

	// set everyone's position = 0
	for (int i = 0; i < team->size(); i++) {		
		temp = team->front();
		team->dequeue();
		temp.setPostion(0);
		team->enqueue(temp);
	}
	
	// check if the first player (from the constructor) == the list's front
	// so that the order of all players is the same, just like rebuild 
	while (team->front().getName() != firstname) {
		temp = team->front();
		team->dequeue();
		team->enqueue(temp);
	}
}

// TO DO: implement this function properly
// Play the chutes and ladders until a player reaches the winning square 100
//    - Each player takes turn to roll the die and moves to the new square by invoking rollDieAndMove.
//         If the new square is outside of the board, the player stays put
//    - Player's new position is checked against the game board's checkChutesLadders
//    - checkChutesLadders returns a different square if player lands on a chute or a ladder
//    - Player's position is then set to the new position as indicated by checkChutesLadders
//          If player lands on a chute or a ladder, player slides down or climbs up
//    - If player lands on the winning square 100, game is over
//    - playGame returns after congratulating and printing the winner's name
void ChutesAndLaddersGame::playGame() {
	// TODO: implement this function properly
	bool isWinner = false;
	Player temp;
	while (!isWinner) {
		temp = team->front();
		// check if there is anyone in the square 100?
		if (temp.getPostion() == WINNING_POSITION) {
			winner = temp.getName();
			cout << "Congratulations " << getWinner() << "! You are the winner." << endl;
			isWinner = true;
			continue;
		}
		// if not, each player takes their turn
		else {
			temp.setPostion(temp.rollDieAndMove());
			temp.setPostion(gameBoard.checkChutesLadders(temp.getPostion()));

			// flip the player at first index to last index, player at second index now stays at the first index
			team->dequeue();
			team->enqueue(temp);			
		}
	}
}
