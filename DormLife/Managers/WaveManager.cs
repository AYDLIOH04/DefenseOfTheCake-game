using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormLife.Managers
{
    public class WaveManager
    {
        private static int _spawnCount = 1;
        private static int _waveLevel = 1;

        public static int GetWaveLevel() => _waveLevel;

        public static void GenerateWave()
        {
            WavesGenerator();
            
            _spawnCount += 2;
            _waveLevel++;
        }

        #region WavesGenerator

        private static void WavesGenerator()
        {
            ProjectileManager.DeleteTiles();
            WallManager.DeleteWalls();


            if (_waveLevel > 10)
            {
                MoreTenWavesGenerator();
            }
            else if (_waveLevel > 5)
            {
                MoreFiveWavesGenerator();
            }
            else
            {
                FirstWavesGenerator();
            }

            HardEnemyGenerator();
            AdditionalObjectsGenerator();
            EnemyGenerator();
        }
                         
        private static void FirstWavesGenerator()
        {
            WallManager.GenerateWalls();
        }

        private static void MoreFiveWavesGenerator()
        {
            for (int _ = 1; _ <= 2; _++)
                WallManager.GenerateWalls(); 
        }

        private static void MoreTenWavesGenerator()
        {
            for (int _ = 1; _ <= 3; _++)
                WallManager.GenerateWalls();
        }

        private static void HardEnemyGenerator()
        {
            for (int _ = 1; _ <= Math.Floor(_waveLevel / 3d); _++)
                    EnemyManager.AddHardEnemy();
        }

        private static void EnemyGenerator()
        {
            for (int _ = 1; _ <= _spawnCount; _++)
                EnemyManager.AddEnemy();
        } 

        private static void AdditionalObjectsGenerator()
        {

            if (_waveLevel % 3 == 0)
            {
                // TODO здоровья тортику
                // Буду генерировать рандомную позицию для ботла с HP
            }
        }

        #endregion
    }
}
