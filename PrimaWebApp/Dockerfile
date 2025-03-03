# Usa l'immagine di .NET SDK per la fase di build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Imposta la directory di lavoro all'interno del container
WORKDIR /app

# Copia il file .csproj nella directory di lavoro
COPY *.csproj ./

# Copia tutti i file della cartella corrente nella directory di lavoro
COPY . ./

# Esegui il restore dei pacchetti NuGet e pubblica il progetto in modalità Release
RUN dotnet restore && dotnet publish -c Release -o out

# Usa l'immagine di .NET ASP.NET per la fase di esecuzione del container
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Imposta la directory di lavoro all'interno del container
WORKDIR /app

# Copia i file generati durante la fase di build dalla fase precedente
COPY --from=build /app/out ./

# Imposta il fuso orario a Europa/Roma
ENV TZ=Europe/Rome
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

# Imposta la variabile di ambiente per il sistema .NET in modalità di globalizzazione invariabile
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=0

# Aggiorna la lista dei pacchetti e installa il pacchetto di localizzazione
RUN apt-get update && apt-get install -y locales && rm -rf /var/lib/apt/lists/*

# Genera la locale per l'Italia con codifica UTF-8
RUN locale-gen it_IT.UTF-8
# Imposta la lingua e la localizzazione predefinita a italiano (Italia)
ENV LANG=it_IT.UTF-8
ENV LC_ALL=it_IT.UTF-8

# Imposta la valuta di default come Euro
ENV DOTNET_CURRENCY="€"

# Scrive la data e l'ora correnti in un file di build_time.txt
RUN date "+%Y-%m-%d %H:%M:%S" > /app/build_time.txt

# Imposta l'ambiente di produzione per l'applicazione .NET
ENV DOTNET_ENVIRONMENT=Production

# Definisce un argomento di build per la versione dell'applicazione, con un valore predefinito
ARG BUILD_VERSION=1.0.0

# Imposta la variabile di ambiente APP_VERSION alla versione di build
ENV APP_VERSION=$BUILD_VERSION

# Scrive la versione dell'applicazione in un file version.txt
RUN echo $APP_VERSION > /app/version.txt

# Imposta il percorso del file JSON dei prodotti come variabile di ambiente
ENV PRODOTTI_JSON_PATH=/app/wroot/data/prodotti.json

# Crea la directory per i dati dei prodotti
RUN mkdir -p /app/wroot/data

# Definisce un volume per la directory dei dati dell'applicazione
VOLUME ["/app/data"]

# Espone la porta 8080 per il container
EXPOSE 8080

# Definisce il punto di ingresso dell'applicazione come esecuzione del file .dll con dotnet
ENTRYPOINT ["dotnet", "PrimaWebApp.dll"]
