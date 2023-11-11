namespace SWP391.OnlineShop.Common.Constraints
{
    public class EmailConstraints
    {
        public const string DomainEmailRequired = "@FPT.EDU.VN";
        public const string EmailNotificationCategory = "Notification";
        public const string EmailNotificationWithPasswordCategory = "NotificationWithPassword";

        #region Email Split Keys
        public const string EmailConstraintSplit = ";";
        #endregion

        #region Email Size Limits
        /// <summary>
        /// 25mb size limit
        /// </summary>
        public const int EmailSizeLimit = 26214400;
        #endregion
    }
}
