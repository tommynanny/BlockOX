#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class AppleTangle
    {
        private static byte[] data = System.Convert.FromBase64String("wRF6/VUG3XdXk/otC7Jdh0VVBfl3SaCQ8dnCbpQsYxnYq7PsEyLLF7k4UORSDDqEYLuHFdZte/dvVm20h3uJaM4TUwEnmrrwTED4aDCWHf0OOAcOC10VGwkJ9wwNOAsJCfc4FVGvDQF0H0heGRZ827+DKzNPq91nOz5SOGo5AzgBDgtdDA4bCl1bORsoaWZsKGttenxhbmFraXxhZ2YoeL8TtZtKLBoizwcVvkWUVmvAQ4gfJziJyw4AIw4JDQ0PCgo4ib4SibsOC10VBgweDBwj2GFPnH4B9vxjhWFuYWtpfGFnZihJfXxgZ3phfHE5igkIDgEijkCO/2tsDQk4ifo4Ig6DEYHW8UNk/Q+jKjgK4BA28FgB2wAjDgkNDQ8KCR4WYHx8eHsyJyd/F5nTFk9Y4w3lVnGMJeM+ql9EXeR4ZG0oWmdnfChLSTgWHwU4Pjg8OqOreZpPW13JpydJu/Dz63jF7qtEtvx7k+babAfDcUc80Ko28XD3Y8DRPnfJj13Rr5GxOkrz0N15lnapWshrO3//Mg8kXuPSBykG0rJ7EUe9f38maXh4ZG0ma2dlJ2l4eGRta2labWRhaWZrbShnZih8YGF7KGttekHQfpc7HG2pf5zBJQoLCQgJq4oJoNR2Kj3CLd3RB95j3KosKxn/qaQeOBwOC10MCxsFSXh4ZG0oWmdnfHhkbShLbXp8YW5ha2l8YWdmKEl9PTo5PDg7PlIfBTs9ODo4MTo5PDhyOIoJfjgGDgtdFQcJCfcMDAsKCQeVNfsjQSASwPbGvbEG0VYU3sM1PpFEJXC/5YST1Pt/k/p+2n84R8lxKGl7e31lbXsoaWtrbXh8aWZrbb0ypfwHBgiaA7kpHiZ83TQF02oeNS5vKII7Yv8FisfW46sn8VtiU2wuOCwOC10MAxsVSXh4ZG0oS216fHppa3xha20oe3xpfG1lbWZ8eyY4OIoMsziKC6uoCwoJCgoJCjgFDgEAVjiKCRkOC10VKAyKCQA4igkMOAwOGwpdWzkbOBkOC10MAhsCSXh4ZG0oQWZrJjkuOCwOC10MAxsVSXgNCAuKCQcIOIoJAgqKCQkI7JmhAWw9Kx1DHVEVu5z//pSWx1iyyVBYIo5Ajv8FCQkNDQg4ajkDOAEOC10mSK7/T0V3AFY4Fw4LXRUrDBA4HiQoa216fGFuYWtpfG0oeGdkYWtxOBkOC10MAhsCSXh4ZG0oQWZrJjl8YW5ha2l8bShqcShpZnEoeGl6fGZsKGtnZmxhfGFnZnsoZ24ofXttnZZyBKxPg1PcHj87w8wHRcYcYdlvhwC8KP/DpCQoZ3i+Nwk4hL9LxxeNi40TkTVPP/qhk0iGJNy5mBrQKGduKHxgbSh8YG1mKGl4eGRha2kP5HUxi4NbKNswzLm3kkcCY/cj9Czq49m/eNcHTekvwvllcOXvvR8famRtKHt8aWZsaXpsKHxtemV7KGkoS0k4igkqOAUOASKOQI7/BQkJCQUOASKOQI7/BQkJDQ0IC4oJCQhUfGBnemF8cTkeOBwOC10MCxsFSXhNdhdEY1ieSYHMfGoDGItJjzuCiYgcI9hhT5x+Afb8Y4UmSK7/T0V3WKKC3dLs9NgBDz+4fX0p");
        private static int[] order = new int[] { 19,17,26,55,29,52,8,45,59,37,30,11,34,16,35,15,59,24,50,58,57,36,38,27,54,34,29,45,28,38,48,40,45,56,48,46,58,56,38,55,46,57,54,46,44,56,55,55,50,58,58,57,57,53,54,59,59,57,58,59,60 };
        private static int key = 8;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
