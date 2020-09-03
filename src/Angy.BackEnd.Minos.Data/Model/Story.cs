using System;
using Angy.Model.Abstract;
using NodaTime;

namespace Angy.BackEnd.Minos.Data.Model
{
    public class Story : EntityBase
    {

        public short Retrieves { get; set; }
        public bool Imported { get; set; }

        public Guid SettingId { get; set; }
        public Setting Setting { get; set; }
        
        public Guid PhotoId { get; set; }
        public Photo Photo { get; set; }
        public Instant Inserted { get; set; }
    }
}