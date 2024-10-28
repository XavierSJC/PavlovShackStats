# Pavlov VR Stats API
This API receives files from a Pavlov VR game server, stores the values in a MySQL database and provides these value to fetch by a API.

You can use this API for check status server OR stores matches information OR **both**.

## RCON
This API can access the the RCON of the server to provide updated status of the server, so far this project only provides read-only data, in the future after improvement the security of the project will be possible manager the server too.

## Mod.io (WIP)
This API can access the mod.io API to provides informations about the rotation map of the server, mandatory 

## Docker Image
### Environment Variables
#### Database Connection
This information is necessary if you wish build a database with the matches played in you server.

|Variable| Description |
| - | - |
|**ConnectionStrings__DefaultConnection**| Connection string to access the MySQL database. Mandatory if you wish save the matches data |

#### RCON Connection
This information is necessary if you wish to this API provides status of the server.

|Variable| Description |
| - | - |
|**RconSettings__ipAddress**| IP of you game server |
|**RconSettings__port**| RCON port |
|**RconSettings__password**| RCON password |

#### Mod.io Connection
You can get this data from [mod.io](https://docs.mod.io/restapiref/) documetantion.
Please check out the Authentication info values from [here](https://docs.mod.io/restapiref/#authentication)

|Variable| Description |
| - | - |
|**Mod.io__ApiPath**| API path to access mod.io |
|**Mod.io__ApiKey**| API key to acess mod.io |


### Build
```
docker build -t <image name> -f ./WebApplication1/Dockerfile ./
```

This project is compatible with ARM architecture, please use the command below to build a project target to ARM.
```
docker build -t <image name> --platform linux/arm64 -f ./WebApplication1/Dockerfile ./
```