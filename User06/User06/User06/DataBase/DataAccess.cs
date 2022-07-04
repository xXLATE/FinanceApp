using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace User06.DataBase
{
    public class DataAccess
    {
        private SQLiteConnection _db;

        public DataAccess(string databasePath)
        {
            _db = new SQLiteConnection(databasePath);
            _db.CreateTable<User>();
            _db.CreateTable<Category>();
            _db.CreateTable<Record>();
            _db.CreateTable<RecordType>();
        }

        public int SaveUser(User user)
        {
            if (user.Id != 0)
            {
                _db.Update(user);
                return user.Id;
            }
            else
                return _db.Insert(user);
        }

        public IEnumerable<User> GetUsers()
        {
            return _db.Table<User>();
        }

        public int SaveCategory(Category category)
        {
            if (category.Id != 0)
            {
                _db.Update(category);
                return category.Id;
            }
            else
                return _db.Insert(category);
        }

        public int SaveRecordType(RecordType recordType)
        {
            if (recordType.Id != 0)
            {
                _db.Update(recordType);
                return recordType.Id;
            }
            else
                return _db.Insert(recordType);
        }

        public int SaveRecord(Record record)
        {
            if (record.Id != 0)
            {
                _db.Update(record);
                return record.Id;
            }
            else
                return _db.Insert(record);
        }

        public IEnumerable<Category> GetCategories()
        {
            return _db.Table<Category>().ToList();
        }

        public IEnumerable<Record> GetRecords()
        {
            return _db.Table<Record>().ToList();
        }

        public IEnumerable<RecordType> GetRecordTypes()
        {
            return _db.Table<RecordType>().ToList();
        }

        public void DeleteRecord(Record record)
        {
            _db.Delete(record);
        }

        public IEnumerable<Record> GetRecordsByUser(int idUser)
        {
            return GetRecords().Where(record => record.User_Id == idUser);
        }

        public IEnumerable<Category> GetCategoriesByUser(int idUser)
        {
            return GetCategories().Where(category => category.User_Id == idUser);
        }
    }
}
