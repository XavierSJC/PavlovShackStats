import React, { Component } from 'react';
import Constants from '../utilities/Constants'
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';

export default class MatchDetailsTable extends Component {
  static displayName = MatchDetailsTable.name;

  constructor(props) {
    super(props);
    this.state = { matchDetails: [], loading: true };
  }

  componentDidMount() {
    this.populateMatchDetails();
  }

  static renderMatchDetails(nameTeam, matchDetails) {
    let colorHeader = nameTeam === 'Time 1' ? '#ee605d' : '#008fd7'
    return (
      <TableContainer component={Paper}>
        <Table aria-label="Estatistica dos jogadores em um time">
          <TableHead>
            <TableRow>
              <TableCell style={{ backgroundColor: colorHeader }}
                colSpan="8">{nameTeam}</TableCell>
            </TableRow>
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
            {matchDetails.map(match =>
              <TableRow key={match.name}>
                <TableCell>{match.name}</TableCell>
                <TableCell align="right">{match.kill}</TableCell>
                <TableCell align="right">{match.death}</TableCell>
                <TableCell align="right">{match.asssistant}</TableCell>
                <TableCell align="right">{match.headshot}</TableCell>
                <TableCell align="right">{match.bombDefused}</TableCell>
                <TableCell align="right">{match.bombPlanted}</TableCell>
                <TableCell align="right">{match.teamKill}</TableCell>
              </TableRow>
            )}
          </TableBody>
        </Table>
      </TableContainer>
    )
  }

  render() {
    let matchDetailsTable1 = this.state.loading
      ? <p><em>Carregando...</em></p>
      : MatchDetailsTable.renderMatchDetails('Time 1', this.state.matchDetails.team0);
    let matchDetailsTable2 = this.state.loading
      ? <p><em>Carregando...</em></p>
      : MatchDetailsTable.renderMatchDetails('Time 2', this.state.matchDetails.team1);

    return (
      <div>
        {matchDetailsTable1}
        {matchDetailsTable2}
      </div>
    );
  }

  async populateMatchDetails() {
    await fetch(Constants.API_URL_GET_MATCH_DETAILS + '?matchId=' + this.props.matchId)
      .then((response) => {
        if (response.ok) {
          return response.json();
        }
      })
      .then ((json) => {
        this.setState({ matchDetails: json, loading: false });
      })
      .catch((error) => {
        console.log("Error to fetch API server '", Constants.API_URL_GET_MATCH_DETAILS, "': ", error);
      });
  }
}
