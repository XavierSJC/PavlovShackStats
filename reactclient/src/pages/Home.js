import React from 'react';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import LiveMatch from '../components/LiveMach'

export default function Home() {
  return (
    <Box>
      <Typography variant='h3'>
        Bem-vindo a página do servidor BRASIL PAVLOV 24H
        <br />
      </Typography>
        <Typography paragraph></Typography>
        <Typography paragraph>
          Olá, jogou recentemente do servidor BRASIL PAVLOV 24H?
        </Typography>
        <Typography paragraph>
          Veja aqui seu ranking, quem está online, os maps disponíveis e o modo de jogo atual.
        </Typography>

      <LiveMatch />
    </Box>
  );
}