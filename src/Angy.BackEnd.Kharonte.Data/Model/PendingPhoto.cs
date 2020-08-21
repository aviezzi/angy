using System;
using Angy.Model;

namespace Angy.BackEnd.Kharonte.Data.Model
{
    public class PendingPhoto : EntityBase
    {
        public DateTimeOffset Inserted { get; set; }
        public string Path { get; set; }
    }
}