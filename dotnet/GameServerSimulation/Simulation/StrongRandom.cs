using System.Security.Cryptography;

namespace GameServerSimulation.Simulation
{
    internal static class StrongRandom
    {
        private static RandomNumberGenerator _trueRandom = new RNGCryptoServiceProvider();

        private static byte[] _buffer = new byte[256];

        private static int _idx = 256;

        public static byte GetNext() {
            if (_idx > 255) {
                _idx = 0;
                _trueRandom.GetBytes(_buffer);
            }
            var value = _buffer[_idx];
            _idx++;
            return value;
        }
    }
}
