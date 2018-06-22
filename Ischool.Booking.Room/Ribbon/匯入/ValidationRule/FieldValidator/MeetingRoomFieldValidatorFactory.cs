using Campus.DocumentValidator;

namespace Ischool.Booking.Room
{
    class MeetingRoomFieldValidatorFactory : IFieldValidatorFactory
    {
        #region IFieldValidatorFactory 成員

        /// <summary>
        /// 根據typeName建立對應的FieldValidator
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="validatorDescription"></param>
        /// <returns></returns>
        public IFieldValidator CreateFieldValidator(string typeName, System.Xml.XmlElement validatorDescription)
        {
            switch (typeName.ToUpper())
            {
                case "CHECKACCOUNTINISCHOOL":
                    return new CheckAccountInIschool();
                case "CHECKUNITINISCHOOL":
                    return new CheckUnitInIschool();
                default:
                    return null;
            }

        }

        #endregion

    }
}
