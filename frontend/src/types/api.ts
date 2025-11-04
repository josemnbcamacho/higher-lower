// Guess type
export const Guess = {
  Higher: 0,
  Lower: 1,
} as const;

export type Guess = typeof Guess[keyof typeof Guess];

// Request Types
export interface StartGameRequest {
  numberOfPlayers: number;
}

export interface GuessRequest {
  gameId: string;
  playerId: string;
  guess: Guess;
}

// Response Types
export interface PlayerResult {
  id: string;
  name: string;
}

export interface GameStartedResult {
  gameId: string;
  drawnCardName: string;
  players: PlayerResult[];
}

export interface TurnResult {
  correctGuess: boolean;
  drawnCardName: string;
  gameEnded: boolean;
}

export interface PlayerScoreResult {
  id: string;
  name: string;
  score: number;
}

export interface GameStateResult {
  gameEnded: boolean;
  playerScores: PlayerScoreResult[];
}
