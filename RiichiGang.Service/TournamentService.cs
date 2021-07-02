using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using RiichiGang.Data;
using RiichiGang.Domain;
using RiichiGang.Service.InputModel;

namespace RiichiGang.Service
{
    public class TournamentService
    {
        private ApplicationDbContext _context;

        public TournamentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Tournament> GetAll()
            => _context.Tournaments.AsQueryable()
                .Include(t => t.Players)
                .AsEnumerable();

        public Tournament GetById(int id)
            => _context.Tournaments.AsQueryable()
                .Include (t => t.Club)
                    .ThenInclude(c => c.Owner)
                .Include(t => t.Ruleset)
                .Include(t => t.Players)
                    .ThenInclude(p => p.User)
                .Include(t => t.Brackets)
                    .ThenInclude(b => b.Players)
                        .ThenInclude(p => p.Player)
                            .ThenInclude(p => p.User)
                .Include(t => t.Brackets)
                    .ThenInclude(b => b.Series)
                        .ThenInclude(s => s.Games)
                .SingleOrDefault(t => t.Id == id);

        public async Task<Tournament> AddTournamentAsync(
            TournamentInputModel inputModel,
            Ruleset ruleset,
            Club club)
        {
            if (ruleset is null)
                throw new ArgumentNullException("O ruleset não pode ser nulo");

            if (club is null)
                throw new ArgumentNullException("O clube não pode ser nulo");

            if (ruleset.ClubId != club.Id)
                throw new ArgumentException("Ruleset e clube não batem");

            var tournament = new Tournament(
                inputModel.Name,
                inputModel.Description,
                ruleset,
                club,
                inputModel.StartDate)
            {
                AllowNonMembers = inputModel.AllowNonMembers,
                RequirePermission = inputModel.RequirePermission
            };

            await _context.AddAsync(tournament);

            if (!inputModel.Brackets.Any())
                throw new ArgumentException("Um torneio deve ter ao menos uma chave");

            var brackets = inputModel.Brackets.OrderBy(b => b.Sequence);
            if (brackets.Last().WinCondition != WinCondition.None.ToString())
                throw new ArgumentException("A última chave não deve conter uma condição de vitória");

            var lastSeq = -1;
            foreach (var im in brackets)
            {
                if (lastSeq == im.Sequence)
                    throw new ArgumentException("Existem sequências repetidas");

                lastSeq = im.Sequence;

                var winCondition = Enum.Parse<WinCondition>(im.WinCondition);

                if (winCondition == WinCondition.First || winCondition == WinCondition.FirstAndSecond)
                    im.NumberOfSeries = 1;

                var bracket = new Bracket(
                    tournament,
                    im.Name,
                    im.Sequence,
                    winCondition,
                    im.NumberOfAdvancing,
                    im.NumberOfSeries,
                    im.GamesPerSeries);

                await _context.AddAsync(bracket);
            }

            await _context.SaveChangesAsync();
            return tournament;
        }

        public async Task<Tournament> UpdateTournamentAsync(Tournament tournament, TournamentInputModel inputModel, Ruleset ruleset)
        {
            if (tournament is null)
                throw new ArgumentNullException("O clube não pode ser nulo");

            if (tournament.Status != TournamentStatus.Scheduled)
                throw new Exception("Um torneio já iniciado não pode ser atualizado");

            if (ruleset != null)
            {
                if (ruleset.ClubId != tournament.ClubId)
                    throw new ArgumentException("Ruleset e clube não batem");

                tournament.SetRuleset(ruleset);
            }

            if (!string.IsNullOrWhiteSpace(inputModel.Name))
                tournament.SetName(inputModel.Name);

            if (!string.IsNullOrWhiteSpace(inputModel.Description))
                tournament.SetDescription(inputModel.Description);

            tournament.StartDate = inputModel.StartDate;
            tournament.AllowNonMembers = inputModel.AllowNonMembers;
            tournament.RequirePermission = inputModel.RequirePermission;

            _context.Update(tournament);
            await _context.SaveChangesAsync();

            return tournament;
        }

        public async Task DeleteTournamentAsync(Tournament tournament)
        {
            _context.Remove(tournament);
            await _context.SaveChangesAsync();
        }

