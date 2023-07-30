import React, { Component } from 'react';

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
      <table className="table table-striped" aria-labelledby="tableLabel">
        <thead>
          <tr>
            <th>Jogador</th>
            <th>Kills</th>
            <th>Deaths</th>
            <th>Assists</th>
            <th>HeadShots</th>
            <th>Bombas Desarmadas</th>
            <th>Bombas Plantadas</th>
            <th>Fogo amigo</th>
          </tr>
        </thead>
        <tbody>
          {playersStats.map(playerStat =>
              <tr key={playerStat.playerName}>
                  <td>{playerStat.playerName}</td>
                  <td>{playerStat.kills}</td>
                  <td>{playerStat.death}</td>
                  <td>{playerStat.assist}</td>
                  <td>{playerStat.headShot}</td>
                  <td>{playerStat.bombDefused}</td>
                  <td>{playerStat.bombPlanted}</td>
                  <td>{playerStat.teamKill}</td>
            </tr>
          )}
        </tbody>
      </table>
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
