using Core.ServiceBus.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ServiceBus.Events.Class
{
    public class EmployeeCreatedEvent
    {
        public EmployeeCreatedEvent(int userId, string name, string familyName, string fatherName, string birthCertificationNo, string birthCertificationSerialNo,string email,
            long sex, long nationality, string nationalCode, string passportNumber, DateTime birthDate, long? birthPlace, long? maritalStatus, bool isDead, byte status,
            AuditBaseDto auditBase, EmployeeDto employee, List<AddressDto> addresses, List<TelecomDto> telecoms, List<EducationDto> educations)
        {
            UserId = userId;
            Name = name;
            FamilyName = familyName;
            FatherName = fatherName;
            BirthCertificationNo = birthCertificationNo;
            BirthCertificationSerialNo = birthCertificationSerialNo;
            Sex = sex;
            Nationality = nationality;
            NationalCode = nationalCode;
            PassportNumber = passportNumber;
            BirthDate = birthDate;
            BirthPlace = birthPlace;
            IsDead = isDead;
            Email = email;
            MaritalStatus = maritalStatus;
            Status = status;
            Base = auditBase;
            Employee = employee;
            Addresses = addresses;
            Telecoms = telecoms;
            Educations = educations;
        }

        #region Prperties
        public int UserId { get; private set; }
        public string Name { get; private set; }
        public string FamilyName { get; private set; }
        public string FatherName { get; private set; }
        public string Email { get; private set; }
        public string BirthCertificationNo { get; private set; }
        public string BirthCertificationSerialNo { get; private set; }
        public long Sex { get; private set; }
        public long Nationality { get; private set; }
        public string NationalCode { get; private set; }
        public string PassportNumber { get; private set; }
        public DateTime BirthDate { get; private set; }
        public long? BirthPlace { get; private set; }
        public bool IsDead { get; private set; }
        public byte Status { get; private set; }
        public long? MaritalStatus { get; private set; }
        public string Image { get; private set; }
        public byte[] RowVersion { get; private set; }
        public AuditBaseDto Base { get; private set; }
        public EmployeeDto Employee { get; private set; }
        public List<AddressDto> Addresses { get; private set; }
        public List<TelecomDto> Telecoms { get; private set; }
        public List<EducationDto> Educations { get; private set; }

        #endregion
    }
}
