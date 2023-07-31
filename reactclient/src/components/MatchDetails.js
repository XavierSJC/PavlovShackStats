import React, { Component } from 'react';

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
    return (
      <table className="table table-striped" aria-labelledby="tableLabel">
        <thead>
          <tr>
            <th colSpan="8">{nameTeam}</th>
          </tr>
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
          {matchDetails.map(match =>
            <tr key={match.name}>
              <td>{match.name}</td>
              <td>{match.kill}</td>
              <td>{match.death}</td>
              <td>{match.asssistant}</td>
              <td>{match.headshot}</td>
              <td>{match.bombDefused}</td>
              <td>{match.bombPlanted}</td>
              <td>{match.teamKill}</td>
            </tr>
          )}
        </tbody>
      </table>
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
    const response = await fetch('https://localhost:32768/api/PavlovShackStats/TeamsMatch?matchId='+this.props.matchId);
    const data = await response.json();
    this.setState({ matchDetails: data, loading: false });
  }
}
