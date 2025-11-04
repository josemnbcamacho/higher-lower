import { useState, useEffect } from 'react';
import { useMutation, useQuery } from '@tanstack/react-query';
import { useNavigate } from '@tanstack/react-router';
import { gameApi } from '../api/client';
import { useGame } from '../store/gameStore';
import { Guess } from '../types/api';
import { Card } from '../components/Card';
import { Button } from '../components/Button';

export function Game() {
  const { gameState, updateCard, nextPlayer, endGame } = useGame();
  const navigate = useNavigate();
  const [lastGuessResult, setLastGuessResult] = useState<{
    correct: boolean;
    message: string;
  } | null>(null);

  // Redirect if no game in progress
  useEffect(() => {
    if (!gameState.gameId) {
      navigate({ to: '/' });
    }
  }, [gameState.gameId, navigate]);

  // Poll game state periodically
  const { data: gameStateData } = useQuery({
    queryKey: ['gameState', gameState.gameId],
    queryFn: () => gameApi.getGameState(gameState.gameId!),
    enabled: !!gameState.gameId,
    refetchInterval: 5000,
  });

  const guessMutation = useMutation({
    mutationFn: gameApi.makeGuess,
    onSuccess: (data) => {
      updateCard(data.drawnCardName);
      setLastGuessResult({
        correct: data.correctGuess,
        message: data.correctGuess ? 'Correct! 🎉' : 'Wrong! 😔',
      });

      // Clear the result after 2 seconds
      setTimeout(() => setLastGuessResult(null), 2000);

      if (data.gameEnded) {
        endGame();
        setTimeout(() => navigate({ to: '/results' }), 2000);
      } else {
        nextPlayer();
      }
    },
  });

  const handleGuess = (guess: Guess) => {
    if (!gameState.gameId || !gameState.players[gameState.currentPlayerIndex]) {
      return;
    }

    guessMutation.mutate({
      gameId: gameState.gameId,
      playerId: gameState.players[gameState.currentPlayerIndex].id,
      guess,
    });
  };

  if (!gameState.gameId || !gameState.currentCard) {
    return null;
  }

  const currentPlayer = gameState.players[gameState.currentPlayerIndex];

  return (
    <div className="min-h-screen flex items-center justify-center p-4">
      <div className="max-w-4xl w-full animate-fade-in">
        {/* Current Player Indicator */}
        <div className="text-center mb-8">
          <div className="inline-block bg-white/10 backdrop-blur-lg rounded-2xl px-8 py-4 border border-white/20">
            <p className="text-blue-200 text-sm mb-1">Current Player</p>
            <h2 className="text-3xl font-bold text-white">
              {currentPlayer?.name}
            </h2>
          </div>
        </div>

        {/* Main Game Area */}
        <div className="bg-white/10 backdrop-blur-lg rounded-2xl shadow-2xl p-8 border border-white/20">
          {/* Card Display */}
          <div className="flex justify-center mb-8">
            <Card
              cardName={gameState.currentCard}
              animate={guessMutation.isPending}
            />
          </div>

          {/* Guess Result Feedback */}
          {lastGuessResult && (
            <div
              className={`text-center mb-6 p-4 rounded-lg animate-slide-up ${
                lastGuessResult.correct
                  ? 'bg-green-500/20 border border-green-400/50'
                  : 'bg-red-500/20 border border-red-400/50'
              }`}
            >
              <p
                className={`text-2xl font-bold ${
                  lastGuessResult.correct ? 'text-green-100' : 'text-red-100'
                }`}
              >
                {lastGuessResult.message}
              </p>
            </div>
          )}

          {/* Question */}
          <h3 className="text-2xl font-semibold text-white text-center mb-8">
            Will the next card be Higher or Lower?
          </h3>

          {/* Guess Buttons */}
          <div className="grid grid-cols-2 gap-6">
            <Button
              size="lg"
              variant="success"
              onClick={() => handleGuess(Guess.Higher)}
              disabled={guessMutation.isPending}
              className="text-2xl py-8"
            >
              ⬆️ Higher
            </Button>
            <Button
              size="lg"
              variant="danger"
              onClick={() => handleGuess(Guess.Lower)}
              disabled={guessMutation.isPending}
              className="text-2xl py-8"
            >
              ⬇️ Lower
            </Button>
          </div>

          {/* Error Message */}
          {guessMutation.isError && (
            <div className="mt-6 bg-red-500/20 border border-red-400/50 rounded-lg p-4">
              <p className="text-red-100 text-center">
                Failed to submit guess. Please try again.
              </p>
            </div>
          )}
        </div>

        {/* Player Scores */}
        {gameStateData && (
          <div className="mt-8 bg-white/10 backdrop-blur-lg rounded-2xl p-6 border border-white/20">
            <h3 className="text-xl font-semibold text-white mb-4 text-center">
              Scoreboard
            </h3>
            <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-4">
              {gameStateData.playerScores.map((player) => (
                <div
                  key={player.id}
                  className={`bg-white/10 rounded-lg p-4 text-center border-2 ${
                    player.id === currentPlayer?.id
                      ? 'border-yellow-400'
                      : 'border-transparent'
                  }`}
                >
                  <p className="text-blue-200 text-sm truncate">
                    {player.name}
                  </p>
                  <p className="text-3xl font-bold text-white">
                    {player.score}
                  </p>
                </div>
              ))}
            </div>
          </div>
        )}
      </div>
    </div>
  );
}
