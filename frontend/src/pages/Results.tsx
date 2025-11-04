import { useEffect } from 'react';
import { useQuery } from '@tanstack/react-query';
import { useNavigate } from '@tanstack/react-router';
import { gameApi } from '../api/client';
import { useGame } from '../store/gameStore';
import { Button } from '../components/Button';

export function Results() {
  const { gameState, resetGame } = useGame();
  const navigate = useNavigate();

  // Fetch final game state
  const { data: finalState } = useQuery({
    queryKey: ['finalGameState', gameState.gameId],
    queryFn: () => gameApi.getGameState(gameState.gameId!),
    enabled: !!gameState.gameId,
  });

  // Redirect if no game exists
  useEffect(() => {
    if (!gameState.gameId) {
      navigate({ to: '/' });
    }
  }, [gameState.gameId, navigate]);

  const handlePlayAgain = () => {
    resetGame();
    navigate({ to: '/' });
  };

  if (!finalState) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <div className="text-white text-2xl">Loading results...</div>
      </div>
    );
  }

  // Sort players by score
  const sortedPlayers = [...finalState.playerScores].sort(
    (a, b) => b.score - a.score
  );

  const winner = sortedPlayers[0];
  const maxScore = winner?.score || 0;

  return (
    <div className="min-h-screen flex items-center justify-center p-4">
      <div className="max-w-4xl w-full animate-fade-in">
        {/* Title */}
        <div className="text-center mb-12">
          <h1 className="text-6xl font-bold text-white mb-4 drop-shadow-lg animate-slide-up">
            🎮 Game Over! 🎮
          </h1>
          {winner && (
            <p className="text-3xl text-yellow-300 font-semibold">
              🏆 Winner: {winner.name} 🏆
            </p>
          )}
        </div>

        {/* Results Container */}
        <div className="bg-white/10 backdrop-blur-lg rounded-2xl shadow-2xl p-8 border border-white/20 mb-8">
          <h2 className="text-3xl font-semibold text-white mb-6 text-center">
            Final Scores
          </h2>

          {/* Podium Style Display */}
          <div className="space-y-4">
            {sortedPlayers.map((player, index) => {
              const isWinner = index === 0 && player.score === maxScore;
              const medals = ['🥇', '🥈', '🥉'];
              const medal = medals[index] || '🎯';

              return (
                <div
                  key={player.id}
                  className={`
                    relative overflow-hidden rounded-xl p-6 transition-all duration-300
                    ${
                      isWinner
                        ? 'bg-gradient-to-r from-yellow-500/30 to-amber-500/30 border-2 border-yellow-400 scale-105'
                        : 'bg-white/10 border border-white/20 hover:bg-white/15'
                    }
                  `}
                  style={{
                    animationDelay: `${index * 100}ms`,
                  }}
                >
                  <div className="flex items-center justify-between">
                    <div className="flex items-center gap-4">
                      <span className="text-5xl">{medal}</span>
                      <div>
                        <p
                          className={`text-2xl font-bold ${
                            isWinner ? 'text-yellow-100' : 'text-white'
                          }`}
                        >
                          {player.name}
                        </p>
                        <p className="text-blue-200 text-sm">
                          Rank #{index + 1}
                        </p>
                      </div>
                    </div>
                    <div className="text-right">
                      <p
                        className={`text-5xl font-bold ${
                          isWinner ? 'text-yellow-100' : 'text-white'
                        }`}
                      >
                        {player.score}
                      </p>
                      <p className="text-blue-200 text-sm">points</p>
                    </div>
                  </div>

                  {/* Progress Bar */}
                  <div className="mt-4 bg-white/10 rounded-full h-2 overflow-hidden">
                    <div
                      className={`h-full rounded-full transition-all duration-1000 ${
                        isWinner
                          ? 'bg-gradient-to-r from-yellow-400 to-amber-500'
                          : 'bg-gradient-to-r from-blue-500 to-purple-600'
                      }`}
                      style={{
                        width: `${maxScore > 0 ? (player.score / maxScore) * 100 : 0}%`,
                        animationDelay: `${index * 100}ms`,
                      }}
                    />
                  </div>
                </div>
              );
            })}
          </div>
        </div>

        {/* Actions */}
        <div className="flex gap-4 justify-center">
          <Button size="lg" onClick={handlePlayAgain} className="text-xl px-12">
            Play Again
          </Button>
        </div>

        {/* Stats */}
        <div className="mt-8 text-center text-blue-200">
          <p className="text-sm">
            Total Players: {sortedPlayers.length} | Highest Score: {maxScore}
          </p>
        </div>
      </div>
    </div>
  );
}
