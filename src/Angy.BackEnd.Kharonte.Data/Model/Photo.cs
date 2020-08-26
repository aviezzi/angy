using System;
using Angy.Model;
using NodaTime;

namespace Angy.BackEnd.Kharonte.Data.Model
{
    public class Photo : EntityBase
    {
        public Instant Inserted { get; } = Instant.FromDateTimeUtc(DateTime.Now.ToUniversalTime());
        public string Path { get; set; }
        public string Filename { get; set; }
        public string Extension { get; set; }
    }
}