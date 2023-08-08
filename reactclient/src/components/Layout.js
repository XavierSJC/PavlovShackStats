import React from 'react';
import Link from '@mui/material/Link';
import { Box } from '@mui/material';
import Stack from '@mui/material/Stack';
import AppRoutes from '../routes'

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
                        {AppRoutes.map((route) => {
                            return (
                                <Link href={route.path} underline='hover'>{route.title}</Link>
                            );
                        })}
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