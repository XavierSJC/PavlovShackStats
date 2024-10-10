import React from 'react';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import LiveMatch from '../components/LiveMach'

export default function Home() {
  return (
    <Box>
      <Typography variant='h3'>
        Bem-vindo a página do servidor [ CPX BRASIL ] - Pavlov 24Hras
        <br />
      </Typography>
        <Typography paragraph></Typography>
        <Typography paragraph>
          Olá, jogou recentemente do servidor [ CPX BRASIL ] - Pavlov 24Hras?
        </Typography>
        <Typography paragraph>
          Veja aqui quem está online e o atual mapa em execução.
        </Typography>

      <LiveMatch />
    </Box>
  );
}