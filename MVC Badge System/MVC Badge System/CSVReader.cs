﻿using System;
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

        ////////////////////////////////////////////////////////
        // Reads a CSV containing users into the database
        // format should be in fname,lname,email,url,link order
        ////////////////////////////////////////////////////////
        public void InputUserList(string choice) {
            StreamReader file;
            string fileName;
            string line;
            int lineNumber = 0;
            List<User> tempUserList = new List<User>();

            //Console.WriteLine("Input the file name path: ");
            //fileName = Console.ReadLine();

            fileName = choice + "\\BadgeSystemPeople.csv";

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
                    if (words.Length == 6)
                    {
                        tempUser.FirstName = words[0];
                        tempUser.LastName = words[1];
                        tempUser.Email = words[2];
                        tempUser.PhotoUrl = words[3];
                        switch(words[4])
                        {
                            case "admin":
                                tempUser.UserType = UserType.Admin;
                                break;
                            case "faculty":
                                tempUser.UserType = UserType.Faculty;
                                break;
                            case "staff":
                                tempUser.UserType = UserType.Staff;
                                break;
                            case "student":
                                tempUser.UserType = UserType.Student;
                                break;
                            default:
                                throw new InvalidDataException("Invalid user type on line: " + line);
                        }
                        tempUser.ShareableLink = words[5];

                        tempUserList.Add(tempUser);
                    }
                    else
                    {
                        Console.WriteLine("Formating error line " + lineNumber);
                        break;
                    } // end if(words.length)
                } // end while(!= eof)

                foreach (User u in tempUserList)
                {
                    Db.Db.CreateUser(u);
                }
                Console.WriteLine("User database filled");
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
        public void InputBadgeList(string choice)
        {
            StreamReader file;
            string fileName;
            string line;
            int lineNumber = 0;

            //Console.WriteLine("Input the file name path: ");
            //fileName = Console.ReadLine();

            fileName = choice + "\\BadgeBank.csv";

            if (File.Exists(fileName))
            {
                file = new StreamReader(fileName);

                while ((line = file.ReadLine()) != null)
                {
                    lineNumber++;
                    string[] words = line.Split(',');
                    Badge tempBadge = new Badge();
                    

                    if (words.Length >= 10)
                    {
                        switch (words[0])
                        {
                            case "comendation":
                                tempBadge.Type = BadgeType.Leaf;
                                break;
                            case "competency":
                                tempBadge.Type = BadgeType.Flower;
                                break;
                            case "core":
                                tempBadge.Type = BadgeType.Apple;
                                break;
                            default:
                                throw new InvalidDataException("Invalid badge type on line: " + line);
                        }

                        if (words[1].Length >= 6) // min length of a datetime x/x/xx
                        {
                            tempBadge.RetirementDate = DateTime.Parse(words[1]);
                        }
                        else
                        {
                            // The maximum datetime for the database
                            // This will indicate that the badge has no retirement date
                            //when the field is blank in the .csv
                            // The database can accept null values in retirement date
                            tempBadge.RetirementDate = DateTime.Parse("12/31/9999");
                        }

                        tempBadge.BeginDate = DateTime.Parse(words[2]);
                        tempBadge.Name = words[3];
                        tempBadge.SelfGive = (words[4] == "true" || words[4] == "True" || words[4] == "T");
                        tempBadge.StudentGive = (words[5] == "true" || words[5] == "True" || words[5] == "T");
                        tempBadge.StaffGive = (words[6] == "true" || words[6] == "True" || words[6] == "T");
                        tempBadge.FacultyGive = (words[7] == "true" || words[7] == "True" || words[7] == "T");
                        tempBadge.Picture = words[8];
                        tempBadge.Description = words[9];

                        int id = Db.Db.CreateBadge(tempBadge);

                        if (!string.IsNullOrEmpty(words[10]) && !string.IsNullOrEmpty(words[11]))
                        {
                            DefaultBadge tempDefaultBadge = new DefaultBadge();
                            int treeLocX;
                            int treeLocY;
                            int.TryParse(words[10], out treeLocX);
                            int.TryParse(words[11], out treeLocY);

                            tempDefaultBadge.BadgeId = id;
                            tempDefaultBadge.TreeLocX = treeLocX;
                            tempDefaultBadge.TreeLocY = treeLocY;
                            tempDefaultBadge.Type = tempBadge.Type;

                            Db.Db.CreateDefaultBadge(tempDefaultBadge);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Formatting error line " + lineNumber);
                    } // end if(words.length)
                } // end while (!= eof)

                Console.WriteLine("Badge database filled");
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
        public void InputGiftList(string choice)
        {
            StreamReader file;
            string fileName;
            string line;
            int lineNumber = 0;
            List<Gift> tempGiftList = new List<Gift>();

            //Console.WriteLine("Input the file name path: ");
            //fileName = Console.ReadLine();

            fileName = choice + "\\BadgeGiftLog.csv";

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
                        Console.WriteLine("Formatting error line " + lineNumber);
                    } // end if(words.length)
                } // end while (!= eof)

    
                foreach (Gift g in tempGiftList)
                {
                    Db.Db.CreateGift(g);
                }

                Console.WriteLine("Gift database filled");
                file.Close();
            }
            else
            {
                Console.WriteLine("File name incorrect/does not exist!\n");
            }
        } // end input badge list
    }
}