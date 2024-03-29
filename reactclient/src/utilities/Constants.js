const API_BASE_URL_DEVELOPMENT = window._env_.API_URL_DEV;
const API_BASE_URL_PRODUCTION = window._env_.API_URL;

const ENDPOINTS = {
    GET_PLAYERS_STATS: 'api/PavlovShackStats/PlayersStats',
    GET_MATCHES: 'api/PavlovShackStats/GetMatches',
    GET_MATCH_DETAILS: 'api/PavlovShackStats/TeamsMatch',
    GET_STATUS_PAVLOV_SERVER: 'api/GameStatus/IsOnline',
    GET_LIVE_MATCH: 'api/GameStatus/ServerInfo'
};

const development = {
    API_URL_GET_PLAYERS_STATS: `${API_BASE_URL_DEVELOPMENT}/${ENDPOINTS.GET_PLAYERS_STATS}`,
    API_URL_GET_MATCHES: `${API_BASE_URL_DEVELOPMENT}/${ENDPOINTS.GET_MATCHES}`,
    API_URL_GET_MATCH_DETAILS: `${API_BASE_URL_DEVELOPMENT}/${ENDPOINTS.GET_MATCH_DETAILS}`,
    API_URL_GET_STATUS_PAVLOV_SERVER: `${API_BASE_URL_DEVELOPMENT}/${ENDPOINTS.GET_STATUS_PAVLOV_SERVER}`,
    API_URL_GET_LIVE_MATCH: `${API_BASE_URL_DEVELOPMENT}/${ENDPOINTS.GET_LIVE_MATCH}`
}

const production = {
    API_URL_GET_PLAYERS_STATS: `${API_BASE_URL_PRODUCTION}/${ENDPOINTS.GET_PLAYERS_STATS}`,
    API_URL_GET_MATCHES: `${API_BASE_URL_PRODUCTION}/${ENDPOINTS.GET_MATCHES}`,
    API_URL_GET_MATCH_DETAILS: `${API_BASE_URL_PRODUCTION}/${ENDPOINTS.GET_MATCH_DETAILS}`,
    API_URL_GET_STATUS_PAVLOV_SERVER: `${API_BASE_URL_PRODUCTION}/${ENDPOINTS.GET_STATUS_PAVLOV_SERVER}`,
    API_URL_GET_LIVE_MATCH: `${API_BASE_URL_PRODUCTION}/${ENDPOINTS.GET_LIVE_MATCH}`
}

const Constants = process.env.NODE_ENV === 'development' ? development : production;

export default Constants;