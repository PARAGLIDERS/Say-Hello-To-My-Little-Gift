namespace SfxSystem {
    public enum SfxType {
        // ui
        UiButtonPress = 0,
        UiButtonSelect = 1,

        // shots
        ShotDry = 100,
        ShotPistol = 101,
        ShotUzi = 102,
        ShotShotgun = 103,
        ShotAuto = 104,
        ShotDoubleShotgun = 105,
        ShotRocketLauncher = 106,
        ShotMinigun = 107,

        // enemies
        BloodParticles = 200,
        EnemySpawn = 201,
        EnemySnowmanThrow = 202, 
        EnemyChickenExplode = 203, 

        // vfx
        VfxExplosion = 300,
        VfxBloodParticles = 301,
        VfxGunPickup = 302,

        // events
        PlayerDeath = 500,
    }
}