        public async Task AskInviteAsync(Tournament tournament, User user)
        {
            if (user is null)
                throw new ArgumentNullException(nameof(user));

            if (tournament is null)
                throw new ArgumentNullException(nameof(tournament));

            if (tournament.Status != TournamentStatus.Scheduled)
                throw new Exception("Não é possível participar de um torneio já iniciado");

            if (tournament.Players.Any(p => p.UserId == user.Id))
                throw new ArgumentException($"{user.Username} já pediu para participar no torneio \"{tournament.Name}\"");

            if (!tournament.AllowNonMembers && tournament.Club.OwnerId == user.Id)
            {
                var club = _context.Clubs
                    .Include(c => c.Members)
                    .Single(c => c.Id == tournament.ClubId);

                if (!club.Members.Any(m => m.UserId == user.Id) && tournament.Club.OwnerId != user.Id)
                    throw new ArgumentException("Torneio não permite jogadores de fora do clube");
            }

            if (!tournament.RequirePermission || tournament.Club.OwnerId == user.Id)
            {
                var player = new TournamentPlayer(user, tournament, TournamentPlayerStatus.Confirmed);
                await _context.AddAsync(player);
            }
            else
            {
                var player = new TournamentPlayer(user, tournament, TournamentPlayerStatus.Pending);
                await _context.AddAsync(player);

                var notification = new Notification($"pediu para participar no torneio \"{tournament.Name}\"", user, tournament.Club.Owner, null, player);
                await _context.AddAsync(notification);
            }

            await _context.SaveChangesAsync();
        }

        public Task QuitAsync(Tournament tournament, User user)
        {
            if (tournament is null)
                throw new ArgumentNullException(nameof(tournament));

            if (user is null)
                throw new ArgumentNullException(nameof(user));

            var player = tournament.Players.SingleOrDefault(p => p.UserId == user.Id);

            if (player is null)
                throw new ArgumentException($"{user.Username} não está inscrito no torneio \"{tournament.Id}\"");

            _context.Remove(player);
            return _context.SaveChangesAsync();
        }

