import React, { Component } from 'react';
import Constants from '../utilities/Constants';
import { DataGrid } from '@mui/x-data-grid';

const columns = [
  {field: 'name', headerName: 'Nome', width: 200,
    renderCell: (params) => {
      if (params.row.profile_url == null)
        return params.row.name;
      return <a href={params.row.profile_url}>{params.row.name}</a>;
    },
  },
  {field: 'mode', headerName: 'Modo', width: 50},
  {field: 'logo', headerName: 'Imagem', width: 350,
    renderCell: (params) => {
      if(params.row.logo == null){
        return '';
      }
      return <img width="350" height="250" src={params.row.logo.original}/>;
    }
  },
  {field: 'summary', headerName: 'Descrição', width: 200},
  {field: 'author', headerName: 'Criador', width: 300, 
    renderCell: (params) => {
      if (params.row.submitted_by == null)
        return '';
      return params.row.submitted_by.username;
    }
  },
  {field: 'available', headerName: 'Status', 
    renderCell: (params) => {
      if (params.row.status == null)
        return 'OK';
      return (params.row.status != 1 || params.row.visible !=1) ? 'Problema encontrado' : 'OK';
    }
  }
]

export default class MapListTable extends Component {
  static displayName = MapListTable.name;

  constructor(props) {
    super(props);
    this.state = { mapListDetails: [], loading: true };
  }

  componentDidMount() {
    this.populateMapListData();
  }

  render() {
    let mapListTable = this.state.loading ?
      <p><em>Carregando...</em></p> :
      MapListTable.DataTable(this.state.mapListDetails);

    return (
      <div>
        <p>
        Lista de mapas disponíveis no servidor, ordenação corresponde a rotação do servidor. 
        </p>
          {mapListTable}
      </div>
    );
  }

  async populateMapListData() {
    await fetch(Constants.API_URL_GET_MAP_LIST)
      .then((response) => {
        if (response.ok) {
          return response.json();
        }
      })
      .then ((json) => {
        this.setState({ mapListDetails: json, loading: false });
      })
      .catch((error) => {
        console.log("Error to fetch API server '", Constants.API_URL_GET_MAP_LIST, "': ", error);
      });
  }

  static DataTable(mapList) {
    return (
      <div style={{width: '100%'}}>
        <DataGrid
          style={{width: '100%'}}
          rows={mapList}
          columns={columns}
          getRowId={(row) => row.name}
          getRowHeight={() => 'auto'}
        />
      </div>
    )
  }
}
