import React from 'react';
import PlayersStatsTable from '../components/PlayerStatsTable'

export default function Matches() {
  return (
    <div>
      <h1>Estatisticas de cada jogador</h1>
      Dados acumulativos
      <PlayersStatsTable />
    </div>
  );
}