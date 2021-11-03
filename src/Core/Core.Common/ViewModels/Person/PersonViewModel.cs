using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common.ViewModels.Person
{
    public class PersonViewModel
    {
        public int PersonId { get; set; }
        public int Center { get; set; }
        public string Name { get; set; }
        public string MidName { get; set; }
        public string FamilyName { get; set; }
        public string FatherName { get; set; }
        //شماره شناسنامه
        public string BirthCertificateNo { get; set; }
        //مسلسل شناسنامه
        public string BirthCertificateCode { get; set; }
        public byte Sex { get; set; }
        public int Nationality { get; set; }
        public string NationalCode { get; set; }
        public DateTime BirthDate { get; set; }
        public int BirthPlace { get; set; }
        public byte IsDisable { get; set; }
        public byte IsDead { get; set; }
        public byte MaritalStatus { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        //public virtual ICollection<Employee> Employees { get; set; }
        //public virtual ICollection<Address> Addresses { get; set; }
        //public virtual ICollection<Telecom> Telecoms { get; set; }
        //public virtual ICollection<ElectronicAddress> ElectronicAddresses { get; set; }
        public int TenantId { get; set; }
        public string PassportNumber { get; set; }

        public string FullName { get; set; }
        public string SexString { get; set; }
        public string PersianBirthDate { get; set; }
        public string BirthPlaceString { get; set; }

        public int CityAddress { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public int AccessType { get; set; }
        public int PositionId { get; set; }
    }
}
