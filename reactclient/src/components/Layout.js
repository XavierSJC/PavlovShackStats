import React from 'react';
import Link from '@mui/material/Link';
import { Box } from '@mui/material';
import Stack from '@mui/material/Stack';


const Layout = ({ children }) => {
    return (
        <Box>
            <header>
                <nav>
                    <Stack className='test' direction="row" spacing={2}
                        sx={
                            {
                                position: 'absolute',
                                right: 0,
                                marginRight: '10px',
                                top: '80px'
                            }}>
                        <item>
                            <Link href='/' underline="hover">Pagina inicial</Link>
                        </item>
                        <item>
                            <Link href='/matches' underline="hover">Últimas Partidas</Link>
                        </item>
                        <item>
                            <Link href='/playerstats' underline="hover">Jogadores</Link>
                        </item>
                    </Stack>
                </nav>
            </header>
            <main>
                {children}
            </main>
            <footer>
            </footer>
        </Box>
    )
}

export default Layout;