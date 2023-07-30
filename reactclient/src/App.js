import './App.css';
import AppRoutes from "./routes";

import { BrowserRouter, Routes, Route } from "react-router-dom";

export default function App() {
  return (
      <BrowserRouter>
        <Routes>
          {AppRoutes.map((route, index) => {
            const { element, ...rest } = route;
            return <Route key={index} {...rest} element={element} />;
          })}
        </Routes>
      </BrowserRouter>
  );
}


