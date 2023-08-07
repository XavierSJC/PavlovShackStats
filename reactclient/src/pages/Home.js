import React from 'react';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemText from '@mui/material/ListItemText';
import ListItemButton from '@mui/material/ListItemButton';

export default function Home() {
  return (
    <Box>
      <Typography variant='h3' paragraph="true">
        Bem-vindo a página do servidor [BR]PowerSJK
        <br />
      </Typography>
      <Typography>
        <p>Olá, jogou recentemente do servidor [BR]PowerSJK?</p>
        <p>Veja aqui como foi seu rendimento comparado a outros jogadores</p>
      </Typography>
      <br />
      
      <List sx={{
        listStyleType: 'disc',
        pl: 3,
        '& .MuiListItem-root': {
          display: 'list-item',
        }
      }}>
        <ListItem>
          <ListItemButton component="a" href="/playerstats">
            <ListItemText primary="Jogadores" />
          </ListItemButton>
        </ListItem>
        <ListItem>
          <ListItemButton component="a" href='/matches'>
            <ListItemText primary="Últimas Partidas" />
          </ListItemButton>
        </ListItem>
      </List>
    </Box>
  );
}