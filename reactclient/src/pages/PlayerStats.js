import React from 'react';
import PlayersStatsTable from '../components/PlayerStatsTable'
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';

export default function PlayersStats() {
  return (
    <Box>
      <Typography variant='h3'>
        Estatisticas de cada jogador
      </Typography>
      <Typography paragraph>
      </Typography>
      <PlayersStatsTable />
    </Box>
  );
}