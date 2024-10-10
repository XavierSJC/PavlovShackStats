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
    HiddenInHeader: false
  }
];

export default AppRoutes;