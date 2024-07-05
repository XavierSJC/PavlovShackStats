import React, { Component } from 'react';
import Constants from '../utilities/Constants'
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Typography from '@mui/material/Typography';
import Paper from '@mui/material/Paper';
import { FaSkull } from "react-icons/fa";

function maxRound(scoreTeam0, scoreTeam1) {
  if (scoreTeam0 > scoreTeam1) {
    return ((10 - scoreTeam0) + scoreTeam0 + scoreTeam1)
  }
  else {
    return ((10 - scoreTeam1) + scoreTeam0 + scoreTeam1)
  }
}

export default class LiveMatch extends Component {
  static displayName = LiveMatch.name;

  constructor(props) {
    super(props);
    this.state = { 
        serverOnline: false,
        matchDetails: [], 
        loading: true
    };
  }

  componentDidMount() {
    this.getLiveMatchDetails();
  }

  static renderMatchDetails(matchDetails) {
    let isTeam0Bigger = matchDetails.team0.length > matchDetails.team1.length;

    return (
      <TableContainer component={Paper}>
        <Table aria-label="Informações sobre a partida atual">
          <TableHead>
            <TableCell style={{ textAlign: 'left' }}>Mapa: {matchDetails.mapName}</TableCell>
            <TableCell/>
            <TableCell/>
            <TableCell/>
            <TableCell/>
            <TableCell/>
            <TableCell style={{ textAlign: 'right' }}>Jogadores: {matchDetails.playerCount}</TableCell>
          </TableHead>
          <TableHead>
              <TableCell style={{ backgroundColor: '#ee605d', textAlign: 'right' }}>Pontuação: {matchDetails.scoreTeam0}</TableCell>
              <TableCell style={{ backgroundColor: '#ee605d' }}>K/D/A</TableCell>
              <TableCell style={{ backgroundColor: '#ee605d' }}>Cash</TableCell>
              <TableCell style={{ textAlign: 'center'}}>Round {matchDetails.scoreTeam0+matchDetails.scoreTeam1+1}/{maxRound(matchDetails.scoreTeam0, matchDetails.scoreTeam1)}</TableCell>
              <TableCell style={{ backgroundColor: '#008fd7' }}>Pontuação: {matchDetails.scoreTeam1}</TableCell>
              <TableCell style={{ backgroundColor: '#008fd7' }}>K/D/A</TableCell>
              <TableCell style={{ backgroundColor: '#008fd7' }}>Cash</TableCell>
          </TableHead>
          <TableBody>
            { isTeam0Bigger ? 
              matchDetails.team0.map((player, index) =>
              <TableRow key={index}>
                <TableCell style={{ textAlign: 'right'}}>{player.dead ? <FaSkull /> : ''} {player.playerName}</TableCell>
                <TableCell>{player.kda}</TableCell>
                <TableCell>{player.cash}</TableCell>
                <TableCell/>
                <TableCell>{matchDetails.team1[index]?.playerName} {matchDetails.team1[index]?.dead ? <FaSkull /> : ''}</TableCell>
                <TableCell>{matchDetails.team1[index]?.kda}</TableCell>
                <TableCell>{matchDetails.team1[index]?.cash}</TableCell>
              </TableRow>)
              : matchDetails.team1.map((player, index) =>
              <TableRow key={index}>
                <TableCell style={{ textAlign: 'right'}}>{matchDetails.team0[index]?.dead ? <FaSkull /> : ''} {matchDetails.team0[index]?.playerName}</TableCell>
                <TableCell>{matchDetails.team0[index]?.kda}</TableCell>
                <TableCell>{matchDetails.team0[index]?.cash}</TableCell>
                <TableCell/>
                <TableCell>{player.playerName} {player.dead ? <FaSkull /> : ''}</TableCell>
                <TableCell>{player.kda}</TableCell>
                <TableCell>{player.cash}</TableCell>
              </TableRow>
            )}
          </TableBody>
        </Table>
      </TableContainer>
    )
  }

  render() {
    if (this.state.loading) {
        return (
            <div>
                <Typography paragraph>Carregando o status atual do servidor</Typography>
            </div>
        )
    }

    if (this.state.serverOnline !== true) {
        return (
            <div>
                <Typography paragraph>Atualmente o servidor está DESLIGADO, consulte nosso grupo do Whats para ver o horário que ficara disponível.</Typography>
            </div>
        )
    }

    let matchDetailsTable = LiveMatch.renderMatchDetails(this.state.matchDetails);
    return (
      <div>
        <Typography paragraph>O servidor atualmente está LIGADO, acompanhe a partida abaixo</Typography>
        {matchDetailsTable}
      </div>
    );
  }

  async getLiveMatchDetails() {
    await fetch(Constants.API_URL_GET_STATUS_PAVLOV_SERVER)
      .then((response) => {
        if (response.ok) {
          return response.json();
        }
      })
      .then ((json) => {
        this.state.isServerPavlovOnline = json;
      })
      .catch((error) => {
        console.log("Error to fetch API server '", Constants.API_URL_GET_STATUS_PAVLOV_SERVER, "': ", error);
        this.state.isServerPavlovOnline = false;
      });

    if (this.state.isServerPavlovOnline !== true)
    {
        this.setState({ loading: false });
        return;
    }
    await fetch(Constants.API_URL_GET_LIVE_MATCH)
      .then((response) => {
        if (response.ok) {
          return response.json();
        }
      })
      .then ((json) => {
        this.setState({ matchDetails: json, loading: false, serverOnline: true });
      })
      .catch((error) => {
        console.log("Error to fetch API server '", Constants.API_URL_GET_STATUS_PAVLOV_SERVER, "': ", error);
      });
  }
}
