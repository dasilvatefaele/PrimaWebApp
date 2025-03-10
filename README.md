# PrimaWebApp (nuovo tentativo)

> Passaggi eseguiti: 
- <u>Creato nuovo repository</u>
- <u>Creato archetipo .NET WebApp di default</u>
- <u>dotnet build</u>
- <u>dotnet run</u>
- <u>Check http://localhost:8080 stato: `funzionante`</u>


---

# Procedimento per Docker

* 1 - in `launchSettings.json` sotto `profiles` modificare la porta

```json
"profiles": 
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

* 3 - Procedimento Docker:

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

#### PROSEGUIMENTO PROCEDURA:

> docker tag primawebapp dasilvatefaele/primawebapp:latest
```powershell
PS C:\Users\39347\Documents\PrimaWebApp\PrimaWebApp> docker tag primawebapp dasilvatefaele/primawebapp:latest
```
> docker tag primawebapp dasilvatefaele/primawebapp:latest
```powershell
PS C:\Users\39347\Documents\PrimaWebApp\PrimaWebApp> docker push dasilvatefaele/primawebapp:latest
The push refers to repository [docker.io/dasilvatefaele/primawebapp]
fe1bce613022: Pushed
190526a05c4c: Pushed
6644aa2c795a: Pushed
ef956b5e5fbc: Pushed
517b3236c982: Pushing [===============================>                   ]  45.26MB/72.36MB
05df6742558b: Pushed
13538303ed9c: Pushing [==================================================>]  46.54MB
5f1ee22ffb5e: Mounted from dasilvatefaele/12_html_css
context canceled
PS C:\Users\39347\Documents\PrimaWebApp\PrimaWebApp> 
```

> docker run -p 80:8080 primawebapp
```powershell
PS C:\Users\39347\Documents\PrimaWebApp\PrimaWebApp> docker run -p 80:8080 primawebapp
warn: Microsoft.AspNetCore.DataProtection.Repositories.FileSystemXmlRepository[60]
      Storing keys in a directory '/root/.aspnet/DataProtection-Keys' that may not be persisted outside of the container. Protected data will be unavailable when container is destroyed. For more information go to https://aka.ms/aspnet/dataprotectionwarning
warn: Microsoft.AspNetCore.DataProtection.KeyManagement.XmlKeyManager[35]
      No XML encryptor configured. Key {c2f35da1-0ab2-480b-9aba-008412d3f4a1} may be persisted to storage in unencrypted form.
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://[::]:8080
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Production
info: Microsoft.Hosting.Lifetime[0]
      Content root path: /app
warn: Microsoft.AspNetCore.HttpsPolicy.HttpsRedirectionMiddleware[3]
      Failed to determine the https port for redirect.
```

> STATO: `Funzionante` [WebApp accessibile *NOTA: no volumi o persistenza dati*]
```url
http://localhost/
```
---


# Mediam sviluppo software per la sanità (informatica sanitaria):
#### gestione app: dal reparto
#### Referente: Matteo Rossi

Numero aziende: **3**

## Digitalizzare cartelle cliniche San Martino

- con metadata (cartella clinica pseudo-digitale)

Problema: 20 anni (47.000 blocchetti di anatomia patologia al mese)
Soluzione: digitalizzazione (sviluppo, integrazione anagrafica)

Requisiti: teste pensanti, chiarezza, propensione alla sicurezza, valore, curiosità, interazione 

5 ASL - 5/8 cartelle cliniche diverse che non interagiscono

immagine diagnostica:

radiologia
endoscopia
dermatologia

DRG: tariffa a prestazione (retrubuizione da Regione a Ospedale)

Quante persone lavorano ad un progetto mediamente?
7
4

UIx 

Back-End / Front - (USERINTERFACE: Photoshop)

Via De Marini (verso Genova via di Francia)

8-17
9-18

Possibilità di crescita

14 mensilità

San Martino
Galliera
Gaslini

Cybercirurity ( asset management )

.NET

---

# Continuous integration

- creare webapp
- creare un immagine (docker e dockerfile)
- usiamo dockerhub come registry
- rendere pubblica l'immagine instanziando un containter attraverso cloud (es. azure o aws)
- collegare dockerhub al cloud. se viene aggiornato il registry deve aggiornarsi anche il container pubblico


Git Actions (o manualmente)
`<LaTuaApp>/.github/workflows/deploy.yml`

requisiti: 
- account github
- github storage (<u>github secrets</u>: simil OTP, memoriziamo dati temporaneamente per proteggere dati sensisibili, es `username`, `password`, `profile` di azure/aws)

continuous integration:
- `deploy` - pipeline per automatizzare i seguenti passaggi:

```
push (sul main di github) ↵ **unico passaggio attivo da compiere**
run & build (via runner/ bin/obj) ↵ 
prendere file sorgente da repository ↵ 
accedere a dockerhub via dati salvati in github secets ↵
run & build dell'immagine (via runner/ immagine docker) ↵
push su dockerhub ↵
(azure uses) +  profile (github actions) + docker hub (sceglie l'immagine) 
```

variabili globali / COSTANTI (in dockerfile)
ENV - variabili di ambiente

```cs
var ambiente = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development";
```


