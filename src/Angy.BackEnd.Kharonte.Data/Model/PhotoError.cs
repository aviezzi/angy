using System;
using Angy.Model;
using Angy.Model.Abstract;
using NodaTime;

namespace Angy.BackEnd.Kharonte.Data.Model
{
    public class PhotoError : EntityBase
    {
        public Instant Inserted { get; } = Instant.FromDateTimeUtc(DateTime.Now.ToUniversalTime());
        public string Filename { get; set; }
        public string Extension { get; set; }
        public string Message { get; set; }
    }
}