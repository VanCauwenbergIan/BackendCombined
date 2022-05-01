namespace Games.Profiles;

public class DTOProfile : Profile
{
    public DTOProfile()
    {
        CreateMap<Game, GameDTO>().ForMember(dest => dest.FranchiseName, opt => opt.MapFrom<FranchiseResolver>()).ForMember(dest => dest.GameModeNames, opt => opt.MapFrom<GameModeResolver>()).ForMember(dest => dest.DeveloperName, opt => opt.MapFrom<DeveloperResolver>()).ForMember(dest => dest.PublisherName, opt => opt.MapFrom<PublisherResolver>()).ForMember(dest => dest.GenreNames, opt => opt.MapFrom<GenreResolver>()).ForMember(dest => dest.ThemeNames, opt => opt.MapFrom<ThemeResolver>()).ForMember(dest => dest.PlatformNames, opt => opt.MapFrom<PlatformResolver>()).ForMember(dest => dest.ReleaseDates, opt => opt.MapFrom<ReleaseDateResolver>()).ForMember(dest => dest.PlayerPerspectiveNames, opt => opt.MapFrom<PlayerPerspectiveResolver>());
    }

    public class FranchiseResolver : IValueResolver<Game, GameDTO, string>
    {
        private readonly IGameService _service;

        public FranchiseResolver(IGameService service)
        {
            _service = service;
        }

        public string Resolve(Game source, GameDTO destination, string dest, ResolutionContext context)
        {
            if (source.FranchiseId != null)
            {
                Franchise f = _service.GetFranchise(source.FranchiseId).Result;

                return f.Name;
            }
            else { return null; };
        }

    }

    public class GameModeResolver : IValueResolver<Game, GameDTO, List<string>>
    {
        private readonly IGameService _service;

        public GameModeResolver(IGameService service)
        {
            _service = service;
        }

        public List<string> Resolve(Game source, GameDTO destination, List<string> dest, ResolutionContext context)
        {
            if (source.GameModeIds != null && source.GameModeIds.Any())
            {
                List<string> results = new List<string>();

                foreach (string g in source.GameModeIds)
                {
                    GameMode gameMode = _service.GetGameMode(g).Result;

                    results.Add(gameMode.Name);
                }

                return results;
            }
            else { return new List<string>(); };
        }

    }

    public class PlayerPerspectiveResolver : IValueResolver<Game, GameDTO, List<string>>
    {
        private readonly IGameService _service;

        public PlayerPerspectiveResolver(IGameService service)
        {
            _service = service;
        }

        public List<string> Resolve(Game source, GameDTO destination, List<string> dest, ResolutionContext context)
        {
            if (source.PlayerPerspectiveIds != null && source.PlayerPerspectiveIds.Any())
            {
                List<string> results = new List<string>();

                foreach (string p in source.PlayerPerspectiveIds)
                {
                    PlayerPerspective playerPerspective = _service.GetPlayerPerspective(p).Result;

                    results.Add(playerPerspective.Name);
                }

                return results;
            }
            else { return new List<string>(); };
        }

    }

    public class DeveloperResolver : IValueResolver<Game, GameDTO, string>
    {
        private readonly IGameService _service;

        public DeveloperResolver(IGameService service)
        {
            _service = service;
        }

        public string Resolve(Game source, GameDTO destination, string dest, ResolutionContext context)
        {
            Company c = _service.GetCompany(source.DeveloperId).Result;

            return c.Name;
        }

    }

    public class PublisherResolver : IValueResolver<Game, GameDTO, string>
    {
        private readonly IGameService _service;

        public PublisherResolver(IGameService service)
        {
            _service = service;
        }

        public string Resolve(Game source, GameDTO destination, string dest, ResolutionContext context)
        {
            Company c = _service.GetCompany(source.PublisherId).Result;

            return c.Name;
        }

    }

    public class GenreResolver : IValueResolver<Game, GameDTO, List<string>>
    {
        private readonly IGameService _service;

        public GenreResolver(IGameService service)
        {
            _service = service;
        }

        public List<string> Resolve(Game source, GameDTO destination, List<string> dest, ResolutionContext context)
        {
            if (source.GenreIds != null && source.GenreIds.Any())
            {
                List<string> results = new List<string>();

                foreach (string g in source.GenreIds)
                {
                    Genre genre = _service.GetGenre(g).Result;

                    results.Add(genre.Name);
                }

                return results;
            }
            else { return new List<string>(); };
        }

    }

    public class ThemeResolver : IValueResolver<Game, GameDTO, List<string>>
    {
        private readonly IGameService _service;

        public ThemeResolver(IGameService service)
        {
            _service = service;
        }

        public List<string> Resolve(Game source, GameDTO destination, List<string> dest, ResolutionContext context)
        {
            if (source.ThemeIds != null && source.ThemeIds.Any())
            {
                List<string> results = new List<string>();

                foreach (string t in source.ThemeIds)
                {
                    Theme theme = _service.GetTheme(t).Result;

                    results.Add(theme.Name);
                }

                return results;
            }
            else { return new List<string>(); };
        }

    }

    public class PlatformResolver : IValueResolver<Game, GameDTO, List<string>>
    {
        private readonly IGameService _service;

        public PlatformResolver(IGameService service)
        {
            _service = service;
        }

        public List<string> Resolve(Game source, GameDTO destination, List<string> dest, ResolutionContext context)
        {
            if (source.PlatformIds != null && source.PlatformIds.Any())
            {
                List<string> results = new List<string>();

                foreach (string p in source.PlatformIds)
                {
                    Platform platform = _service.GetPlatform(p).Result;

                    results.Add(platform.Name);
                }

                return results;
            }
            else { return new List<string>(); };
        }

    }

    public class ReleaseDateResolver : IValueResolver<Game, GameDTO, List<ReleaseDateDTO>>
    {
        private readonly IGameService _service;

        public ReleaseDateResolver(IGameService service)
        {
            _service = service;
        }

        public List<ReleaseDateDTO> Resolve(Game source, GameDTO destination, List<ReleaseDateDTO> dest, ResolutionContext context)
        {
            if (source.ReleaseDates != null && source.ReleaseDates.Any())
            {
                List<ReleaseDateDTO> results = new List<ReleaseDateDTO>();

                foreach (ReleaseDate r in source.ReleaseDates)
                {
                    ReleaseDateDTO releaseDate = new ReleaseDateDTO();

                    releaseDate.Id = r.Id;
                    releaseDate.TimeStamp = r.TimeStamp;
                    releaseDate.CreatedOn = r.CreatedOn;

                    if (r.PlatformIds != null && r.PlatformIds.Any())
                    {
                        List<string> platformNames = new List<string>();

                        foreach (string p in r.PlatformIds)
                        {
                            Platform platform = _service.GetPlatform(p).Result;
                            platformNames.Add(platform.Name);
                        }

                        releaseDate.PlatformNames = platformNames;
                    }

                    results.Add(releaseDate);
                }

                return results;
            }
            else { return new List<ReleaseDateDTO>(); };
        }

    }
}