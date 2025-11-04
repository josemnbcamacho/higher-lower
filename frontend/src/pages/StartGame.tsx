import { useState } from 'react';
import { useMutation } from '@tanstack/react-query';
import { useNavigate } from '@tanstack/react-router';
import { gameApi } from '../api/client';
import { useGame } from '../store/gameStore';
import { Button } from '../components/Button';

export function StartGame() {
  const [numberOfPlayers, setNumberOfPlayers] = useState(2);
  const { startGame } = useGame();
  const navigate = useNavigate();

  const startGameMutation = useMutation({
    mutationFn: gameApi.startGame,
    onSuccess: (data) => {
      startGame(data);
      navigate({ to: '/game' });
    },
  });

  const handleStartGame = () => {
    startGameMutation.mutate({ numberOfPlayers });
  };

  return (
    <div className="min-h-screen flex items-center justify-center p-4">
      <div className="max-w-2xl w-full animate-fade-in">
        {/* Title */}
        <div className="text-center mb-12">
          <h1 className="text-6xl font-bold text-white mb-4 drop-shadow-lg">
            Higher or Lower
          </h1>
          <p className="text-xl text-blue-200">
            Guess if the next card is higher or lower!
          </p>
        </div>

        {/* Card Container */}
        <div className="bg-white/10 backdrop-blur-lg rounded-2xl shadow-2xl p-8 border border-white/20">
          <div className="space-y-8">
            {/* Game Rules */}
            <div className="bg-blue-500/20 rounded-lg p-6 border border-blue-400/30">
              <h2 className="text-2xl font-semibold text-white mb-3">
                How to Play
              </h2>
              <ul className="text-blue-100 space-y-2 list-disc list-inside">
                <li>A card will be shown to you</li>
                <li>Guess if the next card is Higher or Lower</li>
                <li>Correct guesses earn you a point</li>
                <li>The game continues until the deck runs out</li>
                <li>Player with the most points wins!</li>
              </ul>
            </div>

            {/* Player Selection */}
            <div>
              <label className="block text-white text-lg font-semibold mb-4">
                Number of Players (2-12)
              </label>
              <div className="flex items-center gap-4">
                <input
                  type="range"
                  min="2"
                  max="12"
                  value={numberOfPlayers}
                  onChange={(e) => setNumberOfPlayers(parseInt(e.target.value))}
                  className="flex-1 h-3 bg-blue-200 rounded-lg appearance-none cursor-pointer accent-purple-600"
                />
                <div className="w-16 h-16 bg-white rounded-xl flex items-center justify-center shadow-lg">
                  <span className="text-3xl font-bold text-purple-600">
                    {numberOfPlayers}
                  </span>
                </div>
              </div>
            </div>

            {/* Start Button */}
            <Button
              size="lg"
              onClick={handleStartGame}
              disabled={startGameMutation.isPending}
              className="w-full text-xl"
            >
              {startGameMutation.isPending ? 'Starting Game...' : 'Start Game'}
            </Button>

            {/* Error Message */}
            {startGameMutation.isError && (
              <div className="bg-red-500/20 border border-red-400/50 rounded-lg p-4">
                <p className="text-red-100 text-center">
                  Failed to start game. Please make sure the backend is running.
                </p>
              </div>
            )}
          </div>
        </div>

        {/* Footer */}
        <div className="text-center mt-8 text-blue-200">
          <p className="text-sm">
            Built with React, TanStack Router & .NET 9
          </p>
        </div>
      </div>
    </div>
  );
}
