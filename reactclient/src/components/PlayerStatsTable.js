import React, { Component } from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';

export default class PlayerStatsTable extends Component {
  static displayName = PlayerStatsTable.name;

  constructor(props) {
    super(props);
    this.state = { playersStats: [], loading: true };
  }

  componentDidMount() {
    this.populatePlayersStatsData();
  }

  static renderPlayersStats(playersStats) {
    return (
      <TableContainer component={Paper}>
        <Table aria-label="Estatistica acumulada dos jogadores">
          <TableHead>
            <TableRow>
              <TableCell>Jogador</TableCell>
              <TableCell>Kills</TableCell>
              <TableCell>Deaths</TableCell>
              <TableCell>Assists</TableCell>
              <TableCell>HeadShots</TableCell>
              <TableCell>Bombas Desarmadas</TableCell>
              <TableCell>Bombas Plantadas</TableCell>
              <TableCell>Fogo amigo</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {playersStats.map(playerStat =>
              <TableRow
                key={playerStat.playerName}
                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
              >
                <TableCell>{playerStat.playerName}</TableCell>
                <TableCell align="right">{playerStat.kills}</TableCell>
                <TableCell align="right">{playerStat.death}</TableCell>
                <TableCell align="right">{playerStat.assist}</TableCell>
                <TableCell align="right">{playerStat.headShot}</TableCell>
                <TableCell align="right">{playerStat.bombDefused}</TableCell>
                <TableCell align="right">{playerStat.bombPlanted}</TableCell>
                <TableCell align="right">{playerStat.teamKill}</TableCell>
              </TableRow>
            )}
          </TableBody>
        </Table >
      </TableContainer>
    )
  }

  render() {
    let playersStatsTable = this.state.loading
      ? <p><em>Carregando...</em></p>
      : PlayerStatsTable.renderPlayersStats(this.state.playersStats);

    return (
      <div>
        {playersStatsTable}
      </div>
    );
  }

  async populatePlayersStatsData() {
    const response = await fetch('https://localhost:32768/api/PavlovShackStats/PlayersStats');
    const data = await response.json();
    this.setState({ playersStats: data, loading: false });
  }
}
