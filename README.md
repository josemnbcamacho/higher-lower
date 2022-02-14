# HigherOrLower

HigherOrLower is a ASP.NET Core Web API project that hosts the HigherOrLower game API.

## Run the application

```bash
docker-compose -f "HigherOrLower/docker-compose.yml" up -d --build

```

## Usage

### Create a game
```bash
curl 'http://localhost:8000/HigherOrLowerGame' \
  -H 'Content-Type: application/json' \
  --data-raw $'{\n  "numberOfPlayers": 4\n}'
```

### Play a Guess
```bash
curl 'http://localhost:8000/HigherOrLowerGame/guess' \
  -H 'Content-Type: application/json' \
  --data-raw $'{\n  "gameId": "c99b39b0-8450-4016-88a2-6bd5de9732b7",\n  "playerId": "5dd32003-af9c-4f22-9dba-230cdfa74729",\n  "guess": 0\n}'
```

### Check the Result
```bash
curl 'http://localhost:8000/HigherOrLowerGame/c99b39b0-8450-4016-88a2-6bd5de9732b7' \
  -H 'accept: */*' \
  --compressed
```