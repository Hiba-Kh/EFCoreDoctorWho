using DoctorWho.Db.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorWho.Db
{
    public class Episode
    {
        public int EpisodeId { get; set; }
        public int SeriesNumber { get; set; }
        public int EpisodeNumber { get; set; }
        public string EpisodeType { get; set; }
        public String Title { get; set; }
        public DateTime EpisodeDate { get; set; }
        public int AuthorId { get; set; }
        public int DoctorId { get; set; }
        public String Notes { get; set; }
        public EpisodeCompanion EpisodeCompanions { get; set; }
        public EpisodeEnemy EpisodeEnemies { get; set; }
        public Author Author { get; set; }
        public Doctor Doctor { get; set; }



    }
}
