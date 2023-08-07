import React from 'react';
import PlayersStatsTable from '../components/PlayerStatsTable'
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';

export default function Matches() {
  return (
    <Box>
      <Typography variant='h3' paragraph="true">
        Estatisticas de cada jogador
      </Typography>
      <Typography>
        Dados acumulativos
      </Typography>
      <PlayersStatsTable />
    </Box>
  );
}