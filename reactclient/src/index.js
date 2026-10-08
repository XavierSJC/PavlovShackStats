import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import Layout from './components/Layout';
import CssBaseline from '@mui/material/CssBaseline';
import { createTheme, ThemeProvider } from '@mui/material/styles';

const theme = createTheme({
  palette: {
    mode: 'dark',
    background: {
      default: '#222222'
    },
    text: {
      primary: "#ffffff"
    }
  }
});

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.Fragment>
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <Layout>
        <App />
      </Layout>
    </ThemeProvider>
  </React.Fragment>
);

