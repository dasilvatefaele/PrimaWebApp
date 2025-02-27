# PrimaWebApp (nuovo tentativo)

> Passaggi eseguiti: 
- <u>Creato nuovo repository</u>
- <u>Creato archetipo .NET WebApp di default</u>
- <u>dotnet build</u>
- <u>dotnet run</u>

```

---

* 1 - in `launchSettings.json` sotto `profiles` modificare la porta

```json
"profiles": {
    "http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "applicationUrl": "http://localhost:8080",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
```

* 2 - Creare DockerFile

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY *.csproj ./

RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out --no-restore

# Fase di runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build app/out ./
EXPOSE 8080
ENTRYPOINT ["dotnet", "PrimaWebApp.dll"]
```

* 3 - Build via Docker:

```powershell
docker build -t primawebapp .
```

* 4 - Terminale
> docker login
```powershell
PS C:\Users\39347\Documents\PrimaWebApp\PrimaWebApp> docker login
Authenticating with existing credentials...
Login Succeeded
```
> docker build -t primawebapp .
```powershell
PS C:\Users\39347\Documents\PrimaWebApp\PrimaWebApp> docker build -t primawebapp .
[+] Building 7.9s (15/15) FINISHED                                                                                                                                                      docker:desktop-linux 
 => [internal] load build definition from Dockerfile                                                                                                                                                    0.0s 
 => => transferring dockerfile: 395B                                                                                                                                                                    0.0s 
 => [internal] load metadata for mcr.microsoft.com/dotnet/sdk:8.0                                                                                                                                       0.5s 
 => [internal] load .dockerignore                                                                                                                                                                       0.1s 
 => => transferring context: 2B                                                                                                                                                                         0.0s 
 => [build 1/6] FROM mcr.microsoft.com/dotnet/sdk:8.0@sha256:483d6f3faa583c93d522c4ca9ee54e08e535cb112dceb252b2fbb7ef94839cc8                                                                           0.0s 
 => [runtime 1/3] FROM mcr.microsoft.com/dotnet/aspnet:8.0@sha256:e223bd5d93b3042215c7aed59568933631121f7ff4f5268a5092ab54a7e20136                                                                      0.0s 
 => [internal] load build context                                                                                                                                                                       0.1s 
 => => transferring context: 9.40kB                                                                                                                                                                     0.0s 
 => CACHED [runtime 2/3] WORKDIR /app                                                                                                                                                                   0.0s 
 => CACHED [build 2/6] WORKDIR /app                                                                                                                                                                     0.0s 
 => CACHED [build 3/6] COPY *.csproj ./                                                                                                                                                                 0.0s 
 => CACHED [build 4/6] RUN dotnet restore                                                                                                                                                               0.0s 
 => [build 5/6] COPY . ./                                                                                                                                                                               0.2s 
 => [build 6/6] RUN dotnet publish -c Release -o out --no-restore                                                                                                                                       6.4s 
 => [runtime 3/3] COPY --from=build app/out ./                                                                                                                                                          0.2s 
 => exporting to image                                                                                                                                                                                  0.2s 
 => => writing image sha256:3a3dac76a62912b0da6dd2535884224864feb2f3e677a65dc8bc60a26c724a8c                                                                                                            0.0s 
 => => naming to docker.io/library/primawebapp  
```
> docker run -p 8080:80 primawebapp
```powershell
 PS C:\Users\39347\Documents\PrimaWebApp\PrimaWebApp> docker run -p 8080:80 primawebapp
warn: Microsoft.AspNetCore.DataProtection.Repositories.FileSystemXmlRepository[60]
      Storing keys in a directory '/root/.aspnet/DataProtection-Keys' that may not be persisted outside of the container. Protected data will be unavailable when container is destroyed. For more information go to https://aka.ms/aspnet/dataprotectionwarning
warn: Microsoft.AspNetCore.DataProtection.KeyManagement.XmlKeyManager[35]
      No XML encryptor configured. Key {05dda779-287d-48bb-a6d5-1bf34a3f1163} may be persisted to storage in unencrypted form.
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://[::]:8080
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Production
info: Microsoft.Hosting.Lifetime[0]
      Content root path: /app
```

* 4 - Check su Docker Desktop 

### `Container e Immagini presenti su Docker Desktop (modalità Run)`

- 5 - Controllo sulla porta 8080 via browser via URL `http://localhost:8080/`

#### PROBLEMA: 
Messaggio del browser:

---
---

## Questa pagina non funziona in questo momento

### localhost non ha inviato dati.


###### ERR_EMPTY_RESPONSE

---
---

