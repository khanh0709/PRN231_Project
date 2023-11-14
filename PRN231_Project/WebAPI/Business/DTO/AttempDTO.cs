﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebAPI.Business.DTO
{
    public partial class AttempDTO
    {
        public int AttempId { get; set; }
        public double? Xpgained { get; set; }
        public int? TournamentId { get; set; }
        public int? UserId { get; set; }
        public int? TotalWins { get; set; }
        public DateTime? Date { get; set; }
        public bool? Accepted { get; set; }
        [JsonIgnore]
        public virtual TournamentDTO? Tournament { get; set; }
        [JsonIgnore]
        public virtual UserDTO? User { get; set; }
    }
}
