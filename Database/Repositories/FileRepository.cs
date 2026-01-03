using ArceliaHR.Database;
using ArceliaHR.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace ArceliaHR.Database.Repositories
{
    public class FileRepository
    {
        // Insert a new file into DB
        public void AddFile(string filePath)
        {
            var fileName = Path.GetFileName(filePath);
            var ext = Path.GetExtension(filePath).ToLower();
            var data = File.ReadAllBytes(filePath);
            var size = data.Length;

            using var conn = DbServices.DbContext.Open();
            conn.Execute(@"
                INSERT INTO Files (FileName, Extension, MimeType, FileSize, Data)
                VALUES (@FileName, @Extension, @MimeType, @FileSize, @Data)",
                new
                {
                    FileName = fileName,
                    Extension = ext,
                    MimeType = GetMimeType(ext),
                    FileSize = size,
                    Data = data
                });
        }
        public void AddFileForEmployee(string filePath, int employeeId, string? customName = null)
        {
            var fileName = string.IsNullOrEmpty(customName) ? Path.GetFileName(filePath) : customName;
            var ext = Path.GetExtension(filePath).ToLower();
            var data = File.ReadAllBytes(filePath);
            var size = data.Length;

            using var conn = DbServices.DbContext.Open();
            conn.Execute(@"
        INSERT INTO Files (EmployeeId, FileName, Extension, MimeType, FileSize, Data)
        VALUES (@EmployeeId, @FileName, @Extension, @MimeType, @FileSize, @Data)",
                new
                {
                    EmployeeId = employeeId,
                    FileName = fileName,
                    Extension = ext,
                    MimeType = GetMimeType(ext),
                    FileSize = size,
                    Data = data
                });
        }

        // Get all files (for explorer view)
        public List<FileModel> GetAllFiles()
        {
            using var conn = DbServices.DbContext.Open();
            return conn.Query<FileModel>("SELECT * FROM Files ORDER BY CreatedAt DESC").ToList();
        }

        // Get single file by Id
        public FileModel? GetFileById(int id)
        {
            using var conn = DbServices.DbContext.Open();
            return conn.QuerySingleOrDefault<FileModel>(
                "SELECT * FROM Files WHERE Id = @Id", new { Id = id });
        }
        public List<FileModel> GetFilesByEmployee(int employeeId)
        {
            using var conn = DbServices.DbContext.Open();
            return conn.Query<FileModel>(
                "SELECT * FROM Files WHERE EmployeeId = @Id ORDER BY CreatedAt DESC",
                new { Id = employeeId }).ToList();
        }
        // Simple MIME type detection (expand as needed)
        private string GetMimeType(string ext) => ext switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".bmp" => "image/bmp",
            ".pdf" => "application/pdf",
            ".txt" => "text/plain",
            ".doc" or ".docx" => "application/msword",
            ".xls" or ".xlsx" => "application/vnd.ms-excel",
            _ => "application/octet-stream"
        };
    }
}
