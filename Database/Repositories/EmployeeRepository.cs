using ArceliaHR.Models;
using Dapper;
using static ArceliaHR.Database.DbServices;

namespace ArceliaHR.Database.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public void Add(EmployeeModel employee)
        {
            using var conn = DbContext.Open();

            conn.Execute(@"
                INSERT INTO Employees (
                Name, FatherName, Religion, MaritalStatus, Gender, DateOfBirth,
                Mobile, ICEContact, Nationality, Relation,
                PassportNumber, PassportIssueDate, PassportExpiryDate,
                IDNumber, IDCardNumber, IDExpiryDate,
                Work, Department, Picture, Status
                )
                VALUES (
                @Name, @FatherName, @Religion, @MaritalStatus, @Gender, @DateOfBirth,
                @Mobile, @ICEContact, @Nationality, @Relation,
                @PassportNumber, @PassportIssueDate, @PassportExpiryDate,
                @IDNumber, @IDCardNumber, @IDExpiryDate,
                @Work, @Department, @Picture, @Status
                )", employee);
        }

        public IEnumerable<EmployeeModel> GetAll()
        {
            using var conn = DbContext.Open();

            return conn.Query<EmployeeModel>(
                "SELECT * FROM Employees;"
            );
        }

        public EmployeeModel? GetById(int id)
        {
            using var conn = DbContext.Open();

            return conn.QuerySingleOrDefault<EmployeeModel>(
                "SELECT * FROM Employees WHERE Id = @Id;",
                new { Id = id }
            );
        }

        public void Delete(int id)
        {
            using var conn = DbContext.Open();

            conn.Execute(
                "DELETE FROM Employees WHERE Id = @Id;",
                new { Id = id }
            );
        }
        public void Update(EmployeeModel employee)
        {
            using var conn = DbContext.Open();

            conn.Execute(@"
        UPDATE Employees SET
            Name = @Name,
            FatherName = @FatherName,
            Religion = @Religion,
            MaritalStatus = @MaritalStatus,
            Gender = @Gender,
            DateOfBirth = @DateOfBirth,

            Mobile = @Mobile,
            ICEContact = @ICEContact,
            Nationality = @Nationality,
            Relation = @Relation,

            PassportNumber = @PassportNumber,
            PassportIssueDate = @PassportIssueDate,
            PassportExpiryDate = @PassportExpiryDate,

            IDNumber = @IDNumber,
            IDCardNumber = @IDCardNumber,
            IDExpiryDate = @IDExpiryDate,

            Work = @Work,
            Department = @Department,
            Picture = @Picture,
            Status = @Status
        WHERE Id = @Id
    ", employee);
        }


    }
}
