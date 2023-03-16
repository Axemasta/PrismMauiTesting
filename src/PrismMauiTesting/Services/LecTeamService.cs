using System;
using PrismMauiTesting.Abstractions;
using PrismMauiTesting.Models;

namespace PrismMauiTesting.Services
{
	public class LecTeamService : ITeamService
	{
        public List<LecTeam> GetLecTeams()
        {
            return new List<LecTeam>()
            {
                new LecTeam()
                {
                    Name = "G2 Esports",
                    Titles = 10,
                    OperatingCountry = "Spain",
                    LogoUrl = "https://upload.wikimedia.org/wikipedia/en/thumb/1/12/Esports_organization_G2_Esports_logo.svg/800px-Esports_organization_G2_Esports_logo.svg.png",
                    Joined = new DateTime(2016, 01, 01),
                    IsActive = true,
                },
                new LecTeam()
                {
                    Name = "Koi",
                    Titles = 1,
                    OperatingCountry = "Spain",
                    LogoUrl = "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/a/a5/KOI_%28Spanish_Team%29logo_square.png",
                    Joined = new DateTime(2019, 01, 01),
                    IsActive = true,
                },
                new LecTeam()
                {
                    Name = "Fnatic",
                    Titles = 5,
                    OperatingCountry = "England",
                    LogoUrl = "https://cdn.sanity.io/images/5gii1snx/production/c32c2cb848fd3338ff23a590ec5c0e052b080f27-1000x1000.png",
                    Joined = new DateTime(2013, 01, 01),
                    IsActive = true,
                },
                new LecTeam()
                {
                    Name = "MAD Lions",
                    Titles = 2,
                    OperatingCountry = "Spain",
                    LogoUrl = "https://pbs.twimg.com/profile_images/1617496607657705473/t1zRkfAW_400x400.jpg",
                    Joined = new DateTime(2020, 01, 01),
                    IsActive = true,
                },
                new LecTeam()
                {
                    Name = "Misfits Gaming",
                    Titles = 0,
                    OperatingCountry = "USA",
                    LogoUrl = "https://s-qwer.op.gg/images/lol/teams/455_1672190826068.png",
                    Joined = new DateTime(2016, 01, 01),
                    Exited = new DateTime(2022, 07, 27),
                    IsActive = false,
                },
                new LecTeam()
                {
                    Name = "Alliance",
                    Titles = 1,
                    OperatingCountry = "Sweden",
                    LogoUrl = "https://static.wikia.nocookie.net/lolesports_gamepedia_en/images/3/3e/AllianceOldlogo_square.png",
                    Joined = new DateTime(2014, 01, 01),
                    Exited = new DateTime(2015, 01, 01),
                    IsActive = false,
                }
            };
        }
    }
}

