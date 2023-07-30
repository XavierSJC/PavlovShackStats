import Home from "./pages/Home";
import Matches from "./pages/Matches";
import PlayerStats from "./pages/PlayerStats"

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/matches',
    element: <Matches />
  },
  {
    path: '/playerstats',
    element: <PlayerStats />
  }
];

export default AppRoutes;