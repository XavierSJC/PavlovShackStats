import Home from "./pages/Home";
import Matches from "./pages/Matches";
import MapList from "./pages/MapList"
import Sessions from "./pages/Sessions"

const AppRoutes = [
  {
    index: true,
    path: '/',
    element: <Home />,
    title: 'Página inicial',
    HiddenInHeader: false
  },
  {
    path: '/matches',
    element: <Matches />,
    title: 'Últimas Partidas',
    HiddenInHeader: false
  },
  {
    path: '/mapList',
    element: <MapList />,
    title: 'Mapas',
    HiddenInHeader: false
  },
  {
    path: '/sessions',
    element: <Sessions title='atual' since='2022-10-01' until='2024-12-31' />,
    title: 'Temporadas',
    HiddenInHeader: false
  },
  {
    path: '/session4',
    element: <Sessions title='0' since='2024-10-01' until='2024-12-31' />,
    title: 'Temporada 1/4',
    HiddenInHeader: true
  }
];

export default AppRoutes;