using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ischool.Booking.Room
{
    class EnumDescription
    {
        public static string GetIdentityDescription(Type type,string value)
        {
            //Type type = typeof(UserIdentity);
            var name = Enum.GetNames(type).Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase)).Select(d => d).FirstOrDefault();

            if (name == null)
            {
                return string.Empty;
            }

            var field = type.GetField(name);
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            // 無設定Description Attribute, 回傳Enum欄位名稱
            if (customAttribute == null || customAttribute.Length == 0)
            {
                return name;
            }

            // 回傳Description Attribute的設定
            return ((DescriptionAttribute)customAttribute[0]).Description;
        }

        //public static GetReviewDescription()
    }
}
