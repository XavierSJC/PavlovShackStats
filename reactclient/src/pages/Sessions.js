import React, { Component } from 'react';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemText from '@mui/material/ListItemText';
import ListItemButton from '@mui/material/ListItemButton';
import PlayerStatsTable from '../components/PlayerStatsTable';

export default class Sessions extends Component {
  static displayName = Sessions.name;

  render() {
    return (
      <Box>
        <Typography variant='h3'>
          Temporadas
        </Typography>
        <Typography>
          Para as temporadas é contabilizado apenas os mapas do modo SND (Armar/Desarmar bombas)
        </Typography>
        <br/>
        <Typography variant='h4'>
          Temporada {this.props.title} (desde {this.props.since} até {this.props.until}):
        </Typography>
        <PlayerStatsTable since={this.props.since} until={this.props.until} gameMode='SND'/>
        <br/><br/><br/>
        <Typography variant='h4'>
          Temporadas anteriores:
        </Typography>
        <Typography>
          <List sx={{
            listStyleType: 'disc',
            pl: 3,
            '& .MuiListItem-root': {
              display: 'list-item',
            }
          }}>
            <ListItem>
            <ListItemButton component="a" href="/session0">
                <ListItemText primary="Temporada 0" />
              </ListItemButton>
              <ListItemButton component="a" href="/session1">
                <ListItemText primary="Temporada 1" />
              </ListItemButton>
              <ListItemButton component="a" href="/session2">
                <ListItemText primary="Temporada 2" />
              </ListItemButton>
            </ListItem>
          </List>
        </Typography>
        <br/><br/><br/>
        <Typography>
          Caso queira ver a estatistica de todas as temporadas somadas <a href='/playerstats'>consulte aqui!</a>.
        </Typography>
        <br/><br/><br/>
      </Box>
    );
  }
}
