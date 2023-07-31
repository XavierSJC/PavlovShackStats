import React, { Component } from 'react';
import MatchDetails from '../components/MatchDetails'

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
      <table className="table table-striped" aria-labelledby="tableLabel">
        <thead>
          <tr>
            <th>#</th>
            <th>Mapa</th>
            <th>Modo</th>
            <th>Placar time 1</th>
            <th>Placar time 2</th>
            <th>Total de Jogadores</th>
            <th>Data e hora</th>
          </tr>
        </thead>
        <tbody>
          {matches.map((match, index) =>
            <>
              <tr key={match.matchId}>
                <td onClick={() => this.SetExpandedMatch(index)}>
                  <button>
                    {
                      expandedMatch[index] ? '-' : '+'
                    }
                  </button>
                </td>
                <td>{match.mapName}</td>
                <td>{match.gameMode}</td>
                <td>{match.team0Score}</td>
                <td>{match.team1Score}</td>
                <td>{match.playersMatch}</td>
                <td>{match.finishedTime}</td>
              </tr>
              {this.renderTeamsMatch(index, match.matchId)}
            </>
          )}
        </tbody>
      </table>
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
    const response = await fetch('https://localhost:32768/api/PavlovShackStats/GetMatches');
    const data = await response.json();
    this.setState({ matches: data, loading: false });
    this.setState({ expandedMatch: Array(data.lenght).fill(false) })
  }

  renderTeamsMatch(index, matchId) {
    let showTable = this.state.expandedMatch[index]
      ? <MatchDetails matchId={matchId} />
      : <></>
    return (
      <tr>
        <td colSpan="7">
          {showTable}
        </td>
      </tr>
    )
  }
}
