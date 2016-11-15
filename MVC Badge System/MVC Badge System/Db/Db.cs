﻿using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using MVC_Badge_System.Models;

namespace MVC_Badge_System.Db
{
    public class Db
    {
        public const string Connection = "Data Source=.\\SQLEXPRESS;Initial Catalog=GSTdata;Integrated Security=True;" +
                                         "Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;" +
                                         "MultiSubnetFailover=False";

        //
        // GIFT
        //
        public static void CreateGift(Gift gift)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                conn.Query("INSERT INTO GIFT(BADGE_ID, SENDER_ID, RECIPIENT_ID, TREE_LOC_X, TREE_LOC_Y, COMMENT)" +
                           "VALUES (@BadgeId, @SenderId, @RecipientId, @TreeLocX, @TreeLocY, @Comment)",
                           new
                           {
                               BadgeId = gift.BadgeId,
                               SenderId = gift.SenderId,
                               RecipientId = gift.RecipientId,
                               TreeLocX = gift.TreeLocX,
                               TreeLocY = gift.TreeLocY,
                               Comment = gift.Comment
                           });
            }
        }

        public static void UpdateGift(Gift gift)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                conn.Query("UPDATE GIFT SET BADGE_ID = @BadgeId, SENDER_ID = @SenderId, RECIPIENT_ID = @RecipientId " +
                           "TREE_LOC_X = @TreeLocX, TREE_LOC_Y = @TreeLocY, COMMENT = @Comment " +
                           "WHERE GIFT_ID = @GiftId",
                           new
                           {
                               BadgeId = gift.BadgeId,
                               SenderId = gift.SenderId,
                               RecipientId = gift.RecipientId,
                               TreeLocX = gift.TreeLocX,
                               TreeLocY = gift.TreeLocY,
                               Comment = gift.Comment,
                               GiftId = gift.GiftId
                           });
            }
        }

        public static Gift GetGift(int giftId)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                return conn.QueryFirstOrDefault<Gift>("SELECT GIFT_ID GiftId, BADGE_ID BadgeId, " +
                                                      "SENDER_ID SnederId, RECIPIENT_ID RecipientId, " +
                                                      "TREE_LOC_X TreeLocX, TREE_LOC_Y TreeLocY," +
                                                      "COMMENT Comment FROM GIFT WHERE GIFT_ID = @GId",
                    new
                    {
                        GId = giftId
                    });
            }
        }

        public static IEnumerable<Gift> GetAllGifts()
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                return conn.Query<Gift>("SELECT GIFT_ID GiftId, BADGE_ID BadgeId, " +
                                        "SENDER_ID SnederId, RECIPIENT_ID RecipientId, " +
                                        "TREE_LOC_X TreeLocX, TREE_LOC_Y TreeLocY," +
                                        "COMMENT Comment FROM GIFT");
            }
        }

        public static void DeleteGift(Gift gift)
        {
            DeleteGift(gift.GiftId);
        }

        public static void DeleteGift(int giftId)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                conn.Query("DELETE FROM GIFT WHERE GIFT_ID = @GiftId",
                    new
                    {
                        GiftId = giftId
                    });
            }
        }

        //
        // BADGE
        //
        public static void CreateBadge(Badge b)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                string sql = @"INSERT INTO BADGES(DESCRIPTION, BADGE_TYPE, BEGIN_DATE, RETIREMENT_DATE, NAME, SELF_GIVE, STUDENT_GIVE, STAFF_GIVE, FACULTY_GIVE)" +
                              "VALUES('@descript','@type','@startDate','@retireDate','@name','@self','@student','@staff','@faculty');";

                conn.Query<Badge>(sql,
                    new
                    {
                        descript=  b.Description,
                        type = b.Type,
                        startDate = b.BeginDate,
                        retireDate = b.RetirementDate,
                        name = b.Name,
                        self = b.SelfGive,
                        student = b.StudentGive,
                        staff = b.StaffGive,
                        faculty = b.FacultyGive,
                    });
            }
        }

        public static void UpdateBadge(Badge b)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                conn.Query("UPDATE BADGES SET DESCRIPTION = @descript BADGE_TYPE = @type, BEGIN_DATE = @startDate, RETIREMENT_DATE = @retireDate " +
                           "NAME = @name, SELF_GIVE = @self, STAFF_GIVE = @staff, STUDENT_GIVE = @student, FACULTY_GIVE = @faculty" +
                           "WHERE BADGE_ID = @id",
                           new
                           {
                               descript = b.Description,
                               id = b.BadgeId,
                               type = b.Type,
                               startDate = b.BeginDate,
                               retireDate = b.RetirementDate,
                               name = b.Name,
                               self = b.SelfGive,
                               student = b.StudentGive,
                               staff = b.StaffGive,
                               faculty = b.FacultyGive,
                           });
            }
        }

        public static Badge GetBadge(int badgeId)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                return conn.QueryFirstOrDefault<Badge>("SELECT BADGE_ID BadgeId, DESCRIPTION Description, BADGE_TYPE Type," +
                                                       "RETIREMENT_DATE RetirementDate, BEGIN_DATE BeginDate," +
                                                       "NAME Name, SELF_GIVE SelfGive, STUDENT_GIVE StudentGive," +
                                                       "STAFF_GIVE StaffGive, FACULTY_GIVE FacultyGive FROM BADGES WHERE BADGE_ID = @BId",
                    new{ BId = badgeId}
                    );
            }
        }

        public static List<Badge> GetAllBadges()
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                return conn.Query<Badge>("SELECT BADGE_ID BadgeId, DESCRIPTION Description, BADGE_TYPE Type," +
                                         "RETIREMENT_DATE RetirementDate, BEGIN_DATE BeginDate," +
                                         "NAME Name, SELF_GIVE SelfGive, STUDENT_GIVE StudentGive," +
                                         "STAFF_GIVE StaffGive, FACULTY_GIVE FacultyGive FROM BADGES").AsList();
            }
        }

        public static void DeleteBadge(Badge badge)
        {
            DeleteBadge(badge.BadgeId);
        }

        public static void DeleteBadge(int badgeId)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                conn.Query("DELETE FROM BADGES WHERE BADGE_ID = @BadgeId",
                    new
                    {
                        BadgeId = badgeId
                    });
            }
        }

        //
        // USER
        //
        public static void CreateUser(User u)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                string sql = @"INSERT USERS (first_name, last_name, email, photo_url, user_type, sharable_link)" +
                              "VALUES(@FirstName, @LastName, @Email, @PhotoUrl, @UserType, @ShareableLink);";

                conn.Query(sql, u);
            }
        }

        public static void UpdateUser(User user)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                string sql = "UPDATE USERS SET first_name = @FirstName, last_name = @LastName, email = @Email, " +
                             "photo_url = @PhotoUrl, user_type = @UserType, sharable_link = @ShareableLink " +
                             "WHERE user_id = @UserId";
                conn.Query(sql, user);
            }
        }

        public static User GetUser(int userId)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                return conn.QueryFirstOrDefault<User>("SELECT user_id UserId, first_name FirstName," +
                                                      "last_name LastName, email Email, photo_url PhotoUrl," +
                                                      "user_type UserType, sharable_link SharableLink FROM USERS u WHERE user_id = @UserId",
                    new
                    {
                        UserId = userId
                    });
            }
        }

        public static List<User> GetAllUsers()
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                return conn.Query<User>("SELECT user_id UserId, first_name FirstName," +
                                        "last_name LastName, email Email, photo_url PhotoUrl," +
                                        "user_type UserType, sharable_link SharableLink FROM USERS").AsList();
            }
        }

        public static void DeleteUser(User user)
        {
            DeleteUser(user.UserId);
        }

        public static void DeleteUser(int userId)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                conn.Query("DELETE FROM USERS WHERE user_id = @UserId",
                    new
                    {
                        UserId = userId
                    });
            }
        }
    }
}