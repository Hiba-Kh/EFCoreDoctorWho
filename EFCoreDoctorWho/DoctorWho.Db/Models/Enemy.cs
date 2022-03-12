using DoctorWho.Db.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorWho.Db
{
    public class Enemy
    {
        public int EnemyId { get; set; }
        public string EnemyName { get; set; }
        public string Description { get; set; }
        public EpisodeEnemy EnemyEpisodes { get; set; }
    }
}
