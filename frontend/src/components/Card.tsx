import { cn } from '../lib/utils';

interface CardProps {
  cardName: string;
  className?: string;
  animate?: boolean;
}

export function Card({ cardName, className, animate = false }: CardProps) {
  // Parse card name to extract rank and suit
  const parts = cardName.split(' of ');
  const rank = parts[0] || '?';
  const suit = parts[1] || '';

  // Determine if card is red or black
  const isRed = suit === 'Hearts' || suit === 'Diamonds';

  // Get suit symbol
  const suitSymbol = {
    'Hearts': '♥',
    'Diamonds': '♦',
    'Clubs': '♣',
    'Spades': '♠',
  }[suit] || '';

  return (
    <div
      className={cn(
        'relative w-32 h-48 bg-white rounded-xl shadow-2xl border-2 border-gray-300',
        'flex flex-col items-center justify-between p-4',
        'transition-all duration-300 hover:scale-105',
        animate && 'animate-flip',
        className
      )}
    >
      {/* Top rank and suit */}
      <div className="flex flex-col items-center">
        <span
          className={cn(
            'text-3xl font-bold',
            isRed ? 'text-red-600' : 'text-gray-900'
          )}
        >
          {rank}
        </span>
        <span
          className={cn(
            'text-4xl',
            isRed ? 'text-red-600' : 'text-gray-900'
          )}
        >
          {suitSymbol}
        </span>
      </div>

      {/* Center suit symbol */}
      <div
        className={cn(
          'text-6xl',
          isRed ? 'text-red-600' : 'text-gray-900'
        )}
      >
        {suitSymbol}
      </div>

      {/* Bottom rank and suit (rotated) */}
      <div className="flex flex-col items-center rotate-180">
        <span
          className={cn(
            'text-3xl font-bold',
            isRed ? 'text-red-600' : 'text-gray-900'
          )}
        >
          {rank}
        </span>
        <span
          className={cn(
            'text-4xl',
            isRed ? 'text-red-600' : 'text-gray-900'
          )}
        >
          {suitSymbol}
        </span>
      </div>
    </div>
  );
}
