using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using MVC_Badge_System.Models;
using Dapper;

namespace MVC_Badge_System
{

    public class CSVReader
    {
        // contains the path of the database
        private string wPath()
        {
            string pathName = "Data Source=.\\SQLEXPRESS;Initial Catalog=GSTdata;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            return pathName;
        }


        ////////////////////////////////////////////////////////
        // Reads a CSV containing users into the database
        // format should be in fname,lname,email,url,link order
        ////////////////////////////////////////////////////////
        public void InputUserList() {
            StreamReader file;
            string fileName;
            string line;
            int lineNumber = 0;
            //const string CSV = ".csv";
            List<User> tempUserList = new List<User>();

            Console.WriteLine("Input the file name path: ");
            fileName = Console.ReadLine();

            //if(fileName.Substring(fileName.Length-4,4) != CSV)
            //{
            //    fileName = fileName + CSV;
            //}


            if (File.Exists(fileName))
            {
                file = new StreamReader(fileName);

                while ((line = file.ReadLine()) != null)
                {
                    lineNumber++;
                    string[] words = line.Split(',');
                    User tempUser = new User();

                    // determines the number of elements per row as many elements are optional
                    // user_type and shareable_link cannot be null
                    // sample data contains only 2 comma seperated fields
                    if (words.Length >= 6)
                    {
                        tempUser.FirstName = words[0];
                        tempUser.LastName = words[1];
                        tempUser.Email = words[2];
                        tempUser.PhotoUrl = words[3];
                        tempUser.UserType = words[4];
                        tempUser.ShareableLink = words[5];

                        tempUserList.Add(tempUser);
                    }
                    else
                    {
                        Console.WriteLine("Formating error line " + lineNumber);
                        break;
                    } // end if(words.length)
                } // end while(!= eof)

                using (IDbConnection db = new SqlConnection(this.wPath()))
                {
                    foreach (User u in tempUserList)
                    {
                        db.Execute("INSERT INTO USER_ (FIRST_NAME, LAST_NAME, EMAIL, PHOTO_URL, USER_TYPE, SHAREABLE_LINK) VALUES (@first_Name, @last_Name, @email, @photo_url, @user_type, @shareable_link)", u);
                    }
                }

                file.Close();
            }
            else
            {
                Console.WriteLine("File name incorrect/does not exist!\n");
            }
        }// end input user list


        ////////////////////////////////////////////////////////
        // Reads a CSV containing badges into the database
        // format should be in type,retire,start,name,self,student,staff,faculty order
        ////////////////////////////////////////////////////////
        public void InputBadgeList()
        {
            StreamReader file;
            string fileName;
            string line;
            int lineNumber = 0;
            List<Badge> tempBadgeList = new List<Badge>();

            Console.WriteLine("Input the file name path: ");
            fileName = Console.ReadLine();

            if (File.Exists(fileName))
            {
                file = new StreamReader(fileName);

                while ((line = file.ReadLine()) != null)
                {
                    lineNumber++;
                    string[] words = line.Split(',');
                    Badge tempBadge = new Badge();

                    if (words.Length == 8)
                    {
                        tempBadge.Type = words[0];
                        tempBadge.RetirementDate = DateTime.Parse(words[1]);
                        tempBadge.BeginDate = DateTime.Parse(words[2]);
                        tempBadge.Name = words[3];
                        tempBadge.SelfGive = (words[4] == "true" || words[4] == "True" || words[4] == "T");
                        tempBadge.StudentGive = (words[5] == "true" || words[5] == "True" || words[5] == "T");
                        tempBadge.StaffGive = (words[6] == "true" || words[6] == "True" || words[6] == "T");
                        tempBadge.FacultyGive = (words[7] == "true" || words[7] == "True" || words[7] == "T");

                        tempBadgeList.Add(tempBadge);
                    }
                    else
                    {
                        Console.WriteLine("Formatiing error line " + lineNumber);
                    } // end if(words.length)
                } // end while (!= eof)

                using (IDbConnection db = new SqlConnection(this.wPath()))
                {
                    foreach (Badge b in tempBadgeList)
                    {
                        db.Execute("INSERT INTO BADGE (TYPE, RETIREMENT_DATE, START_DATE, NAME, SELF_GIVE, STUDENT_GIVE, STAFF_GIVE, FACULTY_GIVE) VALUES (@type, @retirement_date, @start_date, @name, @self_give, @student_give, @staff_give, @faculty_give)", b);
                    }
                }

                file.Close();
            }
            else
            {
                Console.WriteLine("File name incorrect/does not exist!\n");
            }
        } // end input badge list


        ////////////////////////////////////////////////////////
        // Reads a CSV containing badges into the database
        // format should be in badge,sender,recipient,loc_x,loc_y,comment order
        ////////////////////////////////////////////////////////
        public void InputGiftList()
        {
            StreamReader file;
            string fileName;
            string line;
            int lineNumber = 0;
            List<Gift> tempGiftList = new List<Gift>();

            Console.WriteLine("Input the file name path: ");
            fileName = Console.ReadLine();


            if (File.Exists(fileName))
            {
                file = new StreamReader(fileName);

                while ((line = file.ReadLine()) != null)
                {
                    lineNumber++;
                    string[] words = line.Split(',');
                    Gift tempGift = new Gift();

                    if (words.Length == 6)
                    {
                        tempGift.BadgeId = Int32.Parse(words[0]);
                        tempGift.SenderId = Int32.Parse(words[1]);
                        tempGift.RecipientId = Int32.Parse(words[2]);
                        tempGift.TreeLocX = Int32.Parse(words[3]);
                        tempGift.TreeLocY = Int32.Parse(words[4]);
                        tempGift.Comment = words[5];

                        tempGiftList.Add(tempGift);
                    }
                    else
                    {
                        Console.WriteLine("Formatiing error line " + lineNumber);
                    } // end if(words.length)
                } // end while (!= eof)

                using (IDbConnection db = new SqlConnection(this.wPath()))
                {
                    foreach (Gift g in tempGiftList)
                    {
                        db.Execute("INSERT INTO GIFT (BADGE_ID, SENDER_ID, RECIPIENT_ID, TREE_LOC_X, TREE_LOC_Y, COMMENT) VALUES (@badge_id, @sender_id, @recipient_id, @tree_loc_x, @tree_loc_y, @comment)", g);
                    }
                }

                file.Close();
            }
            else
            {
                Console.WriteLine("File name incorrect/does not exist!\n");
            }
        } // end input badge list
    }
}