        public async Task InitTournamentAsync(Tournament tournament)
        {
            if (tournament is null)
                throw new ArgumentNullException(nameof(tournament));

            if (tournament.Status != TournamentStatus.Scheduled)
                throw new ArgumentException("torneio já inicializado");

            if (!tournament.Players.Any())
                throw new ArgumentException("Torneio não conta com nenhum jogador");

            var players = tournament.Players.Count();
            var brackets = tournament.Brackets.OrderBy(b => b.Sequence);

            foreach (var bracket in brackets)
            {
                if (players % 4 > 0)
                    throw new ArgumentException($"número de jogadores insuficiente para a chave \"{bracket.Name}\"");

                switch (bracket.WinCondition)
                {
                case WinCondition.First:
                    players = players / 4;
                    break;

                case WinCondition.FirstAndSecond:
                    players = players / 2;
                    break;

                case WinCondition.TopX:
                    if (players < bracket.NumberOfAdvancing)
                        throw new ArgumentException($"número de jogadores insuficiente para a chave \"{bracket.Name}\"");

                    players = bracket.NumberOfAdvancing;
                    break;

                case WinCondition.None:
                    break;
                }
            }

            var firstBracket = brackets.First();
            firstBracket.StartedAt = DateTime.UtcNow;
            _context.Update(firstBracket);
            var bracketPlayers = new List<BracketPlayer>();

            foreach (var player in tournament.Players)
            {
                var bracketPlayer = new BracketPlayer(player, firstBracket);
                bracketPlayers.Add(bracketPlayer);
            }

            await _context.AddRangeAsync(bracketPlayers);

            for (var i = 0; i < firstBracket.NumberOfSeries; i++)
            {
                var shuffled = bracketPlayers.OrderBy(_ => Guid.NewGuid()).AsEnumerable();

                while (shuffled.Any())
                {
                    var seriesPlayers = shuffled.Take(4).ToList();
                    shuffled = shuffled.Skip(4); // might be straight up wrong

                    var series = new Series(
                        firstBracket,
                        seriesPlayers[0],
                        seriesPlayers[1],
                        seriesPlayers[2],
                        seriesPlayers[3]);

                    await _context.AddAsync(series);

                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<Game> AddGameAsync(Bracket bracket, Series series, GameInputModel inputModel)
        {
            if (bracket is null)
                throw new ArgumentNullException(nameof(bracket));

            if (series is null)
                throw new ArgumentNullException(nameof(series));

            if (inputModel is null)
                throw new ArgumentNullException(nameof(inputModel));

            if (series.Games.Count() == bracket.GamesPerSeries)
                throw new InvalidOperationException("Série já completa");

            // fetch data
            var split = inputModel.LogLink.Split("?paipu=", 2);

            if (split[0] != "https://mahjongsoul.game.yo-star.com/")
                throw new ArgumentException($"Link de log inválido");

            var client = new WebClient();
            var uri = new Uri($"https://tensoul.herokuapp.com/convert?id={split[1]}");
            var rawLog = client.DownloadString(uri);
            var json = JObject.Parse(rawLog);

            // create game
            var p1Seat = Enum.Parse<Seat>(inputModel.Player1Seat, true);
            var p1Offset = CalculateOffset(p1Seat);
            var p1MatchResult = (int) json["sc"][p1Offset];
            var p1EndScore = (float) json["sc"][p1Offset + 1];

            var p2Seat = Enum.Parse<Seat>(inputModel.Player2Seat, true);
            var p2Offset = CalculateOffset(p2Seat);
            var p2MatchResult = (int) json["sc"][p2Offset];
            var p2EndScore = (float) json["sc"][p2Offset + 1];

            var p3Seat = Enum.Parse<Seat>(inputModel.Player3Seat, true);
            var p3Offset = CalculateOffset(p3Seat);
            var p3MatchResult = (int) json["sc"][p3Offset];
            var p3EndScore = (float) json["sc"][p3Offset + 1];

            var p4Seat = Enum.Parse<Seat>(inputModel.Player4Seat, true);
            var p4Offset = CalculateOffset(p4Seat);
            var p4MatchResult = (int) json["sc"][p4Offset];
            var p4EndScore = (float) json["sc"][p4Offset + 1];

            var game = new Game(series)
            {
                Player1 = new Player
                {
                    Seat = p1Seat,
                    MatchResult = p1MatchResult,
                    EndScore = p1EndScore,
                    RunningTotal = series.Player1.Score + p1EndScore
                },
                Player2 = new Player
                {
                    Seat = p2Seat,
                    MatchResult = p2MatchResult,
                    EndScore = p2EndScore,
                    RunningTotal = series.Player2.Score + p2EndScore
                },
                Player3 = new Player
                {
                    Seat = p3Seat,
                    MatchResult = p3MatchResult,
                    EndScore = p3EndScore,
                    RunningTotal = series.Player3.Score + p3EndScore
                },
                Player4 = new Player
                {
                    Seat = p4Seat,
                    MatchResult = p4MatchResult,
                    EndScore = p4EndScore,
                    RunningTotal = series.Player4.Score + p4EndScore
                },
                PlayedAt = DateTime.Parse((string) json["title"][2]), // might not work
                LogLink = inputModel.LogLink
            };

            await _context.AddAsync(game);

            series.Player1.Score += p1EndScore;
            await _context.AddAsync(series.Player1);

            series.Player2.Score += p2EndScore;
            await _context.AddAsync(series.Player2);

            series.Player3.Score += p3EndScore;
            await _context.AddAsync(series.Player3);

            series.Player4.Score += p4EndScore;
            await _context.AddAsync(series.Player4);

            await _context.SaveChangesAsync();

            await UpdateBracketAsync(series.BracketId);

            await UpdatePlayerStats(series.Player1.Player.UserId, json, p1Seat);
            await UpdatePlayerStats(series.Player2.Player.UserId, json, p2Seat);
            await UpdatePlayerStats(series.Player3.Player.UserId, json, p3Seat);
            await UpdatePlayerStats(series.Player4.Player.UserId, json, p4Seat);

            return game;
        }

        private int CalculateOffset(Seat seat)
        {
            var res = -1;

            switch (seat)
            {
            case Seat.East:
                res =  0;
                break;

            case Seat.South:
                res =  2;
                break;

            case Seat.West:
                res =  4;
                break;

            case Seat.North:
                res =  6;
                break;
            }

            return res;
        }

        private async Task UpdateBracketAsync(int bracketId)
        {
            // fetch data
            var bracket = _context.Brackets.AsQueryable()
                .Include(b => b.Players)
                    .ThenInclude(p => p.Player)
                .Include(b => b.Series)
                    .ThenInclude(s => s.Games)
                .SingleOrDefault(b => b.Id == bracketId);

            // update the player's placement
            var placement = 1;

            foreach (var player in bracket.Players.OrderByDescending(p => p.Score))
            {
                if (player.Placement != placement)
                {
                    player.Placement = placement;
                    _context.Update(player);
                }

                placement++;
            }

            // check if the bracket endend
            foreach (var s in bracket.Series)
            {
                if (s.Games.Count() < bracket.GamesPerSeries)
                {
                    await _context.SaveChangesAsync();
                    return;
                }
            }

            bracket.FinishedAt = DateTime.UtcNow;
            _context.Update(bracket);

            // advance bracket
            var tournament = GetById(bracket.TournamentId);
            var nextBracket = tournament.Brackets
                .OrderBy(b => b.Sequence)
                .FirstOrDefault(b => b.Sequence > bracket.Sequence);

            if (nextBracket is null)
            {
                // was the last bracket
                return;
            }

            nextBracket.StartedAt = DateTime.UtcNow;
            var bracketPlayers = new List<BracketPlayer>();

            switch (bracket.WinCondition)
            {
            case WinCondition.First:
                foreach (var s in bracket.Series)
                {
                    var id = s.FirstPlace();
                    var p = bracket.Players.Single(p => p.Id == id);

                    var bracketPlayer = new BracketPlayer(p.Player, nextBracket);
                    bracketPlayer.Score = p.Score * bracket.FinalScoreMultiplier;
                    bracketPlayers.Add(bracketPlayer);
                }

                break;

            case WinCondition.FirstAndSecond:
                foreach (var s in bracket.Series)
                {
                    // first
                    var id = s.FirstPlace();
                    var p = bracket.Players.Single(p => p.Id == id);

                    var bracketPlayer = new BracketPlayer(p.Player, nextBracket);
                    bracketPlayer.Score = p.Score * bracket.FinalScoreMultiplier;
                    bracketPlayers.Add(bracketPlayer);

                    // second
                    id = s.SecondPlace();
                    p = bracket.Players.Single(p => p.Id == id);

                    bracketPlayer = new BracketPlayer(p.Player, nextBracket);
                    bracketPlayer.Score = p.Score * bracket.FinalScoreMultiplier;
                    bracketPlayers.Add(bracketPlayer);
                }

                break;

            case WinCondition.TopX:
                var count = bracket.NumberOfAdvancing;

                foreach (var p in bracket.Players.OrderByDescending(p => p.Score))
                {
                    var bracketPlayer = new BracketPlayer(p.Player, nextBracket);
                    bracketPlayer.Score = p.Score * bracket.FinalScoreMultiplier;
                    bracketPlayers.Add(bracketPlayer);

                    count--;

                    if (count <= 0)
                    {
                        break;
                    }
                }

                break;

            case WinCondition.None:
                throw new InvalidOperationException("unreachable");
            }

            await _context.AddRangeAsync(bracketPlayers);

            for (var i = 0; i < nextBracket.NumberOfSeries; i++)
            {
                var shuffled = bracketPlayers.OrderBy(_ => Guid.NewGuid()).AsEnumerable();

                while (shuffled.Any())
                {
                    var seriesPlayers = shuffled.Take(4).ToList();
                    shuffled = shuffled.Skip(4); // might be straight up wrong

                    var series = new Series(
                        nextBracket,
                        seriesPlayers[0],
                        seriesPlayers[1],
                        seriesPlayers[2],
                        seriesPlayers[3]);

                    await _context.AddAsync(series);
                }
            }

            await _context.SaveChangesAsync();
        }

        private Task UpdatePlayerStats(int userId, JObject log, Seat seat)
        {
            var user = _context.Users.AsQueryable()
                .FirstOrDefault(u => u.Id == userId);

            if (user is null)
                throw new ArgumentNullException("Usuário não encontrado");

            user.Stats.TotalGames += 1;

            var placement = 1;
            var score = (int) log["sc"][CalculateOffset(seat)];

            for (var i = 0; i <= 6; i += 2 )
            {
                if ((int) log["sc"][i] > score)
                {
                    placement++;
                }
            }

            switch (placement)
            {
            case 1:
                user.Stats.FirstPlaces++;
                break;

            case 2:
                user.Stats.SecondPlaces++;
                break;

            case 3:
                user.Stats.ThirdPlaces++;
                break;

            case 4:
                user.Stats.FourthPlaces++;
                break;
            }

            if (score < 0)
            {
                user.Stats.TotalBusted++;
            }

            var drawsOffset = 0;
            var discardsOffset = 0;
            var resultOffset = 16;
            var playerOffset = 0;

            switch (seat)
            {
            case Seat.East:
                drawsOffset = 5;
                discardsOffset = 6;
                playerOffset = 0;
                break;

            case Seat.South:
                drawsOffset = 8;
                discardsOffset = 9;
                playerOffset = 1;
                break;

            case Seat.West:
                drawsOffset = 11;
                discardsOffset = 12;
                playerOffset = 2;
                break;

            case Seat.North:
                drawsOffset = 14;
                discardsOffset = 15;
                playerOffset = 3;
                break;
            }

            var rounds = (JArray) log["log"];
            foreach (var round in rounds)
            {
                user.Stats.TotalRounds += 1;

                var draws = (JArray) round[drawsOffset];
                foreach (JValue draw in draws)
                {
                    if (draw.Type == JTokenType.String)
                    {
                        user.Stats.CallRounds++;
                        break;
                    }
                }

                var discards = (JArray) round[discardsOffset];
                foreach (JValue discard in discards)
                {
                    if (discard.Type == JTokenType.String)
                    {
                        user.Stats.RiichiRounds++;
                        break;
                    }
                }

                var winner = (int) round[resultOffset][2][0];
                var loser = (int) round[resultOffset][2][1];

                if (winner == playerOffset)
                {
                    user.Stats.WinRounds++;

                    if (loser == playerOffset)
                    {
                        user.Stats.TsumoRounds++;
                    }
                }
                else if (loser == playerOffset)
                {
                    user.Stats.DealInRounds++;
                }
            }

            _context.Update(user);
            return _context.SaveChangesAsync();
        }
    }
}
