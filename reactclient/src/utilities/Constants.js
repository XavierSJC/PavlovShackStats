const API_BASE_URL_DEVELOPMENT = 'https://localhost:32768';
const API_BASE_URL_PRODUCTION = 'https://appname.domainname.net';

const ENDPOINTS = {
    GET_PLAYERS_STATS: 'api/PavlovShackStats/PlayersStats',
    GET_MATCHES: 'api/PavlovShackStats/GetMatches',
    GET_MATCH_DETAILS: 'api/PavlovShackStats/TeamsMatch'
};

const development = {
    API_URL_GET_PLAYERS_STATS: `${API_BASE_URL_DEVELOPMENT}/${ENDPOINTS.GET_PLAYERS_STATS}`,
    API_URL_GET_MATCHES: `${API_BASE_URL_DEVELOPMENT}/${ENDPOINTS.GET_MATCHES}`,
    API_URL_GET_MATCH_DETAILS: `${API_BASE_URL_DEVELOPMENT}/${ENDPOINTS.GET_MATCH_DETAILS}`
}

const production = {
    API_URL_GET_PLAYERS_STATS: `${API_BASE_URL_PRODUCTION}/${ENDPOINTS.GET_PLAYERS_STATS}`,
    API_URL_GET_MATCHES: `${API_BASE_URL_PRODUCTION}/${ENDPOINTS.GET_MATCHES}`,
    API_URL_GET_MATCH_DETAILS: `${API_BASE_URL_PRODUCTION}/${ENDPOINTS.GET_MATCH_DETAILS}`
}

const Constants = process.env.NODE_ENV === 'development' ? development : production;

export default Constants;