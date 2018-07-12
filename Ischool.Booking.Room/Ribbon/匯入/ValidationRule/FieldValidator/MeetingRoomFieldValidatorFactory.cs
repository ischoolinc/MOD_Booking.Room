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
                case "MEETINGROOM_CHECKUNITINISCHOOL":
                    return new CheckUnitInIschool();
                case "INTPARSE":
                    return new CheckInt();
                case "CHECKSTRING":
                    return new CheckString();
                default:
                    return null;
            }

        }

        #endregion

    }
}
