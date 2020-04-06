using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class MasterData
    {
        public int MasterDataId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
