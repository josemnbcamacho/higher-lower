import { createRootRoute, createRoute, createRouter, Outlet } from '@tanstack/react-router';
import { StartGame } from './pages/StartGame';
import { Game } from './pages/Game';
import { Results } from './pages/Results';

// Root route
const rootRoute = createRootRoute({
  component: () => <Outlet />,
});

// Index route (Start Game)
const indexRoute = createRoute({
  getParentRoute: () => rootRoute,
  path: '/',
  component: StartGame,
});

// Game route
const gameRoute = createRoute({
  getParentRoute: () => rootRoute,
  path: '/game',
  component: Game,
});

// Results route
const resultsRoute = createRoute({
  getParentRoute: () => rootRoute,
  path: '/results',
  component: Results,
});

// Create the route tree
const routeTree = rootRoute.addChildren([indexRoute, gameRoute, resultsRoute]);

// Create and export the router
export const router = createRouter({ routeTree });

// Register the router for type safety
declare module '@tanstack/react-router' {
  interface Register {
    router: typeof router;
  }
}
