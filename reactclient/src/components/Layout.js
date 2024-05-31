import React from 'react';
import './Layout.css';
import Link from '@mui/material/Link';
import { Box } from '@mui/material';
import Stack from '@mui/material/Stack';
import AppRoutes from '../routes'
import Typography from '@mui/material/Typography';
import YouTubeIcon from '@mui/icons-material/YouTube';
import FacebookIcon from '@mui/icons-material/Facebook';
import InstagramIcon from '@mui/icons-material/Instagram';
import TikTokIcon from '../utilities/TikTokIcon';
import TwitchIcon from '../utilities/TwitchIcon';

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
                                <Link href={route.path} underline='hover' hidden={route.HiddenInHeader}>{route.title}</Link>
                            );
                        })}
                    </Stack>
                </nav>
            </header>
            <main>
                {children}
            </main>
            <footer>
                <Typography paragraph>Não esqueça de nos seguir nas redes sociais: 
                    <a href='https://www.youtube.com/@kinu_dw'><YouTubeIcon /></a>
                    <a href='https://www.tiktok.com/@kinu_dw'><TikTokIcon width='22px'/></a>
                    <a href='https://www.twitch.tv/kinu_dw'><TwitchIcon width='22px'/></a>
                    <a href='https://www.facebook.com/kinudw'><FacebookIcon/></a>
                    <a href='https://www.instagram.com/kinu_dw'><InstagramIcon/></a>
                </Typography>

            </footer>
        </Box>
    )
}

export default Layout;