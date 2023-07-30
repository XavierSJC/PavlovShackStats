import React, { Component } from 'react';

export default class MatchTable extends Component {
  static displayName = MatchTable.name;

  constructor(props) {
    super(props);
      this.state = { matches: [], loading: true };
  }

  componentDidMount() {
      this.populateMatchData();
  }

  static renderMatches(matches) {
    return (
      <table className="table table-striped" aria-labelledby="tableLabel">
        <thead>
          <tr>
            <th>Mapa</th>
            <th>Modo</th>
            <th>Placar time 1</th>
            <th>Placar time 2</th>
            <th>Total de Jogadores</th>
            <th>Data e hora</th>
          </tr>
        </thead>
        <tbody>
          {matches.map(match =>
              <tr key={match.matchId}>
                  <td>{match.mapName}</td>
                  <td>{match.gameMode}</td>
                  <td>{match.team0Score}</td>
                  <td>{match.team1Score}</td>
                  <td>{match.playersMatch}</td>
                  <td>{match.finishedTime}</td>
            </tr>
          )}
        </tbody>
      </table>
    )
  }

  render() {
      let matchTable = this.state.loading
          ? <p><em>Carregando...</em></p>
          : MatchTable.renderMatches(this.state.matches);

    return (
      <div>
        {matchTable}
      </div>
    );
  }

  async populateMatchData() {
    const response = await fetch('https://localhost:32768/api/PavlovShackStats/GetMatches');
    const data = await response.json();
    this.setState({ matches: data, loading: false });
  }
}
