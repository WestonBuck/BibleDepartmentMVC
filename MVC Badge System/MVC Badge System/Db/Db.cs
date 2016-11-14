using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using MVC_Badge_System.Models;

namespace MVC_Badge_System.Db
{
    public class Db
    {
        public static string Connection = "Data Source=.\\SQLEXPRESS;Initial Catalog=GSTdata;Integrated Security=True;" +
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
                                                      "TREE_LOC_X TreeLocX, TREE_LOC_Y TreeLocY, " +
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
                                        "TREE_LOC_X TreeLocX, TREE_LOC_Y TreeLocY, " +
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
                string sql = "INSERT INTO BADGES(BADGE_TYPE, BEGIN_DATE, RETIREMENT_DATE, NAME, SELF_GIVE, STUDENT_GIVE, STAFF_GIVE, FACULTY_GIVE)" +
                              "VALUES(@type, @startDate, @retireDate, @name, @self, @student, @staff, @faculty);";

                conn.Query<Badge>(sql,
                    new
                    {
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
                conn.Query("UPDATE BADGES SET BADGE_TYPE = @Type, BEGIN_DATE = @startDate, RETIREMENT_DATE = @retireDate " +
                           "NAME = @name, SELF_GIVE = @self, STAFF_GIVE = @staff, STUDENT_GIVE = @student, FACULTY_GIVE = @faculty" +
                           "WHERE BADGE_ID = @id",
                           new
                           {
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
                return conn.QueryFirstOrDefault<Badge>("SELECT BADGE_ID BadgeId, BADGE_TYPE Type, " +
                                                       "RETIREMENT_DATE RetirementDate, BEGIN_DATE BeginDate, " +
                                                       "NAME Name, SELF_GIVE SelfGive, STUDENT_GIVE StudentGive, " +
                                                       "STAFF_GIVE StaffGive, FACULTY_GIVE FacultyGive FROM BADGES WHERE BADGE_ID = @BId",
                    new{ BId = badgeId}
                    );
            }
        }

        public static List<Badge> GetAllBadges()
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                return conn.Query<Badge>("SELECT BADGE_ID BadgeId, BADGE_TYPE Type, " +
                                         "RETIREMENT_DATE RetirementDate, BEGIN_DATE BeginDate, " +
                                         "NAME Name, SELF_GIVE SelfGive, STUDENT_GIVE StudentGive, " +
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
                string sql = "INSERT INTO USERS VALUES(@fname, @lname, @email, @photoURL, @type, @shareLink);";

                conn.Query<User>(sql,
                    new
                    {
                        fname = u.FirstName,
                        lname = u.LastName,
                        email = u.Email,
                        photoURL = u.PhotoUrl,
                        type = u.UserType,
                        shareLink = u.ShareableLink
                    });
            }
        }

        public static void UpdateUser(User user)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                conn.Query("UPDATE USERS SET FIRST_NAME = @FirstName, LAST_NAME = @LastName, EMAIL = @Email, " +
                           "PHOTO_URL = @PhotoUrl, USER_TYPE = @UserType, SHAREABLE_LINK = @ShareableLink " +
                           "WHERE USER_ID = @UserId",
                           new
                           {
                               FirstName = user.FirstName,
                               LastName = user.LastName,
                               Email = user.Email,
                               PhotoUrl = user.PhotoUrl,
                               UserType = user.UserType,
                               ShareableLink = user.ShareableLink,
                               UserId = user.UserId
                           });
            }
        }

        public static User GetUser(int userId)
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                return conn.QueryFirstOrDefault<User>("SELECT USER_ID UserId, FIRST_NAME FirstName, " +
                                                      "LAST_NAME LastName, EMAIL Email, PHOTO_URL PhotoUrl, " +
                                                      "USER_TYPE UserType, SHARABLE_LINK SharableLink FROM USERS WHERE user_id = @UserId",
                    new
                    {
                        UserId = userId
                    });
            }
        }

        public static IEnumerable<User> GetAllUsers()
        {
            using (IDbConnection conn = new SqlConnection(Connection))
            {
                return conn.Query<User>("SELECT USER_ID UserId, FIRST_NAME FirstName, " +
                                        "LAST_NAME LastName, EMAIL Email, PHOTO_URL PhotoUrl, " +
                                        "USER_TYPE UserType, SHARABLE_LINK SharableLink FROM USERS");
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
                conn.Query("DELETE FROM USER WHERE USER_ID = @UserId",
                    new
                    {
                        UserId = userId
                    });
            }
        }
    }
}