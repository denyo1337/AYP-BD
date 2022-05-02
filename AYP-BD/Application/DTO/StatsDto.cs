using Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class StatsDto
    {
        public double KD { get; set; }
        public double HsPercentage { get; set; }
        public Rifles Rifles { get; set; }
        public Snipers Snipers { get; set; }
        public Smgs Smgs { get; set; }
        public HeavyGuns HeavyGuns { get; set; }
        public Pistols Pistols { get; set; }
        public long KnifeKills { get; set; }
        public double RealTimeGamePlayed { get; set; }
        public long BombsPlanted { get; set; }
        public long BombsDefused { get; set; }
        public StatsDto(Dictionary<string, long> stats)
        {
            KD = CalcucalteKDRatio(stats[StatsName.TotalKills], stats[StatsName.TotalDeaths]);
            HsPercentage = CalHsPercentage(stats[StatsName.TotalKills], stats[StatsName.TotalHeadshots]);
            Rifles = new Rifles
            {
                Ak47Kills = stats[StatsName.TotalAk47Kills],
                AugKills = stats[StatsName.TotalAUGKills],
                SG556Kills = stats[StatsName.TotalSG556Kills],
                M4A1Kills = stats[StatsName.TotalM4A1Kills],
                FamasKills = stats[StatsName.TotalFAMASKills],
                GalilKills = stats[StatsName.TotalGALILKills]
            };
            Snipers = new Snipers
            {
                AwpKills = stats[StatsName.TotalAWPKills],
                G3SG1Kills = stats[StatsName.TotalG3SG1Kills],
                SCAR20Kills = stats[StatsName.TotalSCAR20Kills],
                SSG08Kills = stats[StatsName.TotalSSG08Kills]
            };
            Smgs = new Smgs
            {
                Mac10Kills = stats[StatsName.TotalMAC10Kills],
                Mp7Kills = stats[StatsName.TotalMP7Kills],
                Mp9Kills = stats[StatsName.TotalMP7Kills],
                P90Kills = stats[StatsName.TotalP90Kills],
                PpBizonKills = stats[StatsName.TotalBIZONKills],
                Ump45Kills = stats[StatsName.TotalUMP45Kills]
            };
            HeavyGuns = new HeavyGuns
            {
                M249Kills = stats[StatsName.TotalM249Kills],
                MAG7Kills = stats[StatsName.TotalMAG7Kills],
                NegevKills = stats[StatsName.TotalNEGEVKills],
                NOVAKills = stats[StatsName.TotalNOVAKills],
                SawedOffKills = stats[StatsName.TotalSAWEDOFFKills],
                XM1014Kills = stats[StatsName.TotalXM1014Kills]
            };
            Pistols = new Pistols
            {
                DesertEagleKills = stats[StatsName.TotalDEAGLEKills],
                FiveSevenKills = stats[StatsName.Total57Kills],
                TEC9Kills = stats[StatsName.TotalTEC9Kills],
                Glock18Kills = stats[StatsName.TotalGLOCKKills],
                P2000Kills = stats[StatsName.TotalUSPSKills],
                P250Kills = stats[StatsName.TotalP250Kills],
                UspSKills = stats[StatsName.TotalUSPSKills]
            };
            KnifeKills = stats[StatsName.TotalKnifeKills];
            RealTimeGamePlayed = CalcRealHoursSpentInGame(stats[StatsName.TotalTimePlayer]);
            BombsPlanted = stats[StatsName.TotalBombsPlanted];
            BombsDefused = stats[StatsName.TotalBombsDefused];
        }

        #region mappers
        private static double CalcucalteKDRatio(long totalKills, long totalDeaths)
        {
            return Math.Round((double)totalKills / totalDeaths, 2);
        }   
        private static double CalHsPercentage(long totalKills, long totalHeadshots)
        {
            return (Math.Round((double)totalHeadshots / totalKills, 2) * 100);
        }
        private static double CalcRealHoursSpentInGame(long timeInSeconds)
        {
            return Math.Round((double)timeInSeconds / 3600, 2);
        }
        #endregion
    }
    public class Rifles
    {
        public long Ak47Kills { get; set; }
        public long M4A1Kills { get; set; }
        public long AugKills { get; set; }
        public long SG556Kills { get; set; }
        public long FamasKills { get; set; }
        public long GalilKills { get; set; }
    }
    public class Snipers
    {
        public long AwpKills { get; set; }
        public long G3SG1Kills { get; set; }
        public long SCAR20Kills { get; set; }
        public long SSG08Kills { get; set; }
    }
    public class Smgs
    {
        public long Mac10Kills { get; set; }
        public long Mp7Kills { get; set; }
        public long Mp9Kills { get; set; }
        public long P90Kills { get; set; }
        public long PpBizonKills { get; set; }
        public long Ump45Kills { get; set; }
    }
    public class HeavyGuns
    {
        public long MAG7Kills { get; set; }
        public long NOVAKills { get; set; }
        public long SawedOffKills { get; set; }
        public long XM1014Kills { get; set; }
        public long M249Kills { get; set; }
        public long NegevKills { get; set; }
    }
    public class Pistols
    {
        public long UspSKills { get; set; }
        public long Glock18Kills { get; set; }
        public long P2000Kills { get; set; }
        public long P250Kills { get; set; }
        public long TEC9Kills { get; set; }
        public long FiveSevenKills { get; set; }
        public long DesertEagleKills { get; set; }

    }
}
