import React from 'react';
import MapList from '../components/MapList'
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';

export default function Matches() {
  return (
    <Box>
      <Typography variant='h3'>
        Mapas
      </Typography>
      <MapList />
    </Box>
  );
}