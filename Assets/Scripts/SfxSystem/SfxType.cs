namespace SfxSystem {
    public enum SfxType {
        // ui
        UiButtonPress = 0,
        UiButtonSelect = 1,

        // shots
        ShotDry = 100,
        ShotPistol = 101,
        ShotAuto = 102,
        ShotShotgun = 103,
        ShotUzi = 104,

        // enemies
        EnemyBloodParticles = 200,
        EnemySpawn = 201,
        EnemySnowmanThrow = 202, 
        EnemyChickenExplode = 203, 

        // vfx
        VfxExplosion = 300,
        VfxBloodParticles = 301,
        VfxGunPickup = 302,
    }
}
