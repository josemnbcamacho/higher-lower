# Higher or Lower - Frontend

A beautiful React frontend for the Higher or Lower card game, built with modern technologies.

## Tech Stack

- **React 18** with TypeScript
- **TanStack Router** for routing
- **TanStack Query** for data fetching and caching
- **Tailwind CSS** for styling
- **Vite** for build tooling
- **Axios** for API calls

## Features

- 🎮 Interactive card game interface
- 🎨 Beautiful gradient UI with animations
- 📱 Responsive design
- 🔄 Real-time score updates
- 🏆 Podium-style results screen
- 🎯 Support for 2-12 players

## Development

### Prerequisites

- Node.js 20+
- npm or yarn

### Setup

1. Install dependencies:
```bash
npm install
```

2. Copy the environment file:
```bash
cp .env.example .env
```

3. Update the API URL in `.env` if needed:
```
VITE_API_URL=http://localhost:8000
```

4. Start the development server:
```bash
npm run dev
```

The app will be available at `http://localhost:3000`

## Building for Production

```bash
npm run build
```

The built files will be in the `dist` directory.

## Docker

Build and run with Docker:

```bash
docker build -t higher-lower-frontend .
docker run -p 3000:80 higher-lower-frontend
```

Or use docker-compose from the root directory:

```bash
cd ..
docker-compose up
```

## Project Structure

```
src/
├── api/           # API client and endpoints
├── components/    # Reusable UI components
├── lib/          # Utility functions
├── pages/        # Route pages
├── store/        # Global state management
├── types/        # TypeScript type definitions
├── routes.tsx    # Route configuration
├── App.tsx       # Root component
└── main.tsx      # Entry point
```

## License

MIT
