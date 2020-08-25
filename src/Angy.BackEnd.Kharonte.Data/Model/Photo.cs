using System;
using Angy.Model;

namespace Angy.BackEnd.Kharonte.Data.Model
{
    public class Photo : EntityBase
    {
        public DateTimeOffset Inserted { get; } = DateTimeOffset.Now;
        public string Path { get; set; }
        public string Filename { get; set; }
        public string Extension { get; set; }
    }
}