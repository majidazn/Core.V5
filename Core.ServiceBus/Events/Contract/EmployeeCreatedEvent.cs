using Core.ServiceBus.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ServiceBus.Events.Contract
{
    public interface EmployeeCreatedEvent
    {
        public int UserId { get;}
        public string Name { get; }
        public string FamilyName { get; }
        public string FatherName { get; }
        public string Email { get; }
        public string BirthCertificationNo { get; }
        public string BirthCertificationSerialNo { get; }
        public long Sex { get; }
        public long Nationality { get; }
        public string NationalCode { get; }
        public string PassportNumber { get; }
        public DateTime BirthDate { get; }
        public long? BirthPlace { get; }
        public bool IsDead { get; }
        public byte Status { get; }
        public long? MaritalStatus { get; }
        public string Image { get; }
        public byte[] RowVersion { get; }
        public AuditBaseDto Base { get; }
        public EmployeeDto Employee { get; }
        public List<AddressDto> Addresses { get; }
        public List<TelecomDto> Telecoms { get; }
        public List<EducationDto> Educations { get; }
    }
}
