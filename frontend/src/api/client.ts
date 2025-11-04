import axios from 'axios';
import type {
  StartGameRequest,
  GuessRequest,
  GameStartedResult,
  TurnResult,
  GameStateResult,
} from '../types/api';

const API_BASE_URL = import.meta.env.VITE_API_URL || 'http://localhost:8000';

const apiClient = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

export const gameApi = {
  startGame: async (request: StartGameRequest): Promise<GameStartedResult> => {
    const response = await apiClient.post<GameStartedResult>('/HigherOrLowerGame', request);
    return response.data;
  },

  makeGuess: async (request: GuessRequest): Promise<TurnResult> => {
    const response = await apiClient.post<TurnResult>('/HigherOrLowerGame/guess', request);
    return response.data;
  },

  getGameState: async (gameId: string): Promise<GameStateResult> => {
    const response = await apiClient.get<GameStateResult>(`/HigherOrLowerGame/${gameId}`);
    return response.data;
  },
};
