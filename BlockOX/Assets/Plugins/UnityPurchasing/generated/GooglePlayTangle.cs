#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("U+FiQVNuZWpJ5SvllG5iYmJmY2CcE2FvmbSTrzIDqSC5RAssNT8L8ZLTXP5smjuUBEAHil9Xryp+kHX+t1gkPvhPeNLH8sAXrEB+tKJHUMRuEMtrn9KCymVqNMbbEQcPSuwmBAvUE7sz6XdYPRuWQzzCk964gU+K3jMX6E8Lhc3+7LbTMjmT5aybA0qeOaL+7BWcAT/gHmoLKqXGJfUGj3rSBbMRzFf/Td0eYXUhtHuZTzpwoMi6dBnZGQI6OzmiyNREFMil5+zx5gm0pbMWtM55VhU7L5Ri0VjieOFibGNT4WJpYeFiYmOW7k8EN/fJtYhXmCwCe6ZJlNqUBIT+qhnTd+nj/DMfhPf4afize8SOS1vbYy+mK6v36JBvF978xGFgYmNi");
        private static int[] order = new int[] { 0,7,11,4,8,5,6,11,13,10,13,11,13,13,14 };
        private static int key = 99;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
