using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Morito
{
    public interface hasScore
    {
        #region Score Catagories

        int Kills { get; set; }
        int Deaths { get; set; }
        int Points { get; set; }

        int ShotsFired { get; set; }
        int ShotsHit { get; set; }

        #endregion
    }
}
