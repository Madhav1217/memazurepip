using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class BulletinBoardNews
    {
        public long BulletinBoardNewsId { get; set; }
        public string News { get; set; }
        public DateTime? NewsCreateTime { get; set; }
        public long? CreatorId { get; set; }
    }
}
