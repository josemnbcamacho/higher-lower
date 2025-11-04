import { createContext, useContext, useState, useCallback, type ReactNode } from 'react';
import type { GameStartedResult, PlayerResult } from '../types/api';

interface GameState {
  gameId: string | null;
  currentCard: string | null;
  players: PlayerResult[];
  currentPlayerIndex: number;
  gameEnded: boolean;
}

interface GameContextType {
  gameState: GameState;
  startGame: (result: GameStartedResult) => void;
  updateCard: (cardName: string) => void;
  nextPlayer: () => void;
  endGame: () => void;
  resetGame: () => void;
}

const GameContext = createContext<GameContextType | undefined>(undefined);

const initialState: GameState = {
  gameId: null,
  currentCard: null,
  players: [],
  currentPlayerIndex: 0,
  gameEnded: false,
};

export function GameProvider({ children }: { children: ReactNode }) {
  const [gameState, setGameState] = useState<GameState>(initialState);

  const startGame = useCallback((result: GameStartedResult) => {
    setGameState({
      gameId: result.gameId,
      currentCard: result.drawnCardName,
      players: result.players,
      currentPlayerIndex: 0,
      gameEnded: false,
    });
  }, []);

  const updateCard = useCallback((cardName: string) => {
    setGameState((prev) => ({
      ...prev,
      currentCard: cardName,
    }));
  }, []);

  const nextPlayer = useCallback(() => {
    setGameState((prev) => ({
      ...prev,
      currentPlayerIndex: (prev.currentPlayerIndex + 1) % prev.players.length,
    }));
  }, []);

  const endGame = useCallback(() => {
    setGameState((prev) => ({
      ...prev,
      gameEnded: true,
    }));
  }, []);

  const resetGame = useCallback(() => {
    setGameState(initialState);
  }, []);

  return (
    <GameContext.Provider
      value={{
        gameState,
        startGame,
        updateCard,
        nextPlayer,
        endGame,
        resetGame,
      }}
    >
      {children}
    </GameContext.Provider>
  );
}

export function useGame() {
  const context = useContext(GameContext);
  if (context === undefined) {
    throw new Error('useGame must be used within a GameProvider');
  }
  return context;
}
