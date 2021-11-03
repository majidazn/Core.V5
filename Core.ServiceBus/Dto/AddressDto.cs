using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ServiceBus.Dto
{
    public class AddressDto
    {
        #region Constructor
        public AddressDto(long personId, long? countryId, long? cityId, string cityOut, string zone, string mainAddress, string zipCode, byte status, AuditBaseDto auditBase)
        {
            PersonId = personId;
            CountryId = countryId;
            CityId = cityId;
            CityOut = cityOut;
            Zone = zone;
            MainAddress = mainAddress;
            ZipCode = zipCode;
            Status = status;
            Base = auditBase;
        }
        #endregion

        #region Properties
        public long PersonId { get; private set; }
        public long? CountryId { get; private set; }
        public long? CityId { get; private set; }
        public string CityOut { get; private set; }
        public string Zone { get; private set; }
        public string MainAddress { get; private set; }
        public string ZipCode { get; private set; }
        public byte Status { get; private set; }
        public AuditBaseDto Base { get; private set; }
        #endregion
    }
}
