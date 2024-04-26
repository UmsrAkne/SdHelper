namespace SdHelper.Models
{
    public enum Rate
    {
        /// <summary>
        /// 未評価であることを表します。
        /// </summary>
        None = 0,

        /// <summary>
        /// 評価 A を表します。この値が最も良い評価を表します。
        /// </summary>
        A = 5,

        /// <summary>
        /// 評価 B を表します。
        /// </summary>
        B = 4,

        /// <summary>
        /// 評価 C を表します。
        /// </summary>
        C = 3,

        /// <summary>
        /// 評価 D を表します。
        /// </summary>
        D = 2,

        /// <summary>
        /// 評価 E を表します。この値が最も悪い評価を表します。
        /// </summary>
        E = 1,
    }
}