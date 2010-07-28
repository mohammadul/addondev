﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SQLite;

namespace testfdb_cs
{
    class TagDB
    {

        public class FileData
        {
            public string guid;
            public string name;
            public List<string> tags;
            public string comment;

            public FileData(string guid, string name, List<string> tags, string comment)
            {
                this.guid = guid;
                this.name = name;
                this.tags = tags;
                this.comment = comment;
            }

            public string getTagsConcat()
            {
                string seltags = "";
                foreach (string tag in tags)
                {
                    seltags += "'" + tag + "'";
                    if (tags[tags.Count - 1] != tag)
                    {
                        seltags += ",";
                    }
                }

                return seltags;
            }
        }

        public delegate void tagsSelect(string guid, string name, string tags, string comment);
        public tagsSelect tagsSelectEvent = null;

        private string name;
        public string FileName
        {
            get 
            {
                return name;
            }
            set 
            {
                this.name = value;
            }
        }

        private string filetablename;
        public string FileTableName
        {
            get
            {
                return filetablename;
            }
            set
            {
                this.filetablename = value;
            }
        }

        private string tagtablename;
        public string TagTableName
        {
            get
            {
                return tagtablename;
            }
            set
            {
                this.tagtablename = value;
            }
        }

        SQLiteConnection connection;
        SQLiteCommand cmd;
        SQLiteTransaction transaction;

        public TagDB()
        {
            connection = new SQLiteConnection("Data Source=" + FileName);
            connection.Open();

            cmd = connection.CreateCommand();
        }


        public void Dispose()
        {
            //throw new NotImplementedException();
            cmd.Dispose();
            connection.Close();
        }


        private void beginTransaction()
        {
            transaction = this.connection.BeginTransaction();
        }

        private void commitTransaction()
        {
            transaction.Commit();
            transaction.Dispose();
            transaction = null;
        }

        public void createTable()
        {
            if (new FileInfo(FileName).Exists)
            {
                //using (SQLiteConnection cnn = new SQLiteConnection("Data Source=" + Name))
                //using (SQLiteCommand cmd = cnn.CreateCommand())
                //{
                //    cnn.Open();
                cmd.CommandText = String.Format("CREATE TABLE {0} (guid TEXT PRIMARY KEY, name TEXT, tags TEXT, comment TEXT)", FileTableName);
                cmd.ExecuteNonQuery();

                cmd.CommandText = String.Format("CREATE TABLE {0} (guid TEXT PRIMARY KEY, tag TEXT)", TagTableName);
                cmd.ExecuteNonQuery();
                //    cnn.Close();
                //}
                
            }
        }

        public void insertFiles(List<FileData> filedatas, List<string> addtags)
        {
            beginTransaction();

            foreach(FileData file in filedatas)
            {
                IEnumerable<string> newtags = file.tags.Union(addtags).Except(file.tags.Intersect(addtags));
                file.tags = file.tags.Union(addtags).ToList<string>();

                if (hasFileData(file.guid))
                {
                    //updateFile
                }
                else
                {
                    string strcmd = String.Format("INSERT INTO {0}(guid, name, tags, comment) VALUES('{1}', '{2}', '{3}', '{4}')",
                        FileTableName, file.guid, file.name, file.getTagsConcat(), file.comment);
                    cmd.CommandText = strcmd;
                    cmd.ExecuteNonQuery();
                }
                foreach (string tag in newtags)
                {
                    if (!hasTagData(file.guid, tag))
                    {
                        string tagcmd = String.Format("INSERT INTO {0}(guid,tag) VALUES('{1}', '{2}')",
                            TagTableName, file.guid, tag);
                        cmd.CommandText = tagcmd;
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            commitTransaction();
        }

        public void updateFile(string guid, string name)
        {
            string strcmd = String.Format("UPDATE {0} SET name = '{1}' where guid = '{2}'", FileTableName, name, guid);
            cmd.CommandText = strcmd;
            cmd.ExecuteNonQuery();
        }

        public void updateFile(string guid, List<string> tags)
        {
        }

        public void updateFile(string guid, string comment)
        {
            string strcmd = String.Format("UPDATE {0} SET comment = '{1}' where guid = '{2}'", FileTableName, comment, guid);
            cmd.CommandText = strcmd;
            cmd.ExecuteNonQuery();
        }

        public void updateFile(string guid, FileData filedata)
        {
            //string strcmd = String.Format("UPDATE {0} SET comment = '{1}' where guid = '{2}'", FileTableName, comment, guid);
            //cmd.CommandText = strcmd;
            //cmd.ExecuteNonQuery();
        }

        public void insertTags(string[] guids, string[] tags)
        {
            beginTransaction();

            foreach (string guid in guids)
            {
                foreach(string tag in tags)
                {
                    string strcmd = String.Format("INSERT INTO {0}(guid,tag) VALUES('{1}', '{2}')",
                        TagTableName, guid, tag);
                    cmd.CommandText = strcmd;
                    cmd.ExecuteNonQuery();
                }
            }

            commitTransaction();
        }

        public void selectTags(string[] tags)
        {
            string seltags="";
            foreach(string tag in tags)
            {
                seltags += "'" + tag + "'";
                if(tags[tags.Length-1] != tag)
                {
                    seltags+=",";
                }
            }
                                      //SELECT * FROM file WHERE id IN (SELECT id FROM tag WHERE tag IN ('text', 'src') GROUP BY id HAVING COUNT(*) = 2)
            string sql = String.Format("SELECT * FROM {0} WHERE {1} IN (SELECT {1} FROM {2} WHERE {2} IN ({3}) GROUP BY {1} HAVING COUNT(*) = {7})",
                FileTableName, "guid", "tag", seltags, tags.Length);

            beginTransaction();

            cmd.CommandText = sql;
            //command.ExecuteNonQuery();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    if (reader.FieldCount == 3)
                    {
                        tagsSelectEvent((string)reader[0], (string)reader[1], (string)reader[2], (string)reader[3]);
                    }
                }
            }

            commitTransaction();
        }

        public bool hasFileData(string guid)
        {
            string strcmd = String.Format("SELECT COUNT({0}) FROM {1} WHERE guid = '{0}'", guid, FileTableName);
            cmd.CommandText = strcmd;
            return !cmd.ExecuteScalar().Equals(0);
        }

        public bool hasTagData(string guid, string tag)
        {
            string strcmd = String.Format("SELECT COUNT(*) FROM {1} WHERE guid = '{0}' AND tag = '{2}'", guid, TagTableName, tag);
            cmd.CommandText = strcmd;
            return !cmd.ExecuteScalar().Equals(0);
        }
    }
}