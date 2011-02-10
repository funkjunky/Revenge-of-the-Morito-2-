using Microsoft.Xna.Framework;

namespace Morito
{
    public interface isRespawnable : isMortal
    {
        Vector3 Position { get; set; }

        double RespawnTime { get; set; }
               
        Vector3 RespawnPoint { get; set; }

        Vector2 CreateRespawnPoint();
    }
}
