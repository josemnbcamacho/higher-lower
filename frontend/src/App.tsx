import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { RouterProvider } from '@tanstack/react-router';
import { GameProvider } from './store/gameStore';
import { router } from './routes';

// Create a client
const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      refetchOnWindowFocus: false,
      retry: 1,
    },
  },
});

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <GameProvider>
        <RouterProvider router={router} />
      </GameProvider>
    </QueryClientProvider>
  );
}

export default App;
