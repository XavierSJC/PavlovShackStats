import React, { Component } from 'react';
import Constants from '../utilities/Constants';
import { DataGrid } from '@mui/x-data-grid';

function getScore(kill, death, assist, hs, bd, bp, tk) {
  return kill*2 + 
    death*-2 + 
    assist + 
    hs + 
    bd*3 + 
    bp*2 +
    tk*-5
}

const columns = [
  {field: 'playerName', headerName: 'Jogador'},
  {field: 'kills', headerName: 'Kills', type: 'number'},
  {field: 'death', headerName: 'Deaths', type: 'number'},
  {field: 'assist', headerName: 'Assistants', type: 'number'},
  {field: 'headShot', headerName: 'HeadShots', type: 'number'},
  {field: 'bombDefused', headerName: 'Bombas Desarmadas', type: 'number'},
  {field: 'bombPlanted', headerName: 'Bombas Plantadas', type: 'number'},
  {field: 'teamKill', headerName: 'Fogo Amigo', type: 'number'},
  {field: 'numMatches', headerName: 'Partidas Jogadas', type: 'number'},
  {
    field: 'score', 
    headerName: 'Pontuação', 
    type: 'number',
    valueGetter: (params) => 
      getScore(params.row.kills,
        params.row.death,
        params.row.assist,
        params.row.headShot,
        params.row.bombDefused,
        params.row.bombPlanted,
        params.row.teamKill,
      )
  },
  {
    field: 'KDA',
    headerName: 'KDA',
    type: 'number',
    valueGetter: (params) =>
      (params.row.kills + params.row.assist)/params.row.death
  },
  {
    field: 'ScoreAverage',
    headerName: 'P. Média',
    type: 'number',
    valueGetter: (params) =>
      getScore(params.row.kills,
        params.row.death,
        params.row.assist,
        params.row.headShot,
        params.row.bombDefused,
        params.row.bombPlanted,
        params.row.teamKill,
      ) / (params.row.numMatches < 30 ? 999999 : params.row.numMatches)
  }
]

export default class PlayerStatsTable extends Component {
  static displayName = PlayerStatsTable.name;

  constructor(props) {
    super(props);
    this.state = { playersStats: [], loading: true };
  }

  componentDidMount() {
    this.populatePlayersStatsData();
  }

  render() {
    let playersStatsTable = this.state.loading ?
      <p><em>Carregando...</em></p> :
      PlayerStatsTable.DataTable(this.state.playersStats);

    return (
      <div>
        Dados acumulativos 
        {this.props.since ? ' desde '+ this.props.since : ''}
        {this.props.until ? ' até '+ this.props.until : ''}
        {playersStatsTable}
      </div>
    );
  }

  async populatePlayersStatsData() {
    const searchParams = new URLSearchParams();
    if(this.props.since)
      searchParams.append("sinceDate", this.props.since);
    if(this.props.until)
      searchParams.append("untilDate", this.props.until);
    if (this.props.gameMode)
      searchParams.append("gameMode", this.props.gameMode);

    await fetch(Constants.API_URL_GET_PLAYERS_STATS.concat('?', searchParams.toString()))
      .then((response) => {
        if (response.ok) {
          return response.json();
        }
      })
      .then((json) => {
        this.setState({ playersStats: json, loading: false });
      })
      .catch((error) => {
        console.log("Error to fetch API server '", Constants.API_URL_GET_PLAYERS_STATS, "': ", error);
      });
  }

  static DataTable(playersStats) {
    return (
      <div style={{width: '100%'}}>
        <DataGrid
          style={{width: '100%'}}
          rows={playersStats}
          columns={columns}
          getRowId={(row) => row.playerName}
          initialState={{
            pagination: {
              paginationModel: { page: 0, pageSize: 25 },
            },
            sorting: {
              sortModel: [{field: 'score', sort: 'desc'}]
            }
          }}
          pageSizeOptions={[25, 50, 100]}
        />
      </div>
    )
  }
}
