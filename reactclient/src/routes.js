import Home from "./pages/Home";
import Matches from "./pages/Matches";
import PlayerStats from "./pages/PlayerStats"
import Sessions from "./pages/Sessions"

const AppRoutes = [
  {
    index: true,
    path: '/',
    element: <Home />,
    title: 'Página inicial',
    HiddenInHeader: 'false'
  },
  {
    path: '/matches',
    element: <Matches />,
    title: 'Últimas Partidas',
    HiddenInHeader: false
  },
  {
    path: '/playerstats',
    element: <PlayerStats />,
    title: 'Jogadores',
    HiddenInHeader: false
  },
  {
    path: '/sessions',
    element: <Sessions title='atual' since='2024-04-01' until='2024-06-30'/>,
    title: 'Temporadas',
    HiddenInHeader: false
  },
  {
    path: '/session0',
    element: <Sessions title='0' since='2020-01-01' until='2023-12-31'/>,
    title: 'Temporada 0',
    HiddenInHeader: true
  },
  {
    path: '/session1',
    element: <Sessions title='1' since='2024-01-01' until='2024-03-31'/>,
    title: 'Temporada 1',
    HiddenInHeader: true
  },
  {
    path: '/session2',
    element: <Sessions title='atual' since='2024-04-01' until='2024-06-30'/>,
    title: 'Temporada 2',
    HiddenInHeader: true
  }
];

export default AppRoutes;