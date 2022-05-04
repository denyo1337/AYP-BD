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
        private readonly string[] RifleNames = new string[]
        {
           StatsName.TotalAk47Kills,
           StatsName.TotalAUGKills,
           StatsName.TotalM4A1Kills,
           StatsName.TotalFAMASKills,
           StatsName.TotalGALILKills,
           StatsName.TotalG3SG1Kills,
        };
        private readonly string[] SnipersNames = new string[]
        {
           StatsName.TotalAWPKills,
           StatsName.TotalG3SG1Kills,
           StatsName.TotalSCAR20Kills,
           StatsName.TotalSSG08Kills,
        };
        private readonly string[] SmgsNames = new string[]
        {
           StatsName.TotalMAC10Kills,
           StatsName.TotalMP7Kills,
           StatsName.TotalMP9Kills,
           StatsName.TotalUMP45Kills,
           StatsName.TotalMP9Kills,
           StatsName.TotalBIZONKills,
        }; 
        private readonly string[] HeavyGunsNames = new string[]
        {
           StatsName.TotalMAG7Kills,
           StatsName.TotalNOVAKills,
           StatsName.TotalSAWEDOFFKills,
           StatsName.TotalXM1014Kills,
           StatsName.TotalNEGEVKills,
           StatsName.TotalM249Kills,

        };
        private readonly string[] PistolNames = new string[]
        {
           StatsName.TotalUSPSKills,
           StatsName.TotalDEAGLEKills,
           StatsName.Total57Kills,
           StatsName.TotalGLOCKKills,
           StatsName.TotalP250Kills,
           StatsName.TotalTEC9Kills,
        };

        public double? KD { get; set; }
        public double HsPercentage { get; set; }
        public List<Rifles> Rifles { get; set; }
        public List<Snipers> Snipers { get; set; }
        public List<Smgs> Smgs { get; set; }
        public List<HeavyGuns> HeavyGuns { get; set; }
        public List<Pistols> Pistols { get; set; }
        public long KnifeKills { get; set; }
        public double RealTimeGamePlayed { get; set; }
        public long BombsPlanted { get; set; }
        public long BombsDefused { get; set; }
        public StatsDto(Dictionary<string, long> stats)
        {
         
        }
        public StatsDto(List<Stats> stats)
        {
            KD = CalcucalteKDRatio(stats.GetLongValue(StatsName.TotalKills), stats.GetLongValue(StatsName.TotalDeaths));
            HsPercentage = CalHsPercentage(stats.GetLongValue(StatsName.TotalKills), stats.GetLongValue(StatsName.TotalHeadshots));
            Rifles = stats.Where(x => RifleNames.Contains(x.Name)).Select(x => new Rifles
            {
                Name = x.Name.Substring(12),
                Value = x.Value,
            }).ToList();
            Snipers = stats.Where(x => SnipersNames.Contains(x.Name)).Select(x => new Snipers
            {
                Name = x.Name.Substring(12),
                Value = x.Value,
            }).ToList();
            Smgs = stats.Where(x => SmgsNames.Contains(x.Name)).Select(x => new Smgs
            {
                Name = x.Name.Substring(12),
                Value = x.Value,
            }).ToList();
            HeavyGuns = stats.Where(x => HeavyGunsNames.Contains(x.Name)).Select(x => new HeavyGuns
            {
                Name = x.Name.Substring(12),
                Value = x.Value,
            }).ToList();
            Pistols = stats.Where(x => PistolNames.Contains(x.Name)).Select(x => new Pistols
            {
                Name = x.Name.Substring(12) == "hkp2000" ? "USP-S" : x.Name.Substring(12),
                Value = x.Value,
            }).ToList();
            KnifeKills = stats.GetLongValue(StatsName.TotalKnifeKills);
            RealTimeGamePlayed = CalcRealHoursSpentInGame( stats.GetLongValue(StatsName.TotalTimePlayer));
            BombsPlanted = stats.GetLongValue(StatsName.TotalBombsPlanted);
            BombsDefused = stats.GetLongValue(StatsName.TotalBombsDefused);
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
    public class Rifles : BaseModel
    {

    } 
    public class Snipers : BaseModel
    {
    } 
    public class Smgs : BaseModel
    {
    } 
    public class HeavyGuns : BaseModel
    {
    } 
    public class Pistols : BaseModel
    {
    }
    public abstract class BaseModel
    {
        public string Name { get; set; }
        public long Value { get; set; }
    }
}
