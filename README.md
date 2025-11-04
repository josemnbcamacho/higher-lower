# HigherOrLower

HigherOrLower is a full-stack card game application where players guess whether the next card will be higher or lower than the current card.

## Tech Stack

### Backend
- **ASP.NET Core 9** Web API
- **Entity Framework Core** with SQLite
- **Swagger/OpenAPI** documentation

### Frontend
- **React 18** with TypeScript
- **TanStack Router** for routing
- **TanStack Query** for data fetching
- **Tailwind CSS** for styling
- **Vite** for build tooling

## Quick Start

### Using Docker Compose (Recommended)

Run both backend and frontend together:

```bash
docker-compose up -d --build
```

Access the application:
- **Frontend**: http://localhost:3000
- **Backend API**: http://localhost:8000
- **Swagger UI**: http://localhost:8000/swagger (Development mode)

### Manual Development

#### Backend
```bash
cd HigherOrLower.API
dotnet run
```

#### Frontend
```bash
cd frontend
npm install
npm run dev
```

## Game Rules

1. A card is shown to all players
2. Each player takes turns guessing if the next card will be **Higher** or **Lower**
3. Correct guesses earn the player 1 point
4. The game continues until the deck runs out of cards
5. The player with the most points wins!

## API Usage

### Create a game
```bash
curl 'http://localhost:8000/HigherOrLowerGame' \
  -H 'Content-Type: application/json' \
  --data-raw $'{\n  "numberOfPlayers": 4\n}'
```

### Play a Guess
```bash
curl 'http://localhost:8000/HigherOrLowerGame/guess' \
  -H 'Content-Type: application/json' \
  --data-raw $'{\n  "gameId": "c99b39b0-8450-4016-88a2-6bd5de9732b7",\n  "playerId": "5dd32003-af9c-4f22-9dba-230cdfa74729",\n  "guess": 0\n}'
```

### Check the Result
```bash
curl 'http://localhost:8000/HigherOrLowerGame/c99b39b0-8450-4016-88a2-6bd5de9732b7' \
  -H 'accept: */*' \
  --compressed
```

## Features

- 🎮 2-12 player support
- 🎨 Beautiful gradient UI with animations
- 📱 Responsive design
- 🔄 Real-time score updates
- 🏆 Podium-style results screen
- 🎯 RESTful API
- 🐳 Docker support

## Project Structure

```
HigherOrLower/
├── HigherOrLower.API/      # Backend API (.NET 9)
├── HigherOrLower.Tests/    # Unit tests
├── frontend/               # React frontend
├── docker-compose.yml      # Docker orchestration
└── README.md
```

## License

MIT
