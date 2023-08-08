import React from 'react';
import MatchesTable from '../components/MatchTable'
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';

export default function Matches() {
  return (
    <Box>
      <Typography variant='h3'>
        Partidas
      </Typography>
      <MatchesTable />
    </Box>
  );
}