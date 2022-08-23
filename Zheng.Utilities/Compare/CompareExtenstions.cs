namespace Zheng.Utilities.Compare
{
    public static class CompareExtenstions
    {
        /// <summary>
        /// 兩個位元組陣列比較
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool CompareByteArray(this byte[] first, byte[] second)
        {
            if (first == null || second == null) return false;
            if (first.Length != second.Length) return false;

            for (int i = 0; i < first.Length; i++)
                if (first[i] != second[i])
                    return false;

            return true;
        }
    }
}
