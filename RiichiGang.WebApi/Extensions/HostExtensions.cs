using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RiichiGang.Data;
using RiichiGang.Domain;

namespace RiichiGang.WebApi.Extensios
{
    public static class HostExtensions
    {
        public static IHost Seed(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
                {
                    var user = context.Users.FirstOrDefault();

                    if (user is null)
                    {
                        // seed users
                        user = new User("julio", "julio@gmail.com", BCrypt.Net.BCrypt.HashPassword("123456"));
                        context.Users.Add(user);

                        var krohn = new User("krohn", "krohn@gmail.com", BCrypt.Net.BCrypt.HashPassword("123456"));
                        context.Users.Add(krohn);

                        var maria = new User("maria", "maria@gmail.com", BCrypt.Net.BCrypt.HashPassword("123456"));
                        context.Users.Add(maria);

                        var aki = new User("aki", "aki@gmail.com", BCrypt.Net.BCrypt.HashPassword("123456"));
                        context.Users.Add(aki);

                        var setoukuma = new User("setoukuma", "setoukuma@gmail.com", BCrypt.Net.BCrypt.HashPassword("123456"));
                        context.Users.Add(setoukuma);

                        var okada = new User("okada", "okada@gmail.com", BCrypt.Net.BCrypt.HashPassword("123456"));
                        context.Users.Add(okada);

                        var ooi = new User("ooi", "ooi@gmail.com", BCrypt.Net.BCrypt.HashPassword("123456"));
                        context.Users.Add(ooi);

                        var takizawa = new User("takizawa", "takizawa@gmail.com", BCrypt.Net.BCrypt.HashPassword("123456"));
                        context.Users.Add(takizawa);

                        var kobayashi = new User("kobayashi", "kobayashi@gmail.com", BCrypt.Net.BCrypt.HashPassword("123456"));
                        context.Users.Add(kobayashi);

                        var sasaki = new User("sasaki", "sasaki@gmail.com", BCrypt.Net.BCrypt.HashPassword("123456"));
                        context.Users.Add(sasaki);

                        var katsumata = new User("katsumata", "katsumata@gmail.com", BCrypt.Net.BCrypt.HashPassword("123456"));
                        context.Users.Add(katsumata);

                        var shiratori = new User("shiratori", "shiratori@gmail.com", BCrypt.Net.BCrypt.HashPassword("123456"));
                        context.Users.Add(shiratori);

                        var maruyama = new User("maruyama", "maruyama@gmail.com", BCrypt.Net.BCrypt.HashPassword("123456"));
                        context.Users.Add(maruyama);

                        var furuhashi = new User("furuhashi", "furuhashi@gmail.com", BCrypt.Net.BCrypt.HashPassword("123456"));
                        context.Users.Add(furuhashi);

                        var kurosawa = new User("kurosawa", "kurosawa@gmail.com", BCrypt.Net.BCrypt.HashPassword("123456"));
                        context.Users.Add(kurosawa);

                        var fujisaki = new User("fujisaki", "fujisaki@gmail.com", BCrypt.Net.BCrypt.HashPassword("123456"));
                        context.Users.Add(fujisaki);


                        // seed clubs
                        var cluba = new Club("Clube A", user, "https://clubea.etc", "contato@clubea.etc", "Caxias do Sul");
                        context.Clubs.Add(cluba);
                        var rulaa = Ruleset.WRC(cluba);
                        context.Rulesets.Add(rulaa);
                        var rulab = Ruleset.ML(cluba);
                        context.Rulesets.Add(rulab);

                        var memb = new Membership(krohn, cluba)
                        {
                            Status = MembershipStatus.Confirmed
                        };
                        context.Memberships.Add(memb);

                        memb = new Membership(maria, cluba)
                        {
                            Status = MembershipStatus.Confirmed
                        };
                        context.Memberships.Add(memb);

                        memb = new Membership(aki, cluba)
                        {
                            Status = MembershipStatus.Confirmed
                        };
                        context.Memberships.Add(memb);

                        memb = new Membership(setoukuma, cluba)
                        {
                            Status = MembershipStatus.Confirmed
                        };
                        context.Memberships.Add(memb);

                        memb = new Membership(okada, cluba)
                        {
                            Status = MembershipStatus.Confirmed
                        };
                        context.Memberships.Add(memb);

                        memb = new Membership(ooi, cluba)
                        {
                            Status = MembershipStatus.Confirmed
                        };
                        context.Memberships.Add(memb);

                        memb = new Membership(takizawa, cluba)
                        {
                            Status = MembershipStatus.Confirmed
                        };
                        context.Memberships.Add(memb);

                        var clubb = new Club("Clube B", kobayashi, "https://clubeb.etc", "contato@clubeb.etc", "Flores da Cunha");
                        context.Clubs.Add(cluba);
                        var rulba = Ruleset.WRC(clubb);
                        context.Rulesets.Add(rulba);
                        var rulbb = Ruleset.ML(clubb);
                        context.Rulesets.Add(rulbb);

                        memb = new Membership(sasaki, clubb)
                        {
                            Status = MembershipStatus.Confirmed
                        };
                        context.Memberships.Add(memb);

                        memb = new Membership(katsumata, clubb)
                        {
                            Status = MembershipStatus.Confirmed
                        };
                        context.Memberships.Add(memb);

                        memb = new Membership(shiratori, clubb)
                        {
                            Status = MembershipStatus.Confirmed
                        };
                        context.Memberships.Add(memb);

                        memb = new Membership(maruyama, clubb)
                        {
                            Status = MembershipStatus.Confirmed
                        };
                        context.Memberships.Add(memb);

                        memb = new Membership(furuhashi, clubb)
                        {
                            Status = MembershipStatus.Confirmed
                        };
                        context.Memberships.Add(memb);

                        memb = new Membership(kurosawa, clubb)
                        {
                            Status = MembershipStatus.Confirmed
                        };
                        context.Memberships.Add(memb);

                        memb = new Membership(fujisaki, clubb)
                        {
                            Status = MembershipStatus.Confirmed
                        };
                        context.Memberships.Add(memb);


                        // seed tournament a
                        var tourna = new Tournament("Tournament A", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent metus nisi, dictum varius lectus nec, blandit ultricies risus. Quisque imperdiet velit at viverra faucibus. Donec aliquam mi ac mattis ultrices. Nullam vulputate dignissim mollis. Suspendisse eu congue lectus. Etiam porta tincidunt lacinia. Nam laoreet interdum dui nec imperdiet. Sed commodo. ", rulab, cluba, DateTime.UtcNow)
                        {
                            Status = TournamentStatus.Running
                        };
                        context.Tournaments.Add(tourna);

                        var tournap = new List<TournamentPlayer>
                        {
                            new TournamentPlayer(user, tourna, TournamentPlayerStatus.Confirmed),
                            new TournamentPlayer(krohn, tourna, TournamentPlayerStatus.Confirmed),
                            new TournamentPlayer(maria, tourna, TournamentPlayerStatus.Confirmed),
                            new TournamentPlayer(aki, tourna, TournamentPlayerStatus.Confirmed),
                            new TournamentPlayer(setoukuma, tourna, TournamentPlayerStatus.Confirmed),
                            new TournamentPlayer(okada, tourna, TournamentPlayerStatus.Confirmed),
                            new TournamentPlayer(ooi, tourna, TournamentPlayerStatus.Confirmed),
                            new TournamentPlayer(takizawa, tourna, TournamentPlayerStatus.Confirmed)
                        };

                        context.TournamentPlayers.AddRange(tournap);

                        var brackaa = new Bracket(tourna, "Chave A", 10, WinCondition.TopX, 4, 1, 1);
                        context.Brackets.Add(brackaa);

                        var brackaap = new List<BracketPlayer>
                        {
                            new BracketPlayer(tournap[0], brackaa)
                            {
                                Placement = 1,
                                Score = 25.9f
                            },
                            new BracketPlayer(tournap[1], brackaa)
                            {
                                Placement = 2,
                                Score = 5.8f
                            },
                            new BracketPlayer(tournap[2], brackaa)
                            {
                                Placement = 3,
                                Score = 0
                            },
                            new BracketPlayer(tournap[3], brackaa)
                            {
                                Placement = 4,
                                Score = 0
                            },
                            new BracketPlayer(tournap[4], brackaa)
                            {
                                Placement = 5,
                                Score = 0
                            },
                            new BracketPlayer(tournap[5], brackaa)
                            {
                                Placement = 6,
                                Score = 0
                            },
                            new BracketPlayer(tournap[6], brackaa)
                            {
                                Placement = 7,
                                Score = -9.7f
                            },
                            new BracketPlayer(tournap[7], brackaa)
                            {
                                Placement = 8,
                                Score = -22.0f
                            }
                        };
                        context.BracketPlayers.AddRange(brackaap);

                        var sa = new Series(brackaa, brackaap[0], brackaap[1], brackaap[6], brackaap[7]);
                        context.Series.Add(sa);

                        var ga = new Game(sa)
                        {
                            Player1 = new Player
                            {
                                Seat = Seat.East,
                                MatchResult = 35900,
                                EndScore = 25.9f,
                                RunningTotal = 25.9f
                            },
                            Player2 = new Player
                            {
                                Seat = Seat.South,
                                MatchResult = 25800,
                                EndScore = 5.8f,
                                RunningTotal = 5.8f
                            },
                            Player3 = new Player
                            {
                                Seat = Seat.West,
                                MatchResult = 20300,
                                EndScore = -9.7f,
                                RunningTotal = -9.7f
                            },
                            Player4 = new Player
                            {
                                Seat = Seat.North,
                                MatchResult = 18000,
                                EndScore = -22.0f,
                                RunningTotal = -22.0f
                            },
                            PlayedAt = DateTime.UtcNow,
                            LogLink = "https://mahjongsoul.game.yo-star.com/?paipu=210622-6a71d9d3-2707-4fec-a933-cdc54a5db608_a823883725"
                        };
                        context.Games.Add(ga);

                        var sb = new Series(brackaa, brackaap[2], brackaap[3], brackaap[4], brackaap[5]);
                        context.Series.Add(sb);

                        var brackab = new Bracket(tourna, "Chave B", 20, WinCondition.None, 0, 1, 1);
                        context.Brackets.Add(brackab);


                        // seed tournament b
                        var tournb = new Tournament("Tournament B", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent metus nisi, dictum varius lectus nec, blandit ultricies risus. Quisque imperdiet velit at viverra faucibus. Donec aliquam mi ac mattis ultrices. Nullam vulputate dignissim mollis. Suspendisse eu congue lectus. Etiam porta tincidunt lacinia. Nam laoreet interdum dui nec imperdiet. Sed commodo. ", rulbb, clubb, DateTime.UtcNow)
                        {
                            Status = TournamentStatus.Scheduled
                        };
                        context.Tournaments.Add(tournb);

                        var tournbp = new List<TournamentPlayer>
                        {
                            new TournamentPlayer(kobayashi, tournb, TournamentPlayerStatus.Confirmed),
                            new TournamentPlayer(sasaki, tournb, TournamentPlayerStatus.Confirmed),
                            new TournamentPlayer(katsumata, tournb, TournamentPlayerStatus.Confirmed),
                            new TournamentPlayer(shiratori, tournb, TournamentPlayerStatus.Confirmed),
                            new TournamentPlayer(maruyama, tournb, TournamentPlayerStatus.Confirmed),
                            new TournamentPlayer(furuhashi, tournb, TournamentPlayerStatus.Confirmed),
                            new TournamentPlayer(kurosawa, tournb, TournamentPlayerStatus.Confirmed),
                            new TournamentPlayer(fujisaki, tournb, TournamentPlayerStatus.Confirmed)
                        };

                        context.TournamentPlayers.AddRange(tournbp);

                        var brackba = new Bracket(tournb, "Chave A", 10, WinCondition.First, 4, 1, 1);
                        context.Brackets.Add(brackba);

                        var brackbb = new Bracket(tournb, "Chave B", 20, WinCondition.None, 0, 1, 1);
                        context.Brackets.Add(brackbb);

                        // seed tournament b
                        var tournc = new Tournament("Tournament C", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent metus nisi, dictum varius lectus nec, blandit ultricies risus. Quisque imperdiet velit at viverra faucibus. Donec aliquam mi ac mattis ultrices. Nullam vulputate dignissim mollis. Suspendisse eu congue lectus. Etiam porta tincidunt lacinia. Nam laoreet interdum dui nec imperdiet. Sed commodo. ", rulbb, clubb, DateTime.UtcNow)
                        {
                            Status = TournamentStatus.Scheduled
                        };
                        context.Tournaments.Add(tournc);

                        var tourncp = new List<TournamentPlayer>
                        {
                            new TournamentPlayer(kobayashi, tournc, TournamentPlayerStatus.Confirmed),
                            new TournamentPlayer(sasaki, tournc, TournamentPlayerStatus.Confirmed),
                            new TournamentPlayer(katsumata, tournc, TournamentPlayerStatus.Confirmed),
                            new TournamentPlayer(shiratori, tournc, TournamentPlayerStatus.Confirmed),
                            new TournamentPlayer(maruyama, tournc, TournamentPlayerStatus.Confirmed),
                            new TournamentPlayer(furuhashi, tournc, TournamentPlayerStatus.Confirmed),
                            new TournamentPlayer(kurosawa, tournc, TournamentPlayerStatus.Confirmed),
                            new TournamentPlayer(fujisaki, tournc, TournamentPlayerStatus.Confirmed)
                        };

                        context.TournamentPlayers.AddRange(tourncp);

                        var brackca = new Bracket(tournc, "Chave A", 10, WinCondition.FirstAndSecond, 4, 1, 1)
                        {
                            Sequence = 0,
                            FinalScoreMultiplier = 0.5f
                        };
                        context.Brackets.Add(brackca);

                        var brackcb = new Bracket(tournc, "Chave B", 20, WinCondition.None, 0, 1, 1)
                        {
                            Sequence = 10
                        };
                        context.Brackets.Add(brackcb);
                    }

                    context.SaveChanges();
                }
            }

            return host;
        }
    }
}