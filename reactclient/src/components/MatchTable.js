import React, { Component } from 'react';
import MatchDetails from '../components/MatchDetails'
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import IconButton from '@mui/material/IconButton';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';

export default class MatchTable extends Component {
  static displayName = MatchTable.name;

  constructor(props) {
    super(props);
    this.state = {
      matches: [],
      loading: true,
      expandedMatch: []
    };
  }

  SetExpandedMatch(index) {
    let newExpandedMatch = this.state.expandedMatch.slice();
    newExpandedMatch[index] = !newExpandedMatch[index];
    this.setState({ expandedMatch: newExpandedMatch });
  }

  componentDidMount() {
    this.populateMatchData();
  }

  renderMatches(matches, expandedMatch) {
    return (
      <TableContainer component={Paper}>
        <Table stripedRows aria-labelledby="tableLabel">
          <TableHead>
            <TableRow>
              <TableCell></TableCell>
              <TableCell>Mapa</TableCell>
              <TableCell>Modo</TableCell>
              <TableCell>Placar time 1</TableCell>
              <TableCell>Placar time 2</TableCell>
              <TableCell>Total de Jogadores</TableCell>
              <TableCell>Data e hora</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {matches.map((match, index) =>
              <>
                <TableRow key={match.matchId}>
                  <TableCell>
                    <IconButton
                      aria-label="expand row"
                      size="small"
                      onClick={() => this.SetExpandedMatch(index)}>
                      {
                        expandedMatch[index] ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />
                      }
                    </IconButton>
                  </TableCell>
                  <TableCell>{match.mapName}</TableCell>
                  <TableCell>{match.gameMode}</TableCell>
                  <TableCell>{match.team0Score}</TableCell>
                  <TableCell>{match.team1Score}</TableCell>
                  <TableCell>{match.playersMatch}</TableCell>
                  <TableCell>{this.convertTimeZone(match.finishedTime)}</TableCell>
                </TableRow>
                {this.renderTeamsMatch(index, match.matchId)}
              </>
            )}
          </TableBody>
        </Table>
      </TableContainer>
    )
  }

  render() {
    let matchTable = this.state.loading
      ? <p><em>Carregando...</em></p>
      : this.renderMatches(this.state.matches,
        this.state.expandedMatch);

    return (
      <div>
        {matchTable}
      </div>
    );
  }

  async populateMatchData() {
    const response = await fetch('https://localhost:32768/api/PavlovShackStats/GetMatches?count=50');
    const data = await response.json();
    this.setState({ matches: data, loading: false });
    this.setState({ expandedMatch: Array(data.lenght).fill(false) })
  }

  renderTeamsMatch(index, matchId) {
    let showTable = this.state.expandedMatch[index]
      ? <MatchDetails matchId={matchId} />
      : <></>
    return (
      <TableRow>
        <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={7}>
          {showTable}
        </TableCell>
      </TableRow>
    )
  }

  convertTimeZone(strDateTime) {
    let dateTime = new Date(strDateTime);
    dateTime.setHours(dateTime.getHours() - 3);

    return dateTime.toLocaleString("UTC", "pt-BR");
  }
}
