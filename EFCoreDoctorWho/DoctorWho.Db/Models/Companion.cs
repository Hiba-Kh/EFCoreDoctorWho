using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorWho.Db.Models
{
    public class Companion
    {
        public int CompanionId { get; set; }
        public String CompanionName { get; set; }
        public String WhoPlayed { get; set; }
        public EpisodeCompanion CompanionEpisodes { get; set; }


    }
}
