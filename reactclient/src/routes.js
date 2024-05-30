import Home from "./pages/Home";
import Matches from "./pages/Matches";
import PlayerStats from "./pages/PlayerStats"
import Sessions from "./pages/Sessions"

const AppRoutes = [
  {
    index: true,
    path: '/',
    element: <Home />,
    title: 'Página inicial'
  },
  {
    path: '/matches',
    element: <Matches />,
    title: 'Últimas Partidas'
  },
  {
    path: '/playerstats',
    element: <PlayerStats />,
    title: 'Jogadores' 
  },
  {
    path: '/sessions',
    element: <Sessions />,
    title: 'Temporadas' 
  }
];

export default AppRoutes;