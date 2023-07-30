import React from 'react';
import { Link } from 'react-router-dom';

export default function Home() {
  return (
    <div>
      <h1>Bem-vindo a página do servidor PowerSJK</h1>
      <p>Olá, jogou recentemente do servidor [BR]PowerSJK?</p>
      <p>Veja aqui como foi seu rendimento comparado a outros jogadores</p>
      <nav>
        <ul>
          <li>
            <Link to="/playerstats">Jogadores</Link>
          </li>
          <li>
            <Link to="/matches">Últimas partidas</Link>
          </li>
        </ul>
      </nav>
    </div>
  );
}