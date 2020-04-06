using Aliera.BusinessObjects.Member;
using Aliera.MemberDataAccess;
using DinkToPdf.Contracts;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Aliera.Utilities.Helpers;
using Aliera.Utilities.Logging.CustomExceptions;
using Aliera.Utilities.Constants;

namespace Aliera.MemberService
{
    public class IDMemberService : IIDMemberService
    {
        private IIDMemberDataAccess _iDTemplatesDataAccess;
        private IDocConverter _iDocconverter;
        private IConverter _iConverter;

        public IDMemberService(IIDMemberDataAccess iDTemplatesDataAccess, IDocConverter idocConverter, IConverter iConverter)
        {
            _iDTemplatesDataAccess = iDTemplatesDataAccess;
            _iDocconverter = idocConverter;
            _iConverter = iConverter;
        }

        public async Task<byte[]> GetIDMemberDetails(long userId, int productCode, string productLabel)
        {
            var iDBO = await _iDTemplatesDataAccess.GetIDMemberDetails(userId, productCode, productLabel);
            var html = Utilities.UtilityHelper.ReplaceParametersWithValues(iDBO.GetType(), iDBO, iDBO.Html);
            return _iDocconverter.ConvertHtmlToPDF(_iConverter, html);
        }

        /// <summary>
        /// Sends the physical identifier card request email.
        /// </summary>
        /// <param name="memberId">The member identifier.</param>
        /// <param name="memberDetailId">The member detail identifier.</param>
        /// <param name="memberType">Type of the member.</param>
        /// <returns></returns>
        public async Task<bool> SendPhysicalIdCardRequestEmail(long memberId, long memberDetailId, string memberType)
        {
            if (memberId == 0 && memberDetailId == 0 && string.IsNullOrEmpty(memberType))
                throw new CustomException(nameof(MemberConstants.MemberIdCardServiceSendPhysicalIdCardRequestEmailInputEmptyErrorCode));
            return await _iDTemplatesDataAccess.SendPhysicalIdCardRequestEmail(memberId, memberDetailId, memberType);
        }
    }
